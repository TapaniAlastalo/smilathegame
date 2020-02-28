using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MainMenu,
        LoadingScene,
        SampleScene,        
        //Scene01,
        //Scene02,
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        // Set the loader callback action to load the target scene
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
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

    /*transform.Find("MyButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Click Main Menu");
            Loader.Load(Loader.Scene.MainMenu);
        };*/
}
