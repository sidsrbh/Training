using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            // Destroy(this.gameObject);
            // Create a new cube primitive to set the color on
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //// Get the Renderer component from the new cube
            //var cubeRenderer = cube.GetComponent<Renderer>();

            //// Call SetColor using the shader property name "_Color" and setting the color to red
            //cubeRenderer.material.SetColor("_Color", Color.red);
           
        }


    }
}
