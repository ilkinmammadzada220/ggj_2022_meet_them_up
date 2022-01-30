using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            _isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            _isGrounded = false;
    }

    public bool IsGrounded => _isGrounded;
}