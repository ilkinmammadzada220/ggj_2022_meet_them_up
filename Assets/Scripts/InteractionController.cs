using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Transform connectToParent;
        [SerializeField] private MovementController movementController;
        private GameObject _interactableCube;
        private GameObject _carryingCube;

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Cube"))
                _interactableCube = other.gameObject;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.CompareTag("Cube") && other.gameObject != _carryingCube)
                _interactableCube = null;
        }

        private void Update()
        {
            if (_interactableCube != null)
                if (Input.GetKeyUp(KeyCode.C))
                {
                    if (!movementController.IsCarryingCube)
                    {
                        movementController.IsCarryingCube = !movementController.IsCarryingCube;
                        HoldCube();
                        movementController.LookAt(_interactableCube.transform.position);
                    }
                    else
                    {
                        movementController.IsCarryingCube = !movementController.IsCarryingCube;
                        DropCube();
                    }
                }
        }

        private void HoldCube()
        {
            _carryingCube = _interactableCube;
            _interactableCube.GetComponent<Carryable>().Hold();
            _interactableCube.transform.SetParent(connectToParent);
            _interactableCube.transform.position = connectToParent.position;
            _interactableCube.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        private void DropCube()
        {
            _interactableCube.GetComponent<Carryable>().Drop();
            _interactableCube.transform.SetParent(null);
            _interactableCube = null;
            _carryingCube = null;
        }
    }
}