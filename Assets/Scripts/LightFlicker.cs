using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float flickerInterval = 0.1f;
    private Light lightToControl;
    private bool isColliding = false;
    private float timer = 0f;

    private void Start()
    {
        lightToControl = GetComponent<Light>();
    }
    void Update()
    {
        // Only flicker the light when colliding with an enemy
        if (isColliding)
        {
            timer += Time.deltaTime;

            if (timer >= flickerInterval)
            {
                lightToControl.enabled = !lightToControl.enabled; 
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            isColliding = true;
            lightToControl.enabled = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isColliding = false;
            lightToControl.enabled = true; 
        }
    }
}
