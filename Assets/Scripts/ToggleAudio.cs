using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script realizat de Anna si Adela

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic;   

    public void Toggle()
    {
        if (_toggleMusic) {
            SoundManager.Instance.ToggleMusic();
        }
    }
   
}
