using UnityEngine;
using UnityTemplateProjects;

public class MultipleActivatorButtonController : MonoBehaviour
{
    [SerializeField] private DoorActivatorButton[] activators;
    [SerializeField] private Door door;

    private int activatorCount = 0;
    private int activeButtonCount = 0;

    private void Start()
    {
        activatorCount = activators.Length;
    }

    private void OnEnable()
    {
        foreach (var doorActivatorButton in activators)
        {
            doorActivatorButton.OnButtonActivated += OnButtonActivated;
            doorActivatorButton.OnButtonDeactivated += OnButtonDeactivated;
        }
    }

    private void OnDisable()
    {
        foreach (var doorActivatorButton in activators)
        {
            doorActivatorButton.OnButtonActivated -= OnButtonActivated;
            doorActivatorButton.OnButtonDeactivated -= OnButtonDeactivated;
        }
    }

    private void OnButtonActivated()
    {
        activeButtonCount++;
        ActiveStateChecker();
    }

    private void OnButtonDeactivated()
    {
        activeButtonCount--;
        ActiveStateChecker();
    }

    private void ActiveStateChecker()
    {
        print($"active btn count -> {activeButtonCount}\nactivator count -> {activators.Length}");
        if (activeButtonCount == activatorCount)
        {
            door.Open();
            LockDoors();
        }
        else if (door.IsOpen)
            door.Close();
    }

    private void LockDoors()
    {
        foreach (var doorActivatorButton in activators)
        {
            if (doorActivatorButton.IsLockable)
                doorActivatorButton.Lock();
        }
    }
}