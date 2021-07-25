using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{

    private float lerpTimer;
    public float chipTimer = 65f;
    public Image FrontHealthBar;
    public Image BackHealthBar;
    public TankData player;

    private void Start()
    {
        player.health = player.maxHealth;
    }

    public void Update()
    {
        player.health = Mathf.Clamp(player.health, 0, player.maxHealth);
        UpdateHealthBar();
        

        if(Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(Random.Range(2, 20));
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            RestoreHealth(Random.Range(2, 20));
        }
    }

    public void UpdateHealthBar()
    {
        Debug.Log(player.health);
        float fillF = FrontHealthBar.fillAmount;
        float fillb = BackHealthBar.fillAmount;
        float hFraction = player.health / player.maxHealth;
        
        if(fillb > hFraction)
        {
            FrontHealthBar.fillAmount = hFraction;
            BackHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float PercentComplete = lerpTimer / chipTimer;
            BackHealthBar.fillAmount = Mathf.Lerp(fillb, hFraction, PercentComplete);
        }
        if (fillF < hFraction)
        {
            BackHealthBar.fillAmount = hFraction;
            BackHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float PercentComplete = lerpTimer / chipTimer;
            FrontHealthBar.fillAmount = Mathf.Lerp(fillF, BackHealthBar.fillAmount, PercentComplete);
        }
    }

    public void TakeDamage(float DamageAmount)
    {
        player.health -= DamageAmount;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float HealAmount)
    {
        player.health += HealAmount;
        lerpTimer = 0f;
    }
}
