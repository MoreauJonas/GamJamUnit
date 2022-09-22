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

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(60);
        }
    }

    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            //verification si le joueur est toujours vivant
            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilitiyDelay());
        }
        
    }
      public static PlayerHealth instance;
  private void Awake()
  {
    if (instance !=null)
    {
      Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dnas la scéne");
      return;
    }
    instance = this;
  }

    public void Die()
        {
            Debug.Log("le joueur est éliminé");
            //bloquer les mouvement du personnage
            PlayerMovement.instance.enabled=false;

            //jouer l'animation d'élimination
            PlayerMovement.instance.animator.SetTrigger("Die");

            //empécher les interaction pysqie avec les autres éléments de la scéne
            PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
            PlayerMovement.instance.playerCollider.enabled=false;

            //ecran de game over
            GameOverManager.instance.OnPlayerDeath();

        }
        public void Respawn()
        {
            Debug.Log("le joueur est éliminé");
            //bloquer les mouvement du personnage
            PlayerMovement.instance.enabled=true;

            //jouer l'animation d'élimination
            PlayerMovement.instance.animator.SetTrigger("Respawn");

            //empécher les interaction pysqie avec les autres éléments de la scéne
            PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
            PlayerMovement.instance.playerCollider.enabled=true;
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
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
