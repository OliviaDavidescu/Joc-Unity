using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// script realizat de Anna si Adela

public class optionScreen : MonoBehaviour
{



    public List<LevelItem> levels = new List<LevelItem>();
    private int selectedLevel;
    public TMP_Text levelLabel;

    public List<string> balls = new List<string>();
    private int selectedBall;
    public TMP_Text ballLabel;

    public List<string> mazes = new List<string>();
    private int selectedMaze;
    public TMP_Text mazeLabel;

    public List<string> Ais = new List<string>();
    private int selectedAi;
    public TMP_Text aiLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelLeft()
    {
        selectedLevel--;
        if (selectedLevel < 0)
        {
            selectedLevel = 0;
        }
        UpdateLevelLabel();
    }

    public void LevelRight()
    {
        selectedLevel++;
        if (selectedLevel > levels.Count - 1)
        {
            selectedLevel = levels.Count - 1;
        }
        UpdateLevelLabel();
    }

    public void UpdateLevelLabel()
    {
        levelLabel.text = levels[selectedLevel].difficulty.ToString();
    }



    public void AiLeft()
    {
        selectedAi--;
        if (selectedAi < 0)
        {
            selectedAi = 0;
        }
        UpdateAiLabel();
        if (selectedAi==1)
        {
            MazeGenerator.AI = true;
        }
        else
        {
            MazeGenerator.AI = false;
        }
    }

    public void AiRight()
    {
        selectedAi++;
        if (selectedAi > Ais.Count - 1)
        {
            selectedAi = Ais.Count - 1;
        }
        UpdateAiLabel();
        if (selectedAi == 1)
        {
            MazeGenerator.AI = true;
        }
        else
        {
            MazeGenerator.AI = false;
        }
    }

    public void UpdateAiLabel()
    {
        aiLabel.text = Ais[selectedAi];
    }

    public void BallLeft()
    {
        selectedBall--;
        if (selectedBall < 0)
        {
            selectedBall = 0;
        }
        UpdateBallLabel();
        SphereGenerator.sphereMatIndex = selectedBall; //adugat de Radu
    }

    public void BallRight()
    {
        selectedBall++;
        if (selectedBall > balls.Count - 1)
        {
            selectedBall = balls.Count - 1;
        }
        UpdateBallLabel();
        SphereGenerator.sphereMatIndex = selectedBall; //adaugat de Radu
    }

    public void UpdateBallLabel()
    {
        ballLabel.text = balls[selectedBall];
    }


    public void MazeLeft()
    {
        selectedMaze--;
        if (selectedMaze < 0)
        {
            selectedMaze = 0;
        }
        UpdateMazeLabel();
        NodeMatSelector.mazeFloorMatIndex = selectedMaze;
    }

    public void MazeRight()
    {
        selectedMaze++;
        if (selectedMaze > mazes.Count - 1)
        {
            selectedMaze = mazes.Count - 1;
        }
        UpdateMazeLabel();
        NodeMatSelector.mazeFloorMatIndex = selectedMaze;
    }

    public void UpdateMazeLabel()
    {
        mazeLabel.text = mazes[selectedMaze];
    }


}

[System.Serializable]

public class LevelItem 
{
    public string difficulty;

}

