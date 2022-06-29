using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organism : MonoBehaviour
{
    public float age;
    public float growthRate;
    public float lifeSpan;
    public float reproductionRate;
    public float size;
    public float caloricBase;
    public float calories;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // If an Organism outlives its lifespan, it dies.
        if (age >= lifeSpan)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(this);
    }
}
