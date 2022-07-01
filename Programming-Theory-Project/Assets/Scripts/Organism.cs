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
    protected bool canAge;

    protected virtual void Awake()
    {
        canAge = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (canAge)
        {
            canAge = false;
            StartCoroutine(Age());
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual IEnumerator Age()
    {
        yield return new WaitForSeconds(1 / GameManager.instance.gameSpeed);
        age += (1.0f);
        canAge = true;
    }
}
