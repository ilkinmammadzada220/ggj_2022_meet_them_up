using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Vector3 angleToClose, angleToOpen;

        [Space] [Header("Door Animation Settings")] [SerializeField]
        private float duration;

        [SerializeField] private Ease ease;
        private bool _isOpen = false;

        public void Open()
        {
            transform.DORotate(angleToOpen, duration)
                .SetEase(ease);
            _isOpen = true;
        }

        public void Close()
        {
            transform.DORotate(angleToClose, duration)
                .SetEase(ease);
            _isOpen = false;
        }

        public bool IsOpen => _isOpen;
    }
}