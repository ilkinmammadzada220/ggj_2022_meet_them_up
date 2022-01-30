using Cinemachine;
using UnityEngine;

public class CameraFollowerSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private Transform playerOne, playerTwo;

    private void Start()
    {
        cam.Follow = playerOne;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (cam.Follow.Equals(playerOne))
                cam.Follow = playerTwo;
            else
                cam.Follow = playerOne;
        }
    }
}