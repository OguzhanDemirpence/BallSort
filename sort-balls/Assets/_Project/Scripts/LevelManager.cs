using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private MaterialManager materialManager;
    [SerializeField] private UIControl uIControl;
    [Space]
    [SerializeField] private Ball ball;
    [SerializeField] private Tube preftube;
    [Space]
    [SerializeField] private Level[] levels;
    [SerializeField] private TubePos[] tubePosition;

    public UnityAction ChangePanel;

    private List<Tube> tubesList = new List<Tube>();
    private int levelIndex;

    private void Start()
    {
        levelIndex = 0;
        GenerateLevel();
        uIControl.GenerateLevels += GenerateLevel;
    }

    private void OnEnable()
    {
        GameEvents.BallChangedTube += OnBallChangedTube;
    }

    private void OnDisable()
    {
        GameEvents.BallChangedTube -= OnBallChangedTube;
    }

    private void GenerateLevel()
    {
        TubesProduction();
        BallProduction();
    }
    private void DestroyCurrentLevel()
    {
        for (int i = tubesList.Count - 1; i >= 0; i--)
        {
            var tube = tubesList[i];
            tubesList.Remove(tube);
            Destroy(tube.gameObject);
        }
    }
    private void TubesProduction()
    {
        int dynamicLevelIndex = levelIndex % levels.Length;
        for (int i = 0; i < levels[dynamicLevelIndex].startTubeValues.Length; i++)
        {
            var posx = tubePosition[levelIndex].tubePos[i];
            var newTube = Instantiate(preftube, posx, Quaternion.identity);
            tubesList.Add(newTube);
        }
    }

    private void BallProduction()
    {
        int dynamicLevelIndex = levelIndex % levels.Length;
        int numberOfTubes = levels[dynamicLevelIndex].startTubeValues.Length;

        for (int i = 0; i < numberOfTubes; i++)
        {
            int numberOfColorsInThisTube = levels[dynamicLevelIndex].startTubeValues[i].startColorValues.Length;

            for (int j = 0; j < numberOfColorsInThisTube; j++)
            {
                EColor color = levels[dynamicLevelIndex].startTubeValues[i].startColorValues[j];

                if (color != EColor.Empty)
                {
                    var material = materialManager.GetColorMaterial(color);
                    var newBall = Instantiate(ball);

                    newBall.Init(material, color);

                    tubesList[i].SetBall(newBall);
                }
            }
        }
    }

    private void OnBallChangedTube()
    {
        bool areAllTubesSameColor = true;

        for (int i = 0; i < tubesList.Count; i++)
        {
            if (tubesList[i].AreAllBallsSameColor() == false)
            {
                areAllTubesSameColor = false;
                break;
            }
        }

        if (areAllTubesSameColor)
        {
            levelIndex++;
            DestroyCurrentLevel();
            ChangePanel?.Invoke();
        }
    }
}

[System.Serializable]
public class StartTubeValue
{
    public EColor[] startColorValues;
}

[System.Serializable]
public class Level
{
    public StartTubeValue[] startTubeValues;

}

[System.Serializable]
public class TubePos
{
    public Vector3[] tubePos;
}