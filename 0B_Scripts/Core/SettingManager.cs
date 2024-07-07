using UnityEngine;

public enum Resolution {
    SD, HD, FHD, QHD
}

public class SettingManager : MonoSingleton<SettingManager>
{
    public Resolution Resolution { get; set; } = Resolution.FHD;
    public FullScreenMode FullScreen { get; set; } = FullScreenMode.FullScreenWindow;

    protected override void Awake() {
        base.Awake();

        SetVideo();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SetVideo() {
        int width = 1920, height = 1080;

        switch(Resolution) {
            case Resolution.SD:
                width = 854;
                height = 480;
                break;
            case Resolution.HD:
                width = 1280;
                height = 720;
                break;
            case Resolution.FHD:
                width = 1920;
                height = 1080;
                break;
            case Resolution.QHD:
                width = 2560;
                height = 1440;
                break;
        }

        Screen.SetResolution(width, height, FullScreen);
    }
}
