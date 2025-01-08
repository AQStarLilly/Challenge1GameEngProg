using System;
using UnityEngine;
using TMPro;

public class AllInOneScript : MonoBehaviour
{
    //static action to act as an event that the other methods can subscribe to
    public static Action OnButtonPressed;

    public GameObject cubeObject;
    public GameObject sphereObject;
    public TextMeshProUGUI uiText;

    private Renderer _cubeRenderer;
    private bool _sphereIsSpinning;

    //array of messages to display
    private string[] messages =
    {
        "Button was Pressed!",
        "Party Time!",
        "Stop pressing my Buttons!",
        "Your wasting your time",
        "Nothing to see here"
    };

    private void Start()
    {
        //get the renderer component of the cube if it's assigned
        if (cubeObject != null)
            _cubeRenderer = cubeObject.GetComponent<Renderer>();

        // Subscribe to the Action
        OnButtonPressed += ChangeCubeColor;
        OnButtonPressed += StartSphereSpinning;
        OnButtonPressed += DisplayRandomMessage;

        Debug.Log("Script initialized. Subscribed to Action.");
    }

    // This method will be called directly by the Button
    public void OnButtonClicked()
    {
        Debug.Log("Button clicked");
        OnButtonPressed?.Invoke(); //Invoke the action, triggering all subscribed methods
    }

    private void ChangeCubeColor()
    {
        if (_cubeRenderer != null)
        {
            _cubeRenderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            Debug.Log("Cube changed color!");
        }
    }

    private void StartSphereSpinning()
    {
        _sphereIsSpinning = true;
        Debug.Log("Sphere started spinning!");
    }

    private void DisplayRandomMessage()
    {
        if (uiText != null && messages.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, messages.Length);
            uiText.text = messages[randomIndex];
            Debug.Log($"Random message displayed: {messages[randomIndex]}");
        }
    }

    private void Update()
    {
        // Make the sphere spin continuously if it’s spinning
        if (_sphereIsSpinning && sphereObject != null)
        {
            sphereObject.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        //unsubscribe methods from the action to avoid memory leaks
        OnButtonPressed -= ChangeCubeColor;
        OnButtonPressed -= StartSphereSpinning;
        OnButtonPressed -= DisplayRandomMessage;
    }
}
