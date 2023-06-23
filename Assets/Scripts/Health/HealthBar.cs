using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health animalHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = animalHealth.currentHealth / 10;
    }
    private void Update()
    {
        currenthealthBar.fillAmount = animalHealth.currentHealth / 10;
    }
}