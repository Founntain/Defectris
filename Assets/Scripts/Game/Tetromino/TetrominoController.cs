using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TetrominoInputActions;

public class TetrominoController : MonoBehaviour, IPlayerActions
{
    public GameObject ActiveTetrominoObj;
    // public Tetromino ActiveTetromino;
    public TetrominoInputActions controls;

    public PieceType CurrentHoldPieceType = PieceType.Unkown;
    public GameObject CurrentHoldPieceGameObject;

    public float ARR;
    public float DAS;
    public float SDF;
    
    public float ARRTime;
    public float DASTime;
    public float SDFTime;

    public List<GameObject> SevenBag;

    float TimeElapsed = 0f;

    private void OnEnable() {
        if(controls == null){
            controls = new TetrominoInputActions();

            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }

    private void OnDisable() {
        controls.Player.Disable();
    }

    void Start()
    {
        SevenBag = GameLogic.Generate7Bag();
        SevenBag.AddRange(GameLogic.Generate7Bag());

        SpawnNewTetromino();
    }

    void FixedUpdate()
    {
        ActiveTetrominoObj = GameObject.FindGameObjectWithTag("ActiveTetromino");

        TimeElapsed += Time.deltaTime;

        if(controls.Player.Move.phase == InputActionPhase.Started){
            DASTime += Time.deltaTime;

            if(DASTime >= DAS / 1000){
                ARRTime += Time.deltaTime;

                if(ARRTime >= ARR / 1000){
                    ARRTime = 0;
                    
                    MovePieceInDirection(controls.Player.Move.ReadValue<Vector2>());
                }
            }
        }else{
            DASTime = 0;
        }

        if(controls.Player.MoveDown.phase == InputActionPhase.Started){
            SDFTime += Time.deltaTime;

            if(SDFTime >= SDF / 1000){
                SDFTime = 0;

                MovePieceDown();
            }
        }

        if(TimeElapsed >= 1f){
            TimeElapsed = 0f;

            var nextMoveValid = IsNextMoveValid(Vector3.down);

            if(!nextMoveValid){
                // PlaceTetromino();

                return;
            }

            // if(nextMoveValid)
                //ActiveTetrominoObj.transform.position += Vector3.down;
        }
    }

    private void PlaceTetromino(){
        var gameMatrix = GetComponentInParent<GameMatrix>();
        var tetromino = ActiveTetrominoObj.GetComponent<Tetromino>();


        GameLogic.PlaceTetrominoInMatrix(tetromino.GetMinos(), gameMatrix);

        tetromino.RemoveGhostPiece();

        var is3CornerRotation = tetromino.Is3CornerRotation;
        var pieceType = tetromino.PieceType;

        ActiveTetrominoObj.tag = "Untagged";

        SpawnNewTetromino();

        gameMatrix.ClearLines(is3CornerRotation, pieceType);
    }

    private void SpawnNewTetromino()
    {
        var newTetromino = SevenBag[0];

        SevenBag.RemoveAt(0);

        var obj = Instantiate(newTetromino, new Vector3(5, 20, 0), Quaternion.identity);

        obj.tag = "ActiveTetromino";

        if(SevenBag.Count == 7)
            SevenBag.AddRange(GameLogic.Generate7Bag());
    }

    private bool RotateClockwise(){
        // if(GameLogic.IsMinoOutOfBoundsNextRotation(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos())) return;
        // if(GameLogic.IsCellInRotationOccupied(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), GetComponentInParent<GameMatrix>())) return;

        return ActiveTetrominoObj.GetComponent<Tetromino>().RotatePiece();
    }

    private bool RotateCounterClockwise(){
        // if(GameLogic.IsMinoOutOfBoundsNextRotation(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), false)) return;
        // if(GameLogic.IsCellInRotationOccupied(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), GetComponentInParent<GameMatrix>(), false)) return;

       return ActiveTetrominoObj.GetComponent<Tetromino>().RotatePiece(false);
    }

    public bool IsNextMoveValid(Vector3 dirVec){
        if(ActiveTetrominoObj.GetComponent<Tetromino>() == null) return false;
        
        var outOfBounds = GameLogic.AreMinosOutOfBounds(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos());
        var outOfBoundsNextTurn = GameLogic.AreMinosOutOfBoundsNextMove(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), dirVec);
        var isCellOccupiedInDirection = GameLogic.AreCellsInDirectionOccupied(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), GetComponentInParent<GameMatrix>(), dirVec);

