using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int lives; // levens, wordt voortdurend geupdate
    public static int StartLives = 5; //levens waarmee je begint.

    void Start()
    {
        lives = StartLives;
    }

    void FixedUpdate() // hier een FixedUpdate en niet een Update, want FixedUpdates worden wel stopgezet door Time.timeScale = 0;
    {
      if (lives < 0) // als de levens op zijn, dan wordt het spel stopgezet.
      {
        Debug.Log("Game Over!");
        Time.timeScale = 0; // dit pauseert het spel.
        }
    }
}






  
    
    





