using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCollection : MonoBehaviour
{
    private int food = 0;

    [SerializeField] private Text foodText;
    [Header("Sound")]
    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            food = food + 1;
            foodText.text = "x " + food.ToString();
            
        }        
    }            
}
