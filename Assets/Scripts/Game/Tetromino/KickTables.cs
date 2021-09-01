using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTables : MonoBehaviour
{
    //[test, rotationIndex]
    public static Vector2Int[,] GetJLSTZKickTable(){
        var kickTable = new Vector2Int[5, 4];

        kickTable[0, 0] = Vector2Int.zero;
        kickTable[0, 1] = Vector2Int.zero;
        kickTable[0, 2] = Vector2Int.zero;
        kickTable[0, 3] = Vector2Int.zero;

        kickTable[1, 0] = new Vector2Int(-1, 0);
        kickTable[1, 1] = new Vector2Int(1, 0);
        kickTable[1, 2] = new Vector2Int(1, 0);
        kickTable[1, 3] = new Vector2Int(-1, 0);
        
        kickTable[2, 0] = new Vector2Int(-1, 1);
        kickTable[2, 1] = new Vector2Int(1, -1);
        kickTable[2, 2] = new Vector2Int(1, 1);
        kickTable[2, 3] = new Vector2Int(-1, -1);

        kickTable[3, 0] = new Vector2Int(0, -2);
        kickTable[3, 1] = new Vector2Int(0, 2);
        kickTable[3, 2] = new Vector2Int(0, -2);
        kickTable[3, 3] = new Vector2Int(0, 2);

        kickTable[4, 0] = new Vector2Int(-1, -2);
        kickTable[4, 1] = new Vector2Int(1, 2);
        kickTable[4, 2] = new Vector2Int(1, -2);
        kickTable[4, 3] = new Vector2Int(-1, 2);

        return kickTable;
    }

    public static Vector2Int[,] GetIKickTable(){
        var kickTable = new Vector2Int[5, 4];

        kickTable[0, 0] = Vector2Int.zero;
        kickTable[0, 1] = Vector2Int.zero;
        kickTable[0, 2] = Vector2Int.zero;
        kickTable[0, 3] = Vector2Int.zero;

        kickTable[1, 0] = new Vector2Int(-2, 0);
        kickTable[1, 1] = new Vector2Int(-1, 0);
        kickTable[1, 2] = new Vector2Int(2, 0);
        kickTable[1, 3] = new Vector2Int(1, 0);
        
        kickTable[2, 0] = new Vector2Int(1, 0);
        kickTable[2, 1] = new Vector2Int(2, 0);
        kickTable[2, 2] = new Vector2Int(-1, 0);
        kickTable[2, 3] = new Vector2Int(-2, 0);

        kickTable[3, 0] = new Vector2Int(-2, -1);
        kickTable[3, 1] = new Vector2Int(-1, 2);
        kickTable[3, 2] = new Vector2Int(2, 1);
        kickTable[3, 3] = new Vector2Int(1, -2);

        kickTable[4, 0] = new Vector2Int(1, 2);
        kickTable[4, 1] = new Vector2Int(2, -1);
        kickTable[4, 2] = new Vector2Int(-1, -2);
        kickTable[4, 3] = new Vector2Int(-2, 1);

        return kickTable;
    }
}
