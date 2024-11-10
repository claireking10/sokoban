using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenActivate : MonoBehaviour
{
    public LevelManager levelManager;
    public int chickenIndex; // Unique index for each chicken

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the chicken has collided with its corresponding egg
        if (other.CompareTag("Egg"))
        {
            // Call ActivateChickenOnEgg in LevelManager to mark this chicken as on an egg
            levelManager.ActivateChickenOnEgg(chickenIndex);
        }
    }
}
