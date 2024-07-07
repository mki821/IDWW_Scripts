using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private Setting _setting;

    [Header("Input Setting")]
    [SerializeField] private InputReader _inputReader;

    private void Start() {
        _inputReader.PauseAction += HandleTogglePause;
        
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        _inputReader.PauseAction -= HandleTogglePause;
    }

    public void HandleTogglePause() {
        gameObject.SetActive(!gameObject.activeSelf);
        
        _inputReader.ToggleControl(!gameObject.activeSelf);

        Time.timeScale = gameObject.activeSelf ? 0f : 1f;
    }

    public void OpenSetting() {
        _setting.gameObject.SetActive(true);
    }

    public void GotoMain() {
        _inputReader.ToggleControl(true);
        Time.timeScale = 1f;
        LoadingSceneManager.Instance.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }
}
