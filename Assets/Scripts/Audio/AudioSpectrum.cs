using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float SpectrumValue {get; private set;}
    public int SpectrumArraySize;

    private float[] audioSpectrum;

    // Start is called before the first frame update
    void Start()
    {
        audioSpectrum = new float[SpectrumArraySize];
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);

        if(audioSpectrum != null && audioSpectrum.Length > 0){
            SpectrumValue = audioSpectrum[0] * 100;
        }
    }
}
