using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Life Settings")]
    public int maxLife = 5;
    public int currentLife;

    [Header("UI Hearts")]
    public Image[] hearts;
    public Sprite fullHeart;   // For heart_1 or heart_2
    public Sprite emptyHeart;  // For heart_0

    void Start()
    {
        currentLife = maxLife;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife < 0)
            currentLife = 0;

        UpdateHearts();

        if (currentLife == 0)
        {
            Debug.Log("Player Dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddLife(int amount)
    {
        currentLife += amount;
        if (currentLife > maxLife)
            currentLife = maxLife;

        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // If the heart index is less than current life, show the full heart
            if (i < currentLife)
            {
                hearts[i].sprite = fullHeart;
            }
            // Otherwise, show the empty heart
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}