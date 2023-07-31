using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public GameObject mapPanel; // The panel holding your map
    public CameraController cameraController; 

    private Button[] screenButtons;


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

    public void FastTravel(string id)
    {
        cameraController.GoToScreenByID(id);
        ToggleMap();
    }
}