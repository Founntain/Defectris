using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncLightIntensity : AudioSyncer
{
    public Color BeatColor;
    public Color RestColor;
    private Light light;

    private void Start(){
        this.light = GetComponent<Light>();
    }

    public override void OnUpdate(){
        base.OnUpdate();

        if(isBeat) {
            this.light.color = BeatColor;
            return;
        }

        this.light.color = Color.Lerp(BeatColor, RestColor, RestSmoothTime * Time.deltaTime);
    }

    public override void OnBeat(){
        base.OnBeat();

        StopCoroutine("ChangeColor");
        StartCoroutine("ChangeColor", BeatColor);
    }

    private IEnumerator ChangeColor(Color target){
        var cur = this.light.color;
        var initial = cur;
        
        float timer = 0;

        while(cur != target){
            cur = Color.Lerp(initial, target, timer / TimeToBeat);
            timer += Time.deltaTime;

            this.light.color = cur;
            yield return null;
        }

        isBeat = false;
    }
}