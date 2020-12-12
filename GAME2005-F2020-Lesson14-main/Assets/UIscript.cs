using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIscript : MonoBehaviour
{
    public void StartSceneButton()
    {
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
