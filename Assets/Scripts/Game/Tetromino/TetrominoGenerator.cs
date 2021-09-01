using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoGenerator : MonoBehaviour
{
    public GameObject I;
    public GameObject J;
    public GameObject L;
    public GameObject O;
    public GameObject S;
    public GameObject T;
    public GameObject Z;

    public GameObject GetRandomTetromino(){
        var x = Random.Range(0, 7);

        switch(x){
            case 0: return I;
            case 1: return J;
            case 2: return L;
            case 3: return O;
            case 4: return S;
            case 5: return T;
            case 6: return Z;
            default: return GetRandomTetromino();
        }
    }

    public GameObject GetTetrominoOfType(PieceType pieceType){
        switch(pieceType){
            case PieceType.I: return I;
            case PieceType.J: return J;
            case PieceType.L: return L;
            case PieceType.O: return O;
            case PieceType.S: return S;
            case PieceType.T: return T;
            case PieceType.Z: return Z;
            default: return null;
        }
    }
}
