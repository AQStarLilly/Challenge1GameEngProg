using UnityEngine;

public class EventInitiator : MonoBehaviour
{
    public void OnButtonClicked()
    {
        Debug.Log("Button Clicked");
        ActionManager.OnButtonPressed?.Invoke(); //trigger the action
    }
}
