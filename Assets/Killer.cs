using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private ParticleSystem pufFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (pufFX)
                Instantiate(pufFX, other.transform.position + Vector3.up * 3f, pufFX.transform.rotation, null).transform
                    .localScale = Vector3.one;
            GameManager.Instance.Lose();
        }
    }
}