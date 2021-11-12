using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{


    PlayerMouvement plm;
   public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   public void ControlAccelerometer()
    {
        plm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();

        plm.OnTiltControl();
    }

      
    
}
