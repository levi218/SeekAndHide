using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    bool isGameOver;

    public Animator messageSceneAnimator;
    public TextMeshProUGUI messageText;

    public GameObject player;
    public GameObject startPoint;

    public GameObject panelGameOver;
    public GameObject panelPause;
    public GameObject panelInstruction;

    public float timeRoundStart;
    private static GameController _instance = null;
    public static GameController Instance {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        if (Setting.instance._settings.currentLevel == 0)
        {
            Setting.instance._settings.currentTime = 0;
        }
    }
    private void Start()
    {
        player.transform.position = startPoint.transform.position;
        timeRoundStart = Time.time;
    }
    public void GameOver()
    {
        ShowMessage("GameOver");
    }
    
    public void ShowMessage(string message, bool isHideAfter = false)
    {
        messageSceneAnimator.gameObject.SetActive(true);
        Time.timeScale = 0;
        messageSceneAnimator.Play("Appear");
        messageText.text = message;
        if (isHideAfter) StartCoroutine(ExecuteHideMessage(3,messageSceneAnimator));
    }

    public void ShowInstruction(EnemyInformation info)
    {
        panelInstruction.SetActive(true);
        Time.timeScale = 0;
        panelInstruction.GetComponent<Animator>().Play("Appear");
        panelInstruction.GetComponentInChildren<Image>().sprite = info.sprite;
        panelInstruction.GetComponentInChildren<Image>().color = info.color;
        panelInstruction.GetComponentInChildren<TextMeshProUGUI>().text = info.name+": "+info.description;
        //StartCoroutine(ExecuteHideMessage(3, panelInstruction.GetComponent<Animator>()));
        StartCoroutine(ExecuteHideInstruction());
    }

    IEnumerator ExecuteHideInstruction()
    {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        panelInstruction.GetComponent<Animator>().Play("Disappear");
    }

    IEnumerator ExecuteHideMessage(float time, Animator animator)
    {
        yield return new WaitForSecondsRealtime(time);
        HideMessage(animator);
    }
    public void HideMessage(Animator animator)
    {
        Time.timeScale = 1;
        animator.Play("Disappear");
        Invoke("DisableMessagePanel", 1f);
    }
    void DisableMessagePanel()
    {
        messageSceneAnimator.gameObject.SetActive(false);
    }
    public void NextStage()
    {
        Setting.instance._settings.currentLevel++;
        CalculateTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void CalculateTime()
    {
        float timeThisRound = Time.time - timeRoundStart;
        Setting.instance._settings.currentTime += timeThisRound;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        panelPause.SetActive(true);
        ShowMessage("Paused");
        // show panel with buttons
        foreach (Animator animator in panelPause.GetComponentsInChildren<Animator>())
        {
            animator.Play("ButtonShow");
        }
    }

    public void Resume()
    {
        panelPause.SetActive(false);
        HideMessage(messageSceneAnimator);
        // show panel with buttons
        foreach (Animator animator in panelPause.GetComponentsInChildren<Animator>())
        {
            animator.Play("ButtonHide");
        }
    }

    public void PlayerWin()
    {
        CalculateTime();
        if(Setting.instance._settings.minCompleteTime> Setting.instance._settings.currentTime)
        {
            Setting.instance._settings.minCompleteTime = Setting.instance._settings.currentTime;
        }
        ShowMessage("You won");
        panelGameOver.SetActive(true);
        // show panel with buttons
        foreach (Animator animator in panelGameOver.GetComponentsInChildren<Animator>())
        {
            animator.Play("ButtonShow");
        }

    }
    public void ReloadStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
