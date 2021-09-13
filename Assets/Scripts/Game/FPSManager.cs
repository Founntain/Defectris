using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    public TextMeshProUGUI FpsTextMesh;
    private float deltaTime;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        var fps = 1.0f / deltaTime;
        FpsTextMesh.text = $"FPS: {Mathf.Ceil(fps).ToString()}";
    }
}
