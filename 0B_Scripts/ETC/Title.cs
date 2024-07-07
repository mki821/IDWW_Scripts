using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _setting;

    private void Awake() {
        SoundManager.Instance.PlayBGM("Loarune");
    }

    public void ToggleTitle() {
        _setting.SetActive(_title.activeSelf);
        _title.SetActive(!_title.activeSelf);
    }
}
