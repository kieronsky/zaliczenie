using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public HealthManager healthManager;
    public Renderer rend;
    public Material cpOff;
    public Material cpOn;
    
    
    void Start()
    {
        healthManager= FindObjectOfType<HealthManager>();
    }

    public void CheckPointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckPointOff();
        }
        rend.material = cpOn;
    }

    public void CheckPointOff()
    {
        rend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            healthManager.SetSpawnPoint(transform.position);
            CheckPointOn();
        }
    }
}
