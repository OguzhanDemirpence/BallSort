using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject panelFinish;
    [SerializeField] private GameObject panelGamePlay;
    [Space]
    [SerializeField] private Button btnNextGame;
    [Space]
    [SerializeField] private TextMeshProUGUI txtStepsCountGame;
    [SerializeField] private TextMeshProUGUI txtStepsCountFinish;
    [Space]
    [SerializeField] private LevelManager levelManager;

    public UnityAction GenerateLevels;
    private int tubeValue = 2;


    void Start()
    {
        panelGamePlay.SetActive(true);
        panelFinish.SetActive(false);
        btnNextGame.onClick.AddListener(BtnNextGame);
        levelManager.ChangePanel += EnableFinishPanel;

        StartGame();
    }

    private void StartGame()
    {
        txtStepsCountGame.text = "0";
        
    }
    private void DisableFinishPanel()
    {
        panelGamePlay.SetActive(true);
        panelFinish.SetActive(false);
    }

    private void BtnNextGame()
    {
        value++;
        DisableFinishPanel();
        GenerateLevels?.Invoke();
    }

    public void StepsCount(int steps)
    {
        txtStepsCountGame.text = steps.ToString();
    }

    private void EnableFinishPanel()
    {
        panelFinish.SetActive(true);
        panelGamePlay.SetActive(false);
        txtStepsCountFinish.text = txtStepsCountGame.text;
    }

    public int value
    {
        get { return tubeValue; }
        set { tubeValue = value; }
    }

}
