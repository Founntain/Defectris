using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameLogic
{
    public static Matrix4x4 ClockwiseRotationMatrix = new Matrix4x4(
        new Vector4(0, -1, 0 ,0),
        new Vector4(1, 0, 0 ,0),
        new Vector4(0, 0, 0 ,0),
        new Vector4(0, 0, 0 ,0));

    public static Matrix4x4 CounterClockwiseRotationMatrix = new Matrix4x4(
        new Vector4(0, 1, 0 ,0),
        new Vector4(-1, 0, 0 ,0),
        new Vector4(0, 0, 0 ,0),
        new Vector4(0, 0, 0 ,0));

    public static int Combo;

    public static bool AreMinosOutOfBounds(Mino[] minos){
        foreach(var mino in minos){
            var transform = mino.transform;

            var pos = transform.position;

            if(pos.x <= 0 || pos.y < 0 || pos.x > 10)
                return true;
        }

        return false;
    }

    public static bool IsPositionOutOfBounds(Vector3 pos){
        if(pos.x <= 0 || pos.y < 0 || pos.x > 10)
            return true;

        return false;
    }

    public static bool IsPositionInMatrixOutOfBounds(Vector3 pos){
        if(pos.x < 0 || pos.y < 0 || pos.x > 9)
            return true;

        return false;
    }

    public static bool AreMinosOutOfBoundsNextMove(Mino[] minos, Vector3 direction){
        foreach(var mino in minos){
            var pos = mino.transform.position;

            pos += direction;

            if(pos.x <= 0 || pos.y < 0 || pos.x > 10)
                return true;
        }

        return false;
    }

    public static bool IsMinoOutOfBoundsNextMove(Mino mino, Vector3 direction){

        var pos = mino.transform.position;

        pos += direction;

        if(pos.x <= 0 || pos.y < 0 || pos.x > 10)
            return true;

        return false;
    }

    public static bool IsMinoOutOfBounds(Vector3 pos){
        return pos.x <= 0 || pos.y < 0 || pos.x > 10;
    }

    public static bool IsMinoOutOfBoundsNextRotation(Mino[] minos, bool clockwise = true){
        var rotMatrix = clockwise ? ClockwiseRotationMatrix : CounterClockwiseRotationMatrix;

        foreach(var mino in minos){
            var minoPos = mino.transform.localPosition;

            var newPos = rotMatrix.MultiplyVector(minoPos);

            mino.transform.localPosition = newPos;

            if(IsMinoOutOfBounds(mino.transform.position)){
                mino.transform.localPosition = minoPos;
                return true;
            }

            mino.transform.localPosition = minoPos;
        }

        return false;
    }

    public static bool IsCellInRotationOccupied(Mino[] minos, GameMatrix gameMatrix, bool clockwise = true){
        var rotMatrix = clockwise ? ClockwiseRotationMatrix : CounterClockwiseRotationMatrix;

        foreach(var mino in minos){
            var minoPos = mino.transform.localPosition;
            var newPos = rotMatrix.MultiplyVector(minoPos);

            mino.transform.localPosition = newPos;

            var result = IsCellOnPositionOccupied(gameMatrix, mino.transform.position);

            mino.transform.localPosition = minoPos;

            if(result) return true;
        }

        return false;
    }

    public static bool AreMinosValidOnPosition(Mino[] minos, GameMatrix gameMatrix){
        foreach(var mino in minos){
            if(IsMinoOutOfBounds(mino.transform.position))
                return false;
            
            var pos = mino.transform.position;

            var x = (int) pos.x - 1;
            var y = (int) pos.y;

            if(gameMatrix.Matrix[x, y] != null)
                return false;
        }

        return true;
    }

    public static bool AreCellsInDirectionOccupied(Mino[] minos, GameMatrix gameMatrix, Vector3 direction){
        foreach(var mino in minos){
            if(GameLogic.AreMinosOutOfBoundsNextMove(minos, direction)) return true;

            var pos = mino.transform.position + direction;

            var x = (int) pos.x - 1;
            var y = (int) pos.y;

            if(gameMatrix.Matrix[x, y] != null)
                return true;
        }

        return false;
    }

    public static bool IsCellInDirectionOccupied(Mino mino, GameMatrix gameMatrix, Vector3 direction){
        if(GameLogic.IsMinoOutOfBoundsNextMove(mino, direction)) return false;

        var pos = mino.transform.position + direction;

        var x = (int) pos.x - 1;
        var y = (int) pos.y;

        if(gameMatrix.Matrix[x, y] != null)
            return true;
        

        return false;
    }

    public static bool IsCellOnPositionOccupied(GameMatrix gameMatrix, Vector3 pos){
        if(IsMinoOutOfBounds(new Vector3(pos.x, pos.y, pos.z))) return true;

        var x = (int) pos.x - 1;
        var y = (int) pos.y;

        return gameMatrix.Matrix[x, y] != null;
    }

    public static void PlaceTetrominoInMatrix(Mino[] minos, GameMatrix gameMatrix){
        foreach(var mino in minos){
            var pos = mino.transform.position;

            var x = (int) pos.x - 1;
            var y = (int) pos.y;

            gameMatrix.Matrix[x, y] = mino.gameObject;
        }
    }

    public static List<GameObject> Generate7Bag(){
        var bag = new List<GameObject>();
        var tetrominoGenerator = GameObject.FindGameObjectWithTag("TetrominoController").GetComponent<TetrominoGenerator>();

        while(bag.Count != 7){
            var tetrominoObj = tetrominoGenerator.GetRandomTetromino();

            var tetromino = tetrominoObj.GetComponent<Tetromino>();

            if(!bag.Any(x => x.GetComponent<Tetromino>().PieceType == tetromino.PieceType))
                bag.Add(tetrominoObj);
        }

        return bag;
    }
}
