using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class TeleportHole : MonoBehaviour
    {
        [SerializeField] private TeleportHole connectedHole;
        [SerializeField] private Collider collider;
        [SerializeField] private ParticleSystem fx;

        [Space] [Header("Destroy Animation Settings")] [SerializeField]
        private float duration;

        [SerializeField] private Ease ease;

        public void Use()
        {
            collider.enabled = false;
            transform.DOScale(Vector3.zero, duration)
                .SetEase(ease)
                .OnUpdate(() =>
                {
                    fx.startSize = transform.localScale.x;
                })
                .OnComplete(() => { gameObject.SetActive(false); });
        }

        public TeleportHole ConnectedHole => connectedHole;
    }
}