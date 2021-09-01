using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    public float Bias;
    public float TimeStep;
    public float TimeToBeat;
    public float RestSmoothTime;

    private float previousAudioValue;
    private float audioValue;
    private float timer;

    protected bool isBeat;

    public virtual void OnBeat(){ 
        timer = 0;
        isBeat = true;
    }

    public virtual void OnUpdate(){
        previousAudioValue = audioValue;
        audioValue = AudioSpectrum.SpectrumValue;

        if(previousAudioValue > Bias && audioValue <= Bias){
            if(timer > TimeStep)
                OnBeat();
        }

        if(previousAudioValue <= Bias && audioValue > Bias){
            if(timer > TimeStep)
                OnBeat();
        }

        timer += Time.deltaTime;
    }

    private void Update(){
        OnUpdate();
    }
}