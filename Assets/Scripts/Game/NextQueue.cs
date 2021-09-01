using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextQueue : MonoBehaviour
{
    public GameObject Piece1;
    public GameObject Piece2;
    public GameObject Piece3;
    public GameObject Piece4;
    public GameObject Piece5;

    public TetrominoController TetrominoController;
    public DummyGenerator DummyGenerator;

    void Start(){
        TetrominoController = GameObject.FindGameObjectWithTag("TetrominoController").GetComponent<TetrominoController>();
        DummyGenerator = TetrominoController.GetComponent<DummyGenerator>();
    }

   // Update is called once per frame
    void Update()
    {
        var sevenbag = TetrominoController.SevenBag.Take(5).ToArray();

        Destroy(Piece1);
        Destroy(Piece2);
        Destroy(Piece3);
        Destroy(Piece4);
        Destroy(Piece5);

        if(sevenbag.Length == 0) return;

        Piece1 = Instantiate(DummyGenerator.GetTetrominoOfType(sevenbag[0].GetComponent<Tetromino>().PieceType), new Vector3(14, 17, 0), Quaternion.identity);
        Piece2 = Instantiate(DummyGenerator.GetTetrominoOfType(sevenbag[1].GetComponent<Tetromino>().PieceType), new Vector3(14, 14, 0), Quaternion.identity);
        Piece3 = Instantiate(DummyGenerator.GetTetrominoOfType(sevenbag[2].GetComponent<Tetromino>().PieceType), new Vector3(14, 11, 0), Quaternion.identity);
        Piece4 = Instantiate(DummyGenerator.GetTetrominoOfType(sevenbag[3].GetComponent<Tetromino>().PieceType), new Vector3(14, 8, 0), Quaternion.identity);
        Piece5 = Instantiate(DummyGenerator.GetTetrominoOfType(sevenbag[4].GetComponent<Tetromino>().PieceType), new Vector3(14, 5, 0), Quaternion.identity);
        
        Piece2.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f); 
        Piece3.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f); 
        Piece4.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f); 
        Piece5.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f); 
    }
}
