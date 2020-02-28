using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;
    private const float waitTime = 1.0f;

    private void Update()
    {
        if (isFirstUpdate)
        {
            Invoke("callNextLevel", waitTime);
        }
    }

    private void callNextLevel()
    {
        isFirstUpdate = false;
        SceneLoader.LoaderCallback();
    }
}
