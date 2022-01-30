using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class DoorActivatorButton : MonoBehaviour
    {
        [SerializeField] private MeshRenderer mesh;
        [SerializeField] private bool isMatch, isLockable;
        [SerializeField] private string triggableTag;

        [Space] [Header("Animation Settings")] [SerializeField]
        private Transform button;

        [SerializeField] private float duration;

        [SerializeField] private float onActiveY = 0f, onDeactiveY = 0.1f;
        [SerializeField] private Ease ease;

        [Space] [Header("Color Change Settings")] [SerializeField]
        private Color activateColor;

        [SerializeField] private Color deactivateColor;

        private Tween animation;
        private bool _isLocked = false;

        public delegate void ActivatorButtonDelegate();

        public event ActivatorButtonDelegate OnButtonActivated, OnButtonDeactivated;

        private void Start()
        {
            mesh.materials[0].color = deactivateColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(triggableTag))
            {
                if (other.TryGetComponent(out Carryable cube))
                    if (!cube.IsActive)
                        return;

                if (isLockable)
                {
                    if (isMatch && !_isLocked)
                        Activate();
                    else
                    {
                        PlayActivateAnimation();
                        ChangeColor(activateColor);
                    }
                }
                else
                    Activate();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(triggableTag))
            {
                if (other.TryGetComponent(out Carryable cube))
                    if (!cube.IsActive)
                        return;


                if (isLockable)
                {
                    if (isMatch && !_isLocked)
                        Deactivate();
                    else
                    {
                        ChangeColor(deactivateColor);
                        PlayDeactivateAnimation();
                    }
                }
                else
                    Deactivate();
            }
        }

        private void Deactivate()
        {
            OnButtonDeactivated?.Invoke();
            PlayDeactivateAnimation();
            ChangeColor(deactivateColor);
        }

        private void Activate()
        {
            OnButtonActivated?.Invoke();
            PlayActivateAnimation();
            ChangeColor(activateColor);
        }

        private void PlayActivateAnimation()
        {
            animation?.Kill(true);
            animation = button.DOLocalMoveY(onActiveY, duration)
                .SetEase(ease);
        }

        private void PlayDeactivateAnimation()
        {
            animation?.Kill(true);
            animation = button.DOLocalMoveY(onDeactiveY, duration)
                .SetEase(ease);
        }

        private void ChangeColor(Color color)
        {
            mesh.materials[0].color = color;
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public bool IsLockable => isLockable;
    }
}