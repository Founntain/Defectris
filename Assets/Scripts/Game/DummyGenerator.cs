using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGenerator : MonoBehaviour
{
    public GameObject I;
    public GameObject J;
    public GameObject L;
    public GameObject O;
    public GameObject S;
    public GameObject T;
    public GameObject Z;

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
