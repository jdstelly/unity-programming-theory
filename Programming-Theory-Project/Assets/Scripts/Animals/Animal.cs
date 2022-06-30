using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Organism
{
    public float moveSpeed;
    public float metabolism;
    public float satiety;
    public float health;
    public float perception;
    public bool isMeandering;
    public Dictionary<string, Vector3> geoPoints = new Dictionary<string, Vector3>();
    public Dictionary<string, float> animalState = new Dictionary<string, float>();

    private void Awake()
    {
        InitializeAnimal();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Organism Update (Old age causes death)
        base.Update();

        // If an Animal starves, it dies.
        if (satiety <= 0)
        {
            Die();
        }

        // If an Animal's needs are generally met, it idly wanders.
        if (satiety > 50.0f && satiety <= 90.0f)
        {
            Wander();
        }

        // If an Animal is hungry, it searches for food.
        if (satiety < 50.0f)
        {
            Search("food");
        }

    }

    // When an Animal dies, Nature magically turns its body into Carrots.
    protected override void Die()
    {
        // Calculate amount of carrots to spawn on death from creature's base calories.
        int deathSpawns = Mathf.RoundToInt(caloricBase / 25.0f);

        // Spawn the carrots near the point of death.
        for (int i = 0; i < deathSpawns; i++)
        {
            // Generate carrot spawn point.
            Vector3 spawnPoint = new Vector3(
                transform.position.x + Random.Range(-4.0f, 4.0f),
                transform.position.y - 1,
                transform.position.z + Random.Range(-4.0f, 4.0f)
            );

            // Create a carrot there.
            Instantiate(GameManager.instance.organisms[0], spawnPoint, GameManager.instance.organisms[0].transform.rotation);
        }

        // Organism Death
        base.Die();
    }

    // When an Animal is wandering, it periodically meanders.
    protected void Wander()
    {
        if (!isMeandering)
        {
            if (animalState["meanderCooldown"] <= 0)
            {
                // Roll to meander (10% per tick).
                isMeandering = Random.Range(1, 100) > 90;
                if (isMeandering)
                {
                    
                    // Generate location to meander to.
                    geoPoints["actionPoint"] = new Vector3
                    (
                        transform.position.x + Random.Range(-16.0f, 16.0f),
                        transform.position.y,
                        transform.position.z + Random.Range(-16.0f, 16.0f)
                    );
                    //Debug.Log($"Meandering activated. Destination is:{geoPoints["actionPoint"]}");
                }
            } else
            {
                animalState["meanderCooldown"] -= 0.01f;
            }
        } else
        {
            isMeandering = GoTo(geoPoints["actionPoint"]);
            if (!isMeandering)
            {
                animalState["meanderCooldown"] = 5.0f;
            }
        }
    }

    // When an Animal searches, it moves to a random point in search for a need within perception radius.
    protected void Search(string need)
    {

    }

    // When an Animal eats, it moves to and destroys the target object.
    protected void eat(string target)
    {

    }

    // When an Animal mates, it moves to the target Animal and a similar Animal instance is created.
    protected void mate(string target)
    {

    }


    // When an Animal migrates, it moves to a random point on the edge of the map, then destroys itself.
    protected void migrate()
    {

    }

    protected void InitializeAnimal()
    {
        geoPoints.Add("spawnPoint", Vector3.zero);
        geoPoints.Add("actionPoint", Vector3.zero);
        animalState.Add("meanderCooldown", 0);
    }

    protected bool GoTo(Vector3 destination)
    {
        //Debug.Log($"Moving Object {name} to {destination} from {transform.position}");
        if (Vector3.Distance(transform.position, destination) > 0.5f)
        {
            transform.LookAt(geoPoints["actionPoint"]);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }
}