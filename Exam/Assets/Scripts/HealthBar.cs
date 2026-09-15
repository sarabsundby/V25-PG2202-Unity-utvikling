using System;
using UnityEngine;
using UnityEngine.UI;

public class connectHealthBar : MonoBehaviour
{
   
    public Slider healthbar; //kobler opp til slider
    public Health playerHealth; //kobler opp til player health

    private void Start()
    {
        if (playerHealth == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerHealth =playerObj.GetComponent<Health>();
            }
            else
            {
                Debug.Log("fant ikke player");
            }
        }

        if (playerHealth != null)
        {
            healthbar.maxValue = playerHealth.maxHealth;
            healthbar.value = playerHealth.maxHealth;
        }
        else
        {
            Debug.Log("player health er null");
        }
    
        }
        


    public void setHealth(int hp)
    {
        healthbar.value = hp; 
    }

}
