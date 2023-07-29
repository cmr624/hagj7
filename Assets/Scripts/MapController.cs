using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public GameObject mapPanel; // The panel holding your map
    public GameObject buttonPrefab; // The prefab for your screen buttons
    public CameraController cameraController; 

    private Button[] screenButtons;

    void Start()
    {
        // Assuming the number of screens is known and constant
        screenButtons = new Button[cameraController.screens.Length];

        for (int i = 0; i < cameraController.screens.Length; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, mapPanel.transform);
            screenButtons[i] = buttonObj.GetComponent<Button>();
            int index = i; // Copy index to a new variable to prevent problems with the closure capturing the loop variable

            // Set up the click event for your button
            screenButtons[i].onClick.AddListener(() => FastTravel(index));
        }
    }

    void Update()
    {
        // Toggle the map with the 'M' key
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    void ToggleMap()
    {
        mapPanel.SetActive(!mapPanel.activeSelf);
    }

    void FastTravel(int screenIndex)
    {
        cameraController.StartCoroutine(cameraController.TransitionToScreen(screenIndex));
    }
}