using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] Swither musicSwithcer;

    [Space(10)]
    [SerializeField] Swither soundsSwitcher;

    [Space(10)]
    [SerializeField] Swither vibraSwithcer;

    [Space(10)]
    [SerializeField] Sprite spriteOn;
    [SerializeField] Sprite spriteOff;

    public static bool VibraEnbled { get; set; } = true;
    public static bool SoundsEnabled { get; set; } = true;

    private void Start()
    {
        musicSwithcer.Switch(spriteOn, spriteOff, !music.mute);
        soundsSwitcher.Switch(spriteOn, spriteOff, SoundsEnabled);
        vibraSwithcer.Switch(spriteOn, spriteOff, VibraEnbled);
    }

    public void SetMusicStatus()
    {
        music.mute = !music.mute;
        musicSwithcer.Switch(spriteOn, spriteOff, !music.mute);
    }

    public void SetSoundsStatus()
    {
        SoundsEnabled = !SoundsEnabled;
        soundsSwitcher.Switch(spriteOn, spriteOff, SoundsEnabled);
    }

    public void SetVibrationStatus()
    {
        VibraEnbled = !VibraEnbled;
        vibraSwithcer.Switch(spriteOn, spriteOff, VibraEnbled);
    }
}
