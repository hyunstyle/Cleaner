using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public int score;
    public Text scoreText;

    private static ScoreBoard instance;
    public static ScoreBoard Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<ScoreBoard>();
            }
            return ScoreBoard.instance;
        }
    }

}
