using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip RotateSound;
    public AudioClip MoveSound;
    public AudioClip HardDropSound;
    public AudioClip LineClearSound;
    public AudioClip ComboSound;
    public AudioClip TSpinSound;
    public AudioClip TSpinLineClearSound;

    public void PlayRotateSound(){
        ResetPitch();
        GetComponent<AudioSource>().PlayOneShot(RotateSound);
    }

    public void PlayMoveSound(){
        ResetPitch();
        GetComponent<AudioSource>().PlayOneShot(MoveSound);
    }

    public void PlayHardDropSound(){
        ResetPitch();
        GetComponent<AudioSource>().PlayOneShot(HardDropSound);
    }

    public void PlayLineClearSound(){
        var pitch = 1f;
        
        if(GameLogic.Combo > 0){
            pitch += (float) GameLogic.Combo * 0.05f;
        }

        GetComponent<AudioSource>().pitch = pitch;
        GetComponent<AudioSource>().PlayOneShot(LineClearSound);
    }

    public void PlayComboSound(){
        ResetPitch();
        GetComponent<AudioSource>().PlayOneShot(ComboSound);
    }

    public void PlayTSpinSound(){
        ResetPitch();
        GetComponent<AudioSource>().PlayOneShot(TSpinSound);
    }

    public void PlayTSpinLineClearSound(){
        ResetPitch();
        GetComponent<AudioSource>().PlayOneShot(TSpinLineClearSound);
    }

    private void ResetPitch(){
        GetComponent<AudioSource>().pitch = 1f;
    }
}
