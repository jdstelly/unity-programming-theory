using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant
{
    // Start is called before the first frame update
    void Start()
    {
        size = 1.0f;
        growthRate = 1.0f;
        age = 0.0f;
        lifeSpan = 300.0f;
        caloricBase = 25.0f;
        calories = 25.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
