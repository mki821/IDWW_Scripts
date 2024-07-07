using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private SettingManager _settingManager;
    private SoundManager _soundManager;

    private void Start() {
        _settingManager = SettingManager.Instance;
        _soundManager = SoundManager.Instance;
    }

    public void LoadScene(int num) {
        LoadingSceneManager.Instance.LoadScene(num);
    }

    public void SetResolution(int value) {
        _settingManager.Resolution = (Resolution)value;
        _settingManager.SetVideo();
    }

    public void SetFullScreen(int value) {
        _settingManager.FullScreen = (FullScreenMode)value + 1;
        _settingManager.SetVideo();
    }

    public void SetMasterVolume() {
        _soundManager.SetVolumeMaster(_masterSlider.value);
    }

    public void SetBGMVolume() {
        _soundManager.SetVolumeBgm(_bgmSlider.value);
    }

    public void SetSFXVolume() {
        _soundManager.SetVolumeSFX(_sfxSlider.value);
    }
}
