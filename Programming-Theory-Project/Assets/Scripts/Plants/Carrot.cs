using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Carrot : Plant
{
    // Start is called before the first frame update
    void Start()
    {
        size = 1.0f;
        growthRate = 1.0f;
        age = 0.0f;
        lifeSpan = Random.Range(45.0f, 90.0f);
        caloricBase = 50.0f;
        calories = 50.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
