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
        GetComponent<AudioSource>().PlayOneShot(RotateSound);
    }

    public void PlayMoveSound(){
        GetComponent<AudioSource>().PlayOneShot(MoveSound);
    }

    public void PlayHardDropSound(){
        GetComponent<AudioSource>().PlayOneShot(HardDropSound);
    }

    public void PlayLineClearSound(){
        GetComponent<AudioSource>().PlayOneShot(LineClearSound);
    }

    public void PlayComboSound(){
        GetComponent<AudioSource>().PlayOneShot(ComboSound);
    }

    public void PlayTSpinSound(){
        GetComponent<AudioSource>().PlayOneShot(TSpinSound);
    }

    public void PlayTSpinLineClearSound(){
        GetComponent<AudioSource>().PlayOneShot(TSpinLineClearSound);
    }
}
