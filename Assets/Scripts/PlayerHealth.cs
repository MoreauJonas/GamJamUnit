using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invisibilityDelayEnd = 2f; //temps d'invinsibilité après être touché
    public float invisibilityFlashDelay = 0.2f; //temps de clignotement de l'invinsibilités
    public bool isInvincible =false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance !=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void  HealPlayer(int amount)
    {   
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilitiyDelay());
        }
        
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color= new Color (1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invisibilityFlashDelay);
            graphics.color= new Color (1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invisibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilitiyDelay()
    {
        yield return new WaitForSeconds(invisibilityDelayEnd);
        isInvincible = false;
    }

}
