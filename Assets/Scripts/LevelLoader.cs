using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/**
  * 
  * This class provides a method to load a specific scene.
  *
  * Currently used in the Game Menu to load either the
  * tutorial scene or the main game. This script is on the
  * Canvas game object and is referenced by the buttons
  * on the Level Select panel of the menu.
  * 
  * Can also be used to load the next level from each
  * level that is created.
  *
  */
public class LevelLoader : MonoBehaviour {

    /**
      *
      * Loads a level given the name of the scene
      * 
      * @param sceneName - a string containing the name of a scene
      *
      * If the sceneName provided matches a scene in the build settings,
      * the scene will be loaded
      * Otherwise, the currently active scene will be reloaded and
      * a message will be displayed in the console.
      *
      */
    public void LoadLevel(string levelName) {
        Debug.Log("There are " + SceneManager.sceneCountInBuildSettings + " scenes in the build.");
        // Start a Coroutine to find all the scenes
        // We need this to tell if the name provided is a valid scene name
        StartCoroutine(FindAllScenes());
        if (SceneManager.GetSceneByName(levelName).IsValid()) {
            SceneManager.LoadScene(levelName);
        } else {
            Debug.Log("Unable to load level " + levelName + ", reloading actice scene instead.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /**
      *
      * An asynchronous method that finds all the scenes in the build settings
      * 
      * This method attempts to load, using the Additive mode, each scene found
      * in the build settings.
      *
      * For some reason, this is needed to be able to check the names of the
      * scenes in the build settings.
      *
      * There is probably a cleaner way to do this. Right now, it does not seem
      * to slow anything down.
      *
      * TODO: Find a better way to check if the scene name provided as a parameter
      * to the LoadLevel method is a valid scene name matching a scene in the
      * build settings.
      *
      */
    private IEnumerator FindAllScenes() {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation[] asyncLoads = new AsyncOperation[SceneManager.sceneCountInBuildSettings];

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            Debug.Log("Begin Async Loading Scene #" + i + ".");
            asyncLoads[i] = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
        }
        
        bool[] completedLoads = new bool[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < completedLoads.Length; i++) {
            completedLoads[i] = false;
        }
        // Wait until the asynchronous scene fully loads
        while (!(allLoaded(completedLoads))) {
            for (int i = 0; i < completedLoads.Length; i++) {
                completedLoads[i] = asyncLoads[i].isDone;
            }
            yield return null;
        }
    }

    /**
      *
      * Private helper method to check and see if all of the scenes
      * in the build settings have been successfully loaded,
      * allowing us to compare the scene name provided in the
      * LoadLevels method to the scenes present in the build settings
      *
      */
    private bool allLoaded(bool[] loads) {
        for (int i = 0; i < loads.Length; i++) {
            if (loads[i] == false) {
                return false;
            }
        }
        return true;
    }
}
