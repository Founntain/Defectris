using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncColor : AudioSyncer
{
    public Color BeatColor;
    public Color RestColor;

    public override void OnUpdate(){
        base.OnUpdate();

        if(isBeat) return;

        Color currentColor = new Color();

        var color = Color.Lerp(currentColor, RestColor, RestSmoothTime * Time.deltaTime);
    }

    public override void OnBeat(){
        base.OnBeat();

        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", BeatColor);
    }

    private IEnumerator MoveToColor(Color target){
        Color cur = new Color();
        Color initial = cur;
        
        float timer = 0;

        while(cur != target){
            cur = Color.Lerp(initial, target, timer / TimeToBeat);
            timer += Time.deltaTime;

            var color = cur;
            yield return null;
        }

        isBeat = false;
    }
}