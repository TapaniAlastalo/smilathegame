using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{    
    public enum Scene
    {
        MainScene,        
        Scene01,
        Scene02,
        Scene03,        // Last Scene
        LoadingScene,   // Extras Not Counted
    }

    private const Scene firstScene = Scene.MainScene;    
    private const Scene lastScene = Scene.Scene03;      // Last Scene
    private static Scene currentScene = firstScene;

    public static void LoadNext()
    {
        if(currentScene < lastScene)
        {
            Load(currentScene +1);
        }
        else
        {
            Load(Scene.MainScene);
        }
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        // Set the loader callback action to load the target scene
        onLoaderCallback = () =>
        {            
            SceneManager.LoadScene(scene.ToString());
            currentScene = scene;
        };
        // Load the loading scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    // Triggered after first screen update
    // Execute the loadr callback action which will load the target scene
    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
