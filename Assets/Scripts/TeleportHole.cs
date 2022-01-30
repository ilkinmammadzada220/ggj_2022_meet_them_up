using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class TeleportHole : MonoBehaviour
    {
        [SerializeField] private TeleportHole connectedHole;
        [SerializeField] private Collider collider;

        [Space] [Header("Destroy Animation Settings")] [SerializeField]
        private float duration;

        [SerializeField] private float elasticity;
        [SerializeField] private int vibrato;
        [SerializeField] private Ease ease;

        public void Use()
        {
            collider.enabled = false;
            transform.DOPunchScale(Vector3.zero, duration, vibrato, elasticity)
                .SetEase(ease)
                .OnComplete(() => { gameObject.SetActive(false); });
        }

        public TeleportHole ConnectedHole => connectedHole;
    }
}