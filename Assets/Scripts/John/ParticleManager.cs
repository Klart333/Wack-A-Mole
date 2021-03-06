﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField]
    private SharkKilledParticleSystem sharkKillParticleSystem;

    private void Start()
    {
        GameManager.Instance.OnSharkKilled += PlayParticleSystemOnSharkKilled;
    }

    private void PlayParticleSystemOnSharkKilled(float sharkTimeToKill)
    {
        Vector3 clickPos = Input.mousePosition;
        Vector3 position = Camera.main.ScreenToWorldPoint(clickPos);

        sharkKillParticleSystem.GetAtPosAndRot<SharkKilledParticleSystem>(position, Quaternion.identity);
    }
}
