using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        size = 1.5f;
        age = 0.0f;
        lifeSpan = 1800.0f;
        caloricBase = 200.0f;
        calories = 200.0f;
        moveSpeed = 20.0f;
        satiety = 30.0f;
        health = 50.0f;
        metabolism = 1.2f;
        perception = 30.0f;
        diet = "Rabbit";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
