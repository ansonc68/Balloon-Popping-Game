using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Reference to the Slider
    public Slider volumeSlider;
    public Toggle muteToggle;  // Reference to the Toggle
    private float previousVolume;  // To store the volume before muting

    void Start()
    {
        // Initialize the slider value with the current AudioListener volume
        volumeSlider.value = AudioListener.volume;

        // Add a listener to the slider's value change event
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // Initialize muteToggle based on the AudioListener's volume
        if (AudioListener.volume == 0)
        {
            muteToggle.isOn = true;  // Mute the toggle if the volume is 0
        }
        else
        {
            muteToggle.isOn = false;  // Unmute the toggle if the volume is > 0
        }

        // Add a listener to detect when the toggle value changes
        muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
    }

    // This method will be called when the slider value changes
    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }

    // This method will be called when the mute toggle value changes
    void OnMuteToggleChanged(bool isMuted)
    {
        if (isMuted)
        {
            // Store the current volume before muting, so we can restore it later
            previousVolume = AudioListener.volume;
            AudioListener.volume = 0;  // Mute audio
        }
        else
        {
            // Restore the previous volume when unmuting
            AudioListener.volume = previousVolume;
        }
    }
}
