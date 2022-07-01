using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Wolf : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        size = 1.5f;
        age = 0.0f;
        lifeSpan = Random.Range(1000.0f, 2500.0f);
        caloricBase = 200.0f;
        calories = 400.0f;
        calorieMax = 800.0f;
        moveSpeed = 30.0f;
        satiety = (calories / calorieMax) * 100;
        health = 50.0f;
        metabolism = 3.0f;
        perception = 40.0f;
        diet = "Rabbit";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
