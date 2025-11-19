using UnityEngine;

public class GuardarConfig : MonoBehaviour
{
    void Start()
    {
        float volumenGuardado = PlayerPrefs.GetFloat("volumenAudio", 0.5f);

        AudioListener.volume = volumenGuardado;
    }
}
