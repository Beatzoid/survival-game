using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]

    public float currentHealth;
    public float maxHealth = 100f;

    [Space]

    public float currentHunger;
    public float maxHunger = 100f;

    [Space]

    public float currentThirst;
    public float maxThirst = 100f;

    [Header("Stats Depletion")]

    [Tooltip("How much hunger is depleted per second")]
    public float hungerDepletion = 0.5f;

    [Tooltip("How much thirst is depleted per second")]
    public float thirstDepletion = 0.75f;

    [Header("Stats Damages")]

    [Tooltip("How much damage the player takes per second when hunger is at 0")]
    public float hungerDamage = 1.5f;

    [Tooltip("How much damage the player takes per second when thirst is at 0")]
    public float thirstDamage = 2.25f;

    [Header("UI")]
    public StatsBar healthBar;
    public StatsBar hungerBar;
    public StatsBar thirstBar;

    public void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
    }

    public void Update()
    {
        UpdateStats();
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.numberText.text = currentHealth.ToString("f0");
        healthBar.bar.fillAmount = currentHealth / 100;

        hungerBar.numberText.text = currentHunger.ToString("f0");
        hungerBar.bar.fillAmount = currentHunger / 100;

        thirstBar.numberText.text = currentThirst.ToString("f0");
        thirstBar.bar.fillAmount = currentThirst / 100;
    }

    private void UpdateStats()
    {
        // Clamp makes it so that if it goes under the minimum (0) or over
        // the maximum (100) it will set it to the minimum or maximum respectively
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);

        // Depletion

        if (currentHunger > 0)
            currentHunger -= hungerDepletion * Time.deltaTime;

        if (currentThirst > 0)
            currentThirst -= thirstDepletion * Time.deltaTime;

        // Damages

        if (currentHunger <= 0)
            currentHealth -= hungerDamage * Time.deltaTime;

        if (currentThirst <= 0)
            currentHealth -= thirstDamage * Time.deltaTime;
    }
}


// if (health <= 0) health = 0;
// if (health >= maxHealth) health = maxHealth;

// if (hunger <= 0) hunger = 0;
// if (hunger >= maxHunger) hunger = maxHunger;

// if (thirst <= 0) thirst = 0;
// if (thirst >= maxThirst) thirst = maxThirst;
