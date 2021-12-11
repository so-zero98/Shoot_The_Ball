using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

    public static float arcadeStage = 1;
    public static float arcadeGoalScore = 0;
    public static float timeAttackStage = 1;
    public static float timeAttackScore=0; 

    void Awake()
    {
        dataManager = this;
    }

    public static IEnumerator arcadeData(float stage)
    {
        if (stage == 1)
        {
            arcadeStage = 1;
            arcadeGoalScore = 10;
        }
        else if (stage == 2)
        {
            arcadeStage = 2;
            arcadeGoalScore = 13;
        }
        else if (stage == 3)
        {
            arcadeStage = 3;
            arcadeGoalScore = 16;
        }
        else if (stage == 4)
        {
            arcadeStage = 4;
            arcadeGoalScore = 10;
        }
        else if (stage == 5)
        {
            arcadeStage = 5;
            arcadeGoalScore = 13;
        }
        else if (stage == 6)
        {
            arcadeStage = 6;
            arcadeGoalScore = 10;
        }
        else if (stage == 7)
        {
            arcadeStage = 7;
            arcadeGoalScore = 13;
        }
        else if (stage == 8)
        {
            arcadeStage = 8;
            arcadeGoalScore = 10;
        }
        else if (stage == 9)
        {
            arcadeStage = 9;
            arcadeGoalScore = 13;
        }
        else if (stage == 10)
        {
            arcadeStage = 10;
            arcadeGoalScore = 10;
        }
        else if (stage == 11)
        {
            arcadeStage = 11;
            arcadeGoalScore = 13;
        }
        else if (stage == 12)
        {
            arcadeStage = 12;
            arcadeGoalScore = 10;
        }
        else if (stage == 13)
        {
            arcadeStage = 13;
            arcadeGoalScore = 13;
        }
        else if (stage == 14)
        {
            arcadeStage = 14;
            arcadeGoalScore = 10;
        }
        else if (stage == 15)
        {
            arcadeStage = 15;
            arcadeGoalScore = 13;
        }

        yield return null;
    }
}
