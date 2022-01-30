using UnityEngine;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(Rigidbody))]
    public class Carryable : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool _isActive;
        public void Hold()
        {
            rb.isKinematic = true;
            _isActive = false;
        }

        public void Drop()
        {
            rb.isKinematic = false;
            _isActive = true;
        }

        public bool IsActive => _isActive;
    }
}