using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public PieceType PieceType;
    public GameObject GhostPiece;
    public int RotationIndex;
    public bool Is3CornerRotation;

    public void Start(){
        GenerateGhostPiece();
    }

    public void FixedUpdate() {
        var minos = GetComponentsInChildren<Mino>();

        if(minos == null || minos.Length == 0){
            Destroy(gameObject);
        }
    }

    public void GenerateGhostPiece(){
        GhostPiece = Instantiate(GhostPiece, transform.position, transform.rotation);
        GhostPiece.GetComponent<GhostPiece>().ParentPiece = gameObject;
    }

    internal void RemoveGhostPiece()
    {
        Destroy(GhostPiece);
    }

    public Mino[] GetMinos(){
        return GetComponentsInChildren<Mino>();
    }

    public bool RotatePiece(bool clockwise = true){
        var rotMatrix = clockwise ? GameLogic.ClockwiseRotationMatrix : GameLogic.CounterClockwiseRotationMatrix;
        var gameManager = GameObject.FindGameObjectWithTag("GameManager");

        RotateMinos(clockwise);

        for(var i = 0; i < GetMinos().Length; i++)
        {
            var mino = GetMinos()[i];
        }

        if(GameLogic.AreMinosValidOnPosition(GetMinos(), gameManager.GetComponent<GameMatrix>())){
            UpdateRotationIndex(clockwise);

            if(PieceType == PieceType.T){
                var result = ThreeCornerCheck(gameManager.GetComponent<GameMatrix>(), transform.position);

                if(result){
                    Is3CornerRotation = true;
                    PlayTSpinRotationSound();
                }else{
                    Is3CornerRotation = false;
                }
            }
            
            GhostPiece.GetComponent<GhostPiece>().RotateGhostPiece(clockwise);
            return true;
        }
        
        Vector2Int[,] kickTable = null;

        switch(PieceType){
            case PieceType.I:
                kickTable = KickTables.GetIKickTable();
                break;
            default:
                kickTable = KickTables.GetJLSTZKickTable();
                break;
        }

        for(var test = 1; test < 5; test++){
            var futureRotationIndex = GetFutureRotationIndex(clockwise);

            var value = Vector2Int.zero;

            if(clockwise){
                value = kickTable[test, RotationIndex];
            }else{
                value = kickTable[test, futureRotationIndex];
            }

            var vectorValue = new Vector3(value.x, value.y);

            var oldValue = vectorValue;

            if(!clockwise)  
                vectorValue = Vector3.Scale(vectorValue, new Vector3(-1, -1));

            transform.position += vectorValue;

            var isValid = true;

            foreach(var mino in GetMinos()){
                var pos = mino.transform.position;
                if(GameLogic.IsMinoOutOfBounds(mino.transform.position)){
                    isValid = false;
                    break;
                }

                if(GameLogic.IsCellOnPositionOccupied(gameManager.GetComponent<GameMatrix>(), mino.transform.position)){
                    isValid = false;
                    break;
                }
            }

            if(!isValid){
                Debug.Log("HUREENSOHN 2");
                transform.position -= vectorValue;
            }
            else{
                break;
            }

            if(test == kickTable.Length - 1){
                Debug.Log("HUREENSOHN 3");

                RotateMinos(!clockwise);
                
                return false;
            }
        }

        if(!GameLogic.AreMinosValidOnPosition(GetMinos(), gameManager.GetComponent<GameMatrix>())){
            RotateMinos(!clockwise);
            return false;
        }
        
        UpdateRotationIndex(clockwise);

        GhostPiece.GetComponent<GhostPiece>().RotateGhostPiece(clockwise);

        if(PieceType == PieceType.T){
            var result = ThreeCornerCheck(gameManager.GetComponent<GameMatrix>(), transform.position);

            Is3CornerRotation = true;

            if(result){
                PlayTSpinRotationSound();
            }else{
                Is3CornerRotation = false;
            }
        }

        return true;
    }

    private bool ThreeCornerCheck(GameMatrix gameMatrix, Vector3 position)
    {
        var downPos = position + Vector3.down;
        var upPos = position + Vector3.up;

        if(downPos.y < 0 || position.x <= 0 || position.x > 10) 
            return false;

        if(GameLogic.IsPositionInMatrixOutOfBounds(upPos) || GameLogic.IsPositionInMatrixOutOfBounds(downPos))
            return false;

        if(GameLogic.IsPositionInMatrixOutOfBounds(upPos + new Vector3(-2, 0)) || GameLogic.IsPositionInMatrixOutOfBounds(downPos + new Vector3(-2, 0)))
            return false;

        var corner1 = gameMatrix.Matrix[(int) upPos.x - 2, (int) upPos.y];
        var corner2 = gameMatrix.Matrix[(int) upPos.x, (int) upPos.y];
        var corner3 = gameMatrix.Matrix[(int) downPos.x - 2, (int) downPos.y];
        var corner4 = gameMatrix.Matrix[(int) downPos.x, (int) downPos.y];

        var cornersOccupied = 0;

        cornersOccupied += corner1 != null ? 1 : 0;
        cornersOccupied += corner2 != null ? 1 : 0;
        cornersOccupied += corner3 != null ? 1 : 0;
        cornersOccupied += corner4 != null ? 1 : 0;

        if(cornersOccupied < 3) return false;

        return true;
    }

    private void ToggleGhostPieceVisibility()
    {
        foreach(var mino in GhostPiece.GetComponent<GhostPiece>().GetMinos()){
            var meshRenderer = mino.GetComponent<MeshRenderer>();

            meshRenderer.enabled = meshRenderer.enabled ? false : true;
        }
    }

    private void UpdateRotationIndex(bool clockwise){
        if(RotationIndex == 0 && !clockwise){
            RotationIndex = 3;
        }else if(RotationIndex == 3 && clockwise){
            RotationIndex = 0;
        }else{
            RotationIndex += clockwise ? 1 : -1;
        }
    }

    private int GetFutureRotationIndex(bool clockwise){
        if(RotationIndex == 0 && !clockwise){
            return 3;
        }else if(RotationIndex == 3 && clockwise){
            return 0;
        }else{
            return clockwise ? RotationIndex + 1 : RotationIndex - 1;
        }
    }

    private void PlayTSpinRotationSound(){
        var soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager");
        var soundEffectManagerComponent = soundEffectManager.GetComponent<SoundEffectManager>();
        
        soundEffectManagerComponent.PlayTSpinSound();
    
    }

    private void RotateMinos(bool clockwise = true){
        var rotMatrix = clockwise ? GameLogic.ClockwiseRotationMatrix : GameLogic.CounterClockwiseRotationMatrix;

        foreach(var mino in GetMinos()){
            var minoPos = mino.transform.localPosition;

            var newPos = rotMatrix.MultiplyVector(minoPos);

            mino.transform.localPosition = newPos;
        }
    }
}
