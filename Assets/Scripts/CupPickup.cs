using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupPickup : MonoBehaviour
{

    public int value;

    public GameObject pickupEffect;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddCups(value);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
