using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    private void Start()
    {
       
    }
    public void PlayButton() 
    {
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
       
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
    }
}
