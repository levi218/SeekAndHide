using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    void Start()
    {
        HandleEvent(null, null);
        Setting.instance.IsSoundOnChanged += this.HandleEvent;
    }

    public void HandleEvent(object sender, EventArgs args)
    {
        GetComponent<AudioSource>().volume = Setting.instance._settings.isSoundOn ? 1 : 0;
    }

    private void OnDestroy()
    {
        Setting.instance.IsSoundOnChanged -= this.HandleEvent;

    }
}
