using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button btnNewgame;
    public Button btnContinue;
    public Toggle toggleSound;
    public TextMeshProUGUI txtCompleteTime;
    // Start is called before the first frame update
    void Awake()
    {
        if (Setting.instance._settings.currentLevel == 0)
        {
            btnContinue.interactable = false;
        }
        if(Setting.instance._settings.minCompleteTime>0)
        {
            txtCompleteTime.text = "Completed in " + (Setting.instance._settings.minCompleteTime / 60).ToString("F2") + " minutes";
        }
        toggleSound.isOn = Setting.instance._settings.isSoundOn;
        
    }

    public void Newgame() {
        Setting.instance._settings.currentLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToggleSoundChanged()
    {
        Setting.instance.SetSoundStatus(toggleSound.isOn);
    }
}
