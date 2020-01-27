using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{

    public Image healthBar;
    public Text healthText;
    float health1=100f;
    
    public void TakeDamage(float amount)
    {
       health1 -= amount;
        healthText.text = string.Format("% {0}", health1);
        healthBar.fillAmount = health1 / 100f;

        if (health1 <= 0)
        {
            Destroy(gameObject);
        }
    }
}
