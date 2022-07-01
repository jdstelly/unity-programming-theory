using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Plant : Organism
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // POLYMORPHISM
    protected override void Update()
    {
        // If a Plant gets too old, it flowers and goes to seed.
        if (age > lifeSpan)
        {
            Die();
        }

        base.Update();
    }

    // If a plant goes to seed ("dies"), then it multiplies.
    // POLYMORPHISM
    protected override void Die()
    {
        // Calculate between 2 to 4 Carrots to be seeded.
        int deathSpawns = Random.Range(2, 3);

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

        // Organism Death (destruction)
        base.Die();
    }
}
