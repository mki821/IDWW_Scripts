using DG.Tweening;
using ObjectPooling;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour, IPoolable
{
    private TextMeshPro _tmpText;

    [SerializeField] private PoolingType _poolingType;
    public PoolingType PoolingType { 
        get => _poolingType; 
        set => _poolingType = value; }

    public GameObject GameObject => gameObject;

    public IPoolable Poolable => this;

    private Sequence seq;

    private void Awake()
    {
        _tmpText = GetComponent<TextMeshPro>();
    }

    public void StartPopUp(string text, Vector3 pos, int fontSize, Color color, float time = 1f, float yDelta = 2f)
    {
        _tmpText.SetText(text);
        _tmpText.color = color;
        _tmpText.fontSize = fontSize;
        transform.position = pos;

        float scaleTime = 0.2f;
        float fadeTime = 1.5f;

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.2f, scaleTime));
        seq.Append(transform.DOScale(0.8f, scaleTime));
        seq.Append(transform.DOScale(0.3f, fadeTime));
        seq.Join(_tmpText.DOFade(0, fadeTime));
        seq.Join(transform.DOLocalMoveY(pos.y + yDelta, fadeTime));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance?.Push(this);
        });
    }

    private void LateUpdate()
    {
        Transform mainCamTrm = Camera.main.transform;
        Vector3 lookDirection = (transform.position - mainCamTrm.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void ResetItem()
    {
        transform.localScale = Vector3.one;
        _tmpText.alpha = 1f;
    }
}
