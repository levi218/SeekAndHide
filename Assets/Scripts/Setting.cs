using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [Tooltip("Choose which GameSettings asset to use")]
    public GameSettings _settings; // drag GameSettings asset here in inspector

    [SerializeField]
    public static Setting instance;

    public event EventHandler IsSoundOnChanged;

    public void SetSoundStatus(bool isOn)
    {
        _settings.isSoundOn = isOn;
        IsSoundOnChanged?.Invoke(this, EventArgs.Empty);
    }


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Setting.instance == null)
        {
            Setting.instance = this;
        }
        else
        {
            Debug.LogWarning("A previously awakened Settings MonoBehaviour exists!", gameObject);
        }
    }

}
