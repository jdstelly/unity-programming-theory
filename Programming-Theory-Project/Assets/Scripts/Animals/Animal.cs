using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Organism
{
    public float moveSpeed;
    public float metabolism;
    public float satiety;
    public float health;
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
        //InitializeAnimal();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // If an Animal starves, it dies.
        if (satiety <= 0)
        {
            Die();
        }

        // DEBUG
        Wander();
        Debug.Log("Animal Update");

        // Organism Update
        base.Update();
    }

    // If an Animal dies, Nature magically turns its body into Carrots.
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
                transform.position.y,
                transform.position.z + Random.Range(-4.0f, 4.0f)
            );

            // Create a carrot there.
            Instantiate(GameManager.instance.organisms[0], spawnPoint, GameManager.instance.organisms[0].transform.rotation);
        }

        // Organism Death
        base.Die();
    }

    // When an animal isn't dying, it might just be wandering.
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
                        transform.position.x + Random.Range(-6.0f, 6.0f),
                        transform.position.y,
                        transform.position.z + Random.Range(-6.0f, 6.0f)
                    );
                    Debug.Log($"Meandering activated. Destination is:{geoPoints["actionPoint"]}");
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
                animalState["meanderCooldown"] = 50.0f;
            }
        }
    }

    protected void eat(string target)
    {

    }

    protected void mate(string target)
    {

    }

    protected void search(string need)
    {

    }

    protected void migrate()
    {

    }

    protected void InitializeAnimal()
    {
        Debug.Log("Initialized animal");
        geoPoints.Add("spawnPoint", Vector3.zero);
        geoPoints.Add("actionPoint", Vector3.zero);
        animalState.Add("meanderCooldown", 0);
    }

    protected bool GoTo(Vector3 destination)
    {
        Debug.Log($"Moving to {destination}");
        if (Vector3.Distance(transform.position, destination) > 1.0f)
        {
            Debug.Log($"Not yet at {destination} from {transform.position} (distance: {Vector3.Distance(transform.position, destination)})");
            Vector3 travelVector = (destination - transform.position).normalized;
            transform.Translate(travelVector * moveSpeed * Time.deltaTime);
            return true;
        }
        Debug.Log($"Arrived at destination {destination}!");
        return false;
    }
}