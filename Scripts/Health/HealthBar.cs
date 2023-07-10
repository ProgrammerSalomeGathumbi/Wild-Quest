using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health animalHealth; // Reference to the Health component
    [SerializeField] private Image totalhealthBar; // Reference to the total health bar image
    [SerializeField] private Image currenthealthBar; // Reference to the current health bar image

    private void Start()
    {
        totalhealthBar.fillAmount = animalHealth.currentHealth / 10; // Set the initial fill amount of the total health bar based on the current health value
    }

    private void Update()
    {
        currenthealthBar.fillAmount = animalHealth.currentHealth / 10; // Update the fill amount of the current health bar based on the current health value
    }
}
