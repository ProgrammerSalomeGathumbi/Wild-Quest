using UnityEngine;

public class SlashesHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy; // Reference to the enemy's transform

    private void Update()
    {
        transform.localScale = enemy.localScale; // Set the local scale of the slashes holder to match the enemy's local scale
    }
}
