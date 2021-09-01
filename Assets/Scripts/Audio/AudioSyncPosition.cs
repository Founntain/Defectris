using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncPosition : AudioSyncer
{
    public Vector3 BeatPosition;
    public Vector3 RestPosition;

    public override void OnUpdate(){
        base.OnUpdate();

        if(isBeat) return;

        transform.position = Vector3.Lerp(transform.position, RestPosition, RestSmoothTime * Time.deltaTime);
    }

    public override void OnBeat(){
        base.OnBeat();

        StopCoroutine("MoveToPosition");
        StartCoroutine("MoveToPosition", BeatPosition);
    }

    private IEnumerator MoveToPosition(Vector3 target){
        Vector3 cur = transform.position;
        Vector3 initial = cur;
        
        float timer = 0;

        while(cur != target){
            cur = Vector3.Lerp(initial, target, timer / TimeToBeat);
            timer += Time.deltaTime;

            transform.position = cur;
            yield return null;
        }

        isBeat = false;
    }
}