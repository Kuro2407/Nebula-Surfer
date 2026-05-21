using UnityEngine;
using UnityEngine.UI;

public class LogicaAudio : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        EstoyMute();
    }

    // Update is called once per frame
    public void ChangeSliderVolume(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", slider.value);
        AudioListener.volume = slider.value;
        EstoyMute();
    }
    public void EstoyMute()
    {
        imagenMute.gameObject.SetActive(sliderValue == 0);
    }
}
