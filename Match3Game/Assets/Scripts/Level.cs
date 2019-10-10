using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum LevelType
    {
        TIMER,
        OBSTACLE,
        MOVES,
    };

    public Grid grid;
    public HUD hud;

    public int socore1Star;
    public int socore2Star;
    public int socore3Star;

    protected LevelType type;
    public LevelType Type
    {
        get { return type; }
    }

    protected int currentScore;

    protected bool didWin;

    // Start is called before the first frame update
    void Start()
    {
        hud.SetScore(currentScore);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void GameWin()
    {
        grid.GameOver();
        didWin = true;
        StartCoroutine(WaitForGirdFill());
    }

    public virtual void GameLose()
    {
        grid.GameOver();
        didWin = false;
        StartCoroutine(WaitForGirdFill());
    }

    public virtual void OnMove()
    {
        // Debug.Log("You Moved");
    }

    public virtual void OnPieceCleared(GamePiece piece)
    {
        // 更新分数
        currentScore += piece.score;
        // Debug.Log("Score: " + currentScore);
        hud.SetScore(currentScore);
    }

    protected virtual IEnumerator WaitForGirdFill()
    {
        while (grid.IsFilling)
        {
            yield return 0;
        }
        if (didWin)
        {
            hud.OnGameWin(currentScore);
        }
        else
        {
            hud.OnGameLose();
        }
    }
}
