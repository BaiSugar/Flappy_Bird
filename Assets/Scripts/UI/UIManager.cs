using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject Main_Start_Panel;
    public GameObject Stop_Panel;
    public GameObject restartBtn;

    public Button backBtn;
    public InputField jumpHeight;
    public InputField groundSpeed;
    public InputField playerscore;

    public PipeCreat pipeCreat;
    public PlayerController playerController;

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        restartBtn.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OnReStart);
        scoreText.text = "Score\n0";
        groundSpeed.text = pipeCreat.bgMovent.speed.ToString();
        jumpHeight.text = playerController.jumpHeight.ToString();
        playerscore.text = playerController.score.ToString();
    }

    public void GmTalk()
    {
        if (jumpHeight.text == "" || groundSpeed.text == "" || scoreText.text == "") return;
        pipeCreat.bgMovent.speed = float.Parse(groundSpeed.text);
        playerController.jumpHeight = float.Parse(jumpHeight.text);
        playerController.score = int.Parse(playerscore.text);
        scoreText.text = "Score\n" + playerController.score.ToString();
    }

    public void StartOnClick()
    {
        Main_Start_Panel.SetActive(false);
        GameStrateManager.SetGameState(State.Playin);
        backBtn.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        pipeCreat.ReStart();
    }

    public void BackMainStart()
    {
        backBtn.gameObject.SetActive(false);
        Main_Start_Panel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        GameStrateManager.SetGameState(State.None);

        pipeCreat.ReStart();
    }

    public void OnReStart()
    {
        restartBtn.gameObject.SetActive(false);
        Stop_Panel.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(true);
        playerController.ReStart();
        pipeCreat.ReStart();
        scoreText.text = "Score\n0";
        GameStrateManager.SetGameState(State.Playin);
    }

    public void AddScore(long score)
    {
        if (GameStrateManager.GetState() != State.Playin) return;
        scoreText.text = "Score\n"+score.ToString();
    }

    public void StopPanel()
    {
        Time.timeScale = 0;
    }

    public void CancelStopPanel()
    {
        Time.timeScale = 1;
    }

    public void ShowReStartPanel()
    {
        restartBtn.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(true);
    }

    public void QuitOnClick()
    {
        Application.Quit();
    }


    
}
