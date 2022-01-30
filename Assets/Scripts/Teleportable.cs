using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityTemplateProjects;
using Random = UnityEngine.Random;

public class Teleportable : MonoBehaviour
{
    [SerializeField] private Carryable carryable;
    [SerializeField] private Collider collider;
    [SerializeField] private Rigidbody rb;

    [Space] [Header("Teleport Animation Settings")] [SerializeField]
    private float duration;

    [SerializeField] private float elasticity;

    [SerializeField] private int vibrato;
    [SerializeField] private Ease ease;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleportHole") && carryable.IsActive)
        {
            var teleportHole = other.GetComponent<TeleportHole>();
            Teleport(teleportHole);
        }
    }

    private void Teleport(TeleportHole teleportHole)
    {
        collider.enabled = false;

        rb.isKinematic = true;

        transform.DOShakeScale(duration, Vector3.zero, vibrato, elasticity)
            .SetEase(ease)
            .OnStart(teleportHole.Use)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                transform.position = teleportHole.ConnectedHole.transform.position + Vector3.up * 1.5f;
                gameObject.SetActive(true);

                transform.DOShakeScale(duration, Vector3.one, vibrato, elasticity)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        teleportHole.ConnectedHole.Use();
                        rb.isKinematic = false;
                        collider.enabled = true;
                    });
            });
    }
}