        return !(outOfBounds || outOfBoundsNextTurn || isCellOccupiedInDirection);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        try{
            if(context.phase != InputActionPhase.Performed) return;

            MovePieceInDirection(context.ReadValue<Vector2>());
        }catch(Exception ){ }
    }

     public void OnMoveDown(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed) return;

        MovePieceDown();
    }

    public void OnHardDrop(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed) return;

        var pos = ActiveTetrominoObj.transform.position;

        for(var y = pos.y; y >= 0; y--){
            var x = IsNextMoveValid(Vector3.down);

            GetComponentInParent<ScoreManager>().AddScore(2);

            if(!x){
                PlaceTetromino();

                var soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager").GetComponent<SoundEffectManager>();
                soundEffectManager.PlayHardDropSound();

                return;
            }
            
            ActiveTetrominoObj.transform.position += Vector3.down;
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed) return;
            
        var result = RotateClockwise();

        if(!result) return;

        var soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager").GetComponent<SoundEffectManager>();
        soundEffectManager.PlayRotateSound();
    }

    public void OnRotateCounterClockwise(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed) return;
            
        var result = RotateCounterClockwise();

        if(!result) return;

        var soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager").GetComponent<SoundEffectManager>();
        soundEffectManager.PlayRotateSound();
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed) return;

        if(CurrentHoldPieceType == PieceType.Unkown){
            CurrentHoldPieceType = ActiveTetrominoObj.GetComponent<Tetromino>().PieceType;

            CurrentHoldPieceGameObject = Instantiate(GetComponent<DummyGenerator>().GetTetrominoOfType(CurrentHoldPieceType), new Vector3(-3, 17, 0), Quaternion.identity);

            ActiveTetrominoObj.GetComponent<Tetromino>().RemoveGhostPiece();
            Destroy(ActiveTetrominoObj);

            SpawnNewTetromino();

            return;
        }

        var newPiece = Instantiate(GetComponent<TetrominoGenerator>().GetTetrominoOfType(CurrentHoldPieceType), new Vector3(5, 20, 0), Quaternion.identity);

        ActiveTetrominoObj.GetComponent<Tetromino>().RemoveGhostPiece();
        Destroy(ActiveTetrominoObj);

        Destroy(CurrentHoldPieceGameObject);

        CurrentHoldPieceType = ActiveTetrominoObj.GetComponent<Tetromino>().PieceType;

        CurrentHoldPieceGameObject = Instantiate(GetComponent<DummyGenerator>().GetTetrominoOfType(CurrentHoldPieceType), new Vector3(-3, 17, 0), Quaternion.identity);

        newPiece.tag = "ActiveTetromino";
    }   

    private void MovePieceInDirection(Vector3 dir){
        var dirVec = new Vector3(dir.x, 0, 0);

        if(GameLogic.AreMinosOutOfBoundsNextMove(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), dirVec)) return;
        if(GameLogic.AreCellsInDirectionOccupied(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), GetComponentInParent<GameMatrix>(), dirVec)) return;

        ActiveTetrominoObj.transform.position += dirVec;

        var soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager").GetComponent<SoundEffectManager>();
        soundEffectManager.PlayMoveSound();
    }

    private void MovePieceDown(){
        if(GameLogic.AreMinosOutOfBoundsNextMove(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), Vector3.down)) return;
        if(GameLogic.AreCellsInDirectionOccupied(ActiveTetrominoObj.GetComponent<Tetromino>().GetMinos(), GetComponentInParent<GameMatrix>(), Vector3.down)) {
            Debug.Log("Cell in direction is occupied");
            return;
        }

        ActiveTetrominoObj.transform.position += Vector3.down;

        GetComponentInParent<ScoreManager>().AddScore(1);
    }
}
