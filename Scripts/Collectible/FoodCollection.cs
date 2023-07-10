using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCollection : MonoBehaviour
{
    private int food = 0; // Counter for the collected food items

    [SerializeField] private Text foodText; // Reference to the UI text element displaying the collected food count

    [Header("Sound")]
    [SerializeField] private AudioSource collectSoundEffect; // Sound effect played when collecting food

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food")) // Check if the collided object has the "Food" tag
        {
            collectSoundEffect.Play(); // Play the collect sound effect
            Destroy(collision.gameObject); // Destroy the collected food object
            food = food + 1; // Increment the food count
            foodText.text = "x " + food.ToString(); // Update the UI text element to display the new food count
        }        
    }            
}
