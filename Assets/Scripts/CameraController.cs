using System.Collections;
using System.Collections.Generic;
using IndieMarc.TopDown;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float waitTime = 1.0f;

    public struct Screen
    {
        public Rect rect;
        public List<int> adjacentScreens; // We're storing indices now
        public MapTileData data;
    }

    public Screen[] screens;
    private int currentScreen = 0;
    private bool isTransitioning = false;

    private void Awake()
    {
        GameObject[] sprites = GameObject.FindGameObjectsWithTag("Ground");
        screens = new Screen[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            Bounds bounds = sprites[i].GetComponent<SpriteRenderer>().bounds;
            GameObject sprite = sprites[i];
            MapTileData data = sprite.GetComponent<MapTileData>();
            
            Vector2 position = sprites[i].transform.position; // The center of the sprite
            Vector2 size = bounds.size;
            // Align the rectangle with the sprite
            Rect rect = new Rect(position.x - size.x / 2, position.y - size.y / 2, size.x, size.y); 
            screens[i] = new Screen { rect = rect, adjacentScreens = new List<int>(), data = data};
        }

        for (int i = 0; i < screens.Length; i++)
        {
            for (int j = i+1; j < screens.Length; j++)
            {
                if (RectTouches(screens[i].rect, screens[j].rect))
                {
                    screens[i].adjacentScreens.Add(j);
                    screens[j].adjacentScreens.Add(i);
                }
            }
        }
    }

    private bool RectTouches(Rect a, Rect b)
    {
        return !(b.min.x > a.max.x || b.max.x < a.min.x || b.min.y > a.max.y || b.max.y < a.min.y);
    }

    private void Update()
    {
        int newScreenIndex = GetCurrentScreenIndex();
        if (newScreenIndex == -1)
        {
            Debug.LogError("Player is not within any screen!");
            return;
        }

        // If the player has moved to a different screen
        if (newScreenIndex != currentScreen)
        {
            currentScreen = newScreenIndex;
            StartCoroutine(TransitionToScreen(currentScreen));
        }
    }
    private int GetCurrentScreenIndex()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].rect.Contains(player.position))
            {
                return i;
            }
        }
        return -1;  // Return -1 if no screen is found
    }


    public void GoToScreenByID(string id)
    {
        StartCoroutine(TransitionToScreenByID(id));
        
    }

    public IEnumerator TransitionToScreenByID(string id)
    {
        isTransitioning = true;
        yield return new WaitForSeconds(waitTime);
        
        // based on the ID, find the screen in the screens array that has the id in the data component
        Screen targetScreen = System.Array.Find(screens, screen => screen.data.ID == id);
    
        // Check if the screen with the provided id exists.
        if(targetScreen.Equals(default(Screen)))
        {
            Debug.LogError("Screen with the provided id does not exist.");
            yield break;
        }
        
        // Move the camera to the center of the new screen.
        transform.position = new Vector3(targetScreen.rect.center.x, targetScreen.rect.center.y, transform.position.z);
    
        player.position = new Vector3(targetScreen.rect.center.x, targetScreen.rect.center.y, player.position.z);
        player.GetComponent<PlayerCharacter>().ChangeSprite(targetScreen.data.PlayerSprite);
        
        UpdateScreenText(targetScreen);
        isTransitioning = false; 
        currentScreen = GetCurrentScreenIndex();
        
        // the move the camera to the center of the new screen.
        
    }

    public void ResetPlayer()
    {
        // get the current screen
        // get the center point of that screen
        // move the player transform to this new place
    }

    // public variable for text mesh pro text field
    public TextMeshProUGUI screenText;
    
    public void UpdateScreenText(Screen screen)
    {
        // update the tmp pro text field with the screen's data title
        screenText.text = screen.data.MapDisplayName;
    }
    
    public IEnumerator TransitionToScreen(int screenIndex)
    {
        isTransitioning = true;

        yield return new WaitForSeconds(waitTime);

        // Move the camera to the center of the new screen
        Screen screen = screens[screenIndex];
        transform.position = new Vector3(screen.rect.center.x, screen.rect.center.y, transform.position.z);

        player.GetComponent<PlayerCharacter>().ChangeSprite(screen.data.PlayerSprite);
        UpdateScreenText(screen);
        
        isTransitioning = false;
    }

}
