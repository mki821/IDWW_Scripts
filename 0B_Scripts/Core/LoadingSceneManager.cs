using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoSingleton<LoadingSceneManager>
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Image _progressBar;

    protected override void Awake() {
        base.Awake();
        
        Application.targetFrameRate = 60;
    }

    private void Start() {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode) {
        _loadingPanel.SetActive(false);
        _progressBar.fillAmount = 0f;
    }

    public void LoadScene(int sceneNum) {
        StartCoroutine(LoadSceneCoroutine(sceneNum));
    }

    private IEnumerator LoadSceneCoroutine(int sceneNum) {
        _loadingPanel.SetActive(true);
        Resources.UnloadUnusedAssets();

        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneNum);
        op.allowSceneActivation = false;

        float timer = 0f;

        while(!op.isDone) {
            yield return null;
            timer += Time.deltaTime;

            if(op.progress < 0.90f) {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, op.progress, timer);
                if(_progressBar.fillAmount > op.progress) {
                    timer = 0f;
                }
            }
            else {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, 1f, timer);
                if(_progressBar.fillAmount == 1f) {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
