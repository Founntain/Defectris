using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PerlinNoise : MonoBehaviour
{
    public int Width = 512;
    public int Heigth = 512;

    public float Scale = 20f;

    public float XOffset = 0f;
    public float YOffset = 0f;

    public Color Color;

    // Update is called once per frame
    void Update()
    {
        var renderer = GetComponent<Renderer>();

        renderer.sharedMaterial.mainTexture = GenerateTexture(); 
    }

    private Texture2D GenerateTexture(){
        var texture = new Texture2D(Width, Heigth);

        for(var x = 0; x < Width; x++){
            for(var y = 0; y < Heigth; y++){
                var color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        return texture;
    }

    private Color CalculateColor(int x, int y){
        var xCoord = (float) x / Width * Scale + XOffset;
        var yCoord = (float) y / Heigth * Scale + YOffset;

        var noise = Mathf.PerlinNoise(xCoord, yCoord);
        var otherNoise = Mathf.PerlinNoise(xCoord * 0.5f, yCoord * 0.5f);

        var sample = noise + otherNoise;

        return new Color(Color.r * sample,Color.g * sample, Color.b * sample);
    }
}
