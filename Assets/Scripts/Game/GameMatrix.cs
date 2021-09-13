using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMatrix : MonoBehaviour
{
    public int MatrixWidth = 10;
    public int MatrixHeigth = 24;
    public TextMeshProUGUI Text;

    //x, y
    public GameObject[,] Matrix;

    void Start()
    {
        this.Matrix = new GameObject[MatrixWidth, MatrixHeigth];
        Debug.Log($"Matrix size:{MatrixWidth}x{MatrixHeigth}");
    }

    void Update()
    {
        // this.Text.text = "Test";
        //DrawMatrix();
    }

    private void DrawMatrix(){
        var s = "Matrix\n\n";

        for(var y = MatrixHeigth - 1; y >= 0; y--){
            for(var x = 0; x < MatrixWidth; x++){
                s += $"{(Matrix[x, y] == null ? 0 : 1)} ";
            }

            s += "\n";
        }

        this.Text.text = s;
    }

    public void ClearLines(bool is3CornerRotation, PieceType pieceType){
        var rowsCleared = 0;

        for(var y = 0; y < MatrixHeigth; y++){
            if(IsRowFullAt(y)){
                DeleteRow(y);

                rowsCleared++;

                MoveAllRowsDown(y + 1);

                y--;
            }
        }

        if(rowsCleared == 0) {
            GameLogic.Combo = -1;
            return;
        }

        var score = 0;
        if(is3CornerRotation && pieceType == PieceType.T){
            switch(rowsCleared){
                case 1: 
                    score = 800;
                    break;
                case 2: 
                    score = 1200;
                    break;
                case 3: 
                    score = 1600;
                    break;
            }
        }else{
            switch(rowsCleared){
                case 1: 
                    score = 100;
                    break;
                case 2: 
                    score = 300;
                    break;
                case 3: 
                    score = 500;
                    break;
                case 4: 
                    score = 800;
                    break;
                default:
                    score = 0;
                    break;
            }
        }

        var sm = GetComponent<ScoreManager>();

        var soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager").GetComponent<SoundEffectManager>();

        GameLogic.Combo++;

        if(GameLogic.Combo > 0){
            sm.AddScore(50 * GameLogic.Combo * sm.Level);
        }

        if(is3CornerRotation && pieceType == PieceType.T)
            soundEffectManager.PlayTSpinLineClearSound();
        else
            soundEffectManager.PlayLineClearSound();

        sm.AddScore(score * sm.Level);
        sm.AddLines(rowsCleared);
    }

    private void DeleteRow(int y){
        for(var x = 0; x < MatrixWidth; x++){
            Destroy(Matrix[x, y]);

            Matrix[x, y] = null;
        }
    }

    private void MoveAllRowsDown(int y){
        for(var i = y; i < MatrixHeigth; i++){
            MoveRowDown(i);
        }
    }

    private void MoveRowDown(int y){
        for(var x = 0; x < MatrixWidth; x++){
            if(Matrix[x, y] != null){
                Matrix[x, y - 1] = Matrix[x, y];
                
                Matrix[x, y] = null;

                Matrix[x, y - 1].transform.position += Vector3.down;
            }
        }
    }

    private bool IsRowFullAt(int y){
        for(var x = 0; x < MatrixWidth; x++){
            if(Matrix[x, y] == null) return false;
        }

        return true;
    }

}
