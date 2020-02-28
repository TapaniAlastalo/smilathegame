using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    /*void Awake()
    {
        transform.Find("StartGameButton").GetComponent<Button>().ClickFunc = () =>
        {
            Debug.Log("Click Play");
            SceneLoader.Load(SceneLoader.Scene.SampleScene);
        };
    }*/

    public Button startGameButton;

    void Start()
    {
        Button btn = startGameButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Click Play");
        SceneLoader.Load(SceneLoader.Scene.SampleScene);
    }


}
