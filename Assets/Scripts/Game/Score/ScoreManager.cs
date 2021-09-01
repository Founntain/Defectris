using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    
    public int Level;
    public int Score;
    public int LinesCleared;

    public void Update(){
        ScoreText.text = $"Score: {Score}\nLevel: {Level}";
    }

    public void AddScore(int score){
        Score += score;
    }

    public void AddLevel(int level){
        Level += level;
    }

    public void SetLevel(int level){
        Level = level;
    }

    public void AddLines(int lines){
        LinesCleared += lines;

        var level = 1 + (Mathf.FloorToInt(LinesCleared / 10));

        SetLevel(level);
    }

}
