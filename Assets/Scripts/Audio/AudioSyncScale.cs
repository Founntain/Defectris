using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncScale : AudioSyncer
{
    public Vector3 BeatScale;
    public Vector3 RestScale;

    public override void OnUpdate(){
        base.OnUpdate();

        if(isBeat) return;

        transform.localScale = Vector3.Lerp(transform.localScale, RestScale, RestSmoothTime * Time.deltaTime);
    }

    public override void OnBeat(){
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", BeatScale);
    }

    private IEnumerator MoveToScale(Vector3 target){
        Vector3 cur = transform.localScale;
        Vector3 initial = cur;
        
        float timer = 0;

        while(cur != target){
            cur = Vector3.Lerp(initial, target, timer / TimeToBeat);
            timer += Time.deltaTime;

            transform.localScale = cur;
            yield return null;
        }

        isBeat = false;
    }
}