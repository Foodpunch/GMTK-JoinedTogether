﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound(AudioManager.SoundType.ROCKETEXPLOSION, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
