using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public int maxHealth = 300;
    public int currentHealth;
    public connectHealthBar healthBar;

    private bool iDied = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (iDied) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.setHealth(currentHealth);
        }

    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            iDied = true;
            SceneManager.LoadScene(2);
            Debug.Log("Player døde");
        }
    }

}
