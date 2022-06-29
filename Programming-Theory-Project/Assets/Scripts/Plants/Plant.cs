using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Organism
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // If a plant goes to seed ("dies"), then it multiplies.
    protected override void Die()
    {
        // Calculate between 2 to 4 Carrots to be seeded.
        int deathSpawns = Random.Range(2, 4);

        // Spawn the carrots near the point of death.
        for (int i = 0; i < deathSpawns; i++)
        {
            // Generate carrot spawn point.
            Vector3 spawnPoint = new Vector3(
                transform.position.x + Random.Range(-4.0f, 4.0f),
                transform.position.y,
                transform.position.z + Random.Range(-4.0f, 4.0f)
            );

            // Create a carrot there.
            Instantiate(GameManager.instance.organisms[0], spawnPoint, GameManager.instance.organisms[0].transform.rotation);
        }

        // Organism Death
        base.Die();
    }
}
