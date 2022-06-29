using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        size = 1.0f;
        age = 0.0f;
        lifeSpan = 900.0f;
        caloricBase = 100.0f;
        calories = 100.0f;
        moveSpeed = 1.0f;
        satiety = 50.0f;
        health = 50.0f;
        metabolism = 1.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
