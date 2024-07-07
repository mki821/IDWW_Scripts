using System.Collections;
using UnityEngine;

public class WaveCrystal : MonoBehaviour, IInteractable
{
    [SerializeField] WaveSO[] waves;
    [SerializeField] private int _index;
    public WaveCrystal nextCrystal;
    public GameObject crystalProgressImage;
    Rigidbody[] rigids;

    private void Awake()
    {
        rigids = transform.GetComponentsInChildren<Rigidbody>();
    }

    [ContextMenu("asdasd")]
    public void Interact(PlayerInteraction player)
    {
        UIManager.Instance.ShowChatBox(false);
        player.EndInteract();

        SoundManager.Instance.PlaySFX("CrystalBreak");

        MapManager.Instance.StartBattleZone(transform.position, _index);

        StartCoroutine(DelayStartWave());
    }

    private IEnumerator DelayStartWave()
    {
        WaveManager.Instance.StartWaves(waves, nextCrystal, this, transform.position);
        foreach (var rigid in rigids)
        {
            rigid.isKinematic = false;
            rigid.useGravity = true;
        }
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void UpdateInteract() {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.y -= 50;
        UIManager.Instance.MoveChatBox(screenPos);
    }
}
