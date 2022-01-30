using System;
using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Transform connectToParent;
        [SerializeField] private MovementController movementController;

        [SerializeField] private ParticleSystem metFX;
        [SerializeField] private GameObject heartPrefab;
        private GameObject _interactableCube;
        private GameObject _carryingCube;
        GameObject heart;

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Cube"))
                _interactableCube = other.gameObject;

            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                if (movementController.CanControllable)
                {
                    PlayMetAnimation();
                    GameManager.Instance.Win();
                    other.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.CompareTag("Cube") && other.gameObject != _carryingCube)
                _interactableCube = null;
        }

        private void PlayMetAnimation()
        {
            if (metFX)
                Instantiate(metFX, transform.position, metFX.transform.rotation, null);
            if (heartPrefab)
                heart = Instantiate(heartPrefab, transform.position, heartPrefab.transform.rotation, null);
            heart.transform.localScale = Vector3.zero;
            heart.transform.DOMoveY(5f, 3f)
                .OnStart(() => { heart.transform.DOScale(Vector3.one, 1.5f); });
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