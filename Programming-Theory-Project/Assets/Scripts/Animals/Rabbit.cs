using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Rabbit : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        size = 1.0f;
        age = 0.0f;
        lifeSpan = Random.Range(600.0f, 1100.0f);
        caloricBase = 100.0f;
        calories = 200.0f;
        calorieMax = 400.0f;
        moveSpeed = 15.0f;
        satiety = (calories / calorieMax) * 100;
        health = 50.0f;
        metabolism = 1.0f;
        perception = 40.0f;
        diet = "Carrot";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
