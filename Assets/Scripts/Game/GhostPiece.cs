using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPiece : MonoBehaviour
{
    public PieceType PieceType;
    public GameObject ParentPiece;

    void Update(){
        if(ParentPiece == null) return;

        var tetrominoController = GameObject.FindGameObjectWithTag("TetrominoController").GetComponent<TetrominoController>();

        transform.position = new Vector3(ParentPiece.transform.position.x, ParentPiece.transform.position.y, 0);

        while(IsMoveDownValid(Vector3.down)){
            transform.position += Vector3.down;
        }
    }

    public Mino[] GetMinos(){
        return GetComponentsInChildren<Mino>();
    }

    public bool IsMoveDownValid(Vector3 dir){

        var gameMatrix = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMatrix>();

        foreach(var mino in GetMinos()){
            if(GameLogic.IsMinoOutOfBoundsNextMove(mino, dir) || GameLogic.IsCellInDirectionOccupied(mino, gameMatrix, dir))
                return false;                
        }

        return true;
    }

    public void RotateGhostPiece(bool clockwise = true){
        var rotMatrix = clockwise ? GameLogic.ClockwiseRotationMatrix : GameLogic.CounterClockwiseRotationMatrix;

        foreach(var mino in GetMinos()){
            var minoPos = mino.transform.localPosition;

            var newPos = rotMatrix.MultiplyVector(minoPos);

            mino.transform.localPosition = newPos;
        }
    }
}
