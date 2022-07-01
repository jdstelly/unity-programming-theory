using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Animal : Organism
{
    public float moveSpeed;
    public float metabolism;
    public float satiety;
    public float calorieMax;
    public float health;
    public float perception;
    public bool isMoving;
    public bool isMigrating;
    public string diet;
    public GameObject targetEntity = null;
    public Dictionary<string, Vector3> geoPoints = new Dictionary<string, Vector3>();
    public Dictionary<string, float> animalState = new Dictionary<string, float>();

    protected override void Awake()
    {
        base.Awake();
        InitializeAnimal();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // POLYMORPHISM
    protected override void Update()
    {
        // If an Animal starves or gets too old, it dies.
        if (satiety <= 0 || age > lifeSpan) Die();
        
        // Time passes, Animal gets older.
        if (canAge)
        {
            StartCoroutine(Age());
            canAge = false;
        }
        
        // Animal Behavior
        AnimalBrain();

    }

    // Animal Behavior Function
    // ABSTRACTION
    protected void AnimalBrain()
    {

        // If an Animal is hungry, it searches for food.
        if (satiety < 90.0f)
        {
            if (targetEntity == null)
            {
                Search("eat");
            } else
            {
                Eat(targetEntity);
            }
        }

        // If an Animal is very well fed, it breeds.
        if (satiety >= 90.0f)
        {
            if (targetEntity == null)
            {
                Search("mate");
            }
            else
            {
                Mate(targetEntity);
            }
        }
    }

    // When an Animal dies, Nature magically turns its body into Carrots.
    // POLYMORPHISM
    protected override void Die()
    {
        // Calculate amount of carrots to spawn on death from creature's base calories.
        int deathSpawns = Mathf.RoundToInt(caloricBase / 50.0f);

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

        // Organism Death (destruction)
        base.Die();
    }

    // When an Animal is wandering, it periodically meanders.
    protected void Wander()
    {
        Search("wander");
    }

    // When an Animal searches, it moves to a random point in a variable radius in search of a goal.
    protected void Search(string goal)
    {
        // Movement Cycle
        if (!isMoving)
        {
            if (animalState["moveCooldown"] <= 0)
            {
                // Roll to move to next point (10% per tick).
                isMoving = Random.Range(1, 100) > 90;
                if (isMoving)
                {
                    // Generate location to meander to.
                    geoPoints["actionPoint"] = GenerateNavPoint(goal == "wander" ? 8.0f : perception);
                }
            } else
            {
                animalState["moveCooldown"] -= (0.01f * GameManager.instance.gameSpeed);
            }
        } else
        {
            isMoving = GoTo(geoPoints["actionPoint"]);
            if (!isMoving)
            {
                animalState["moveCooldown"] = Random.Range(2.0f, 5.0f);
            }
        }

        // Goal Search/Action Cycle. Interrupts Movement Cycle.
        switch (goal)
        {
            // Search for food or mate.
            case "mate":
            case "eat":
                string searchTarget = (goal == "eat" ? diet : gameObject.name);
                // Generate a sphere to detect entity colliders.
                //Debug.Log($"{gameObject.name} is searching for {searchTarget} to {goal}.");
                Collider[] detectedEntities = Physics.OverlapSphere(transform.position, perception);
                List<GameObject> validEntities = new List<GameObject>();
                foreach (Collider entity in detectedEntities)
                {
                    GameObject thisEntity = entity.gameObject;
                    // If this entity does NOT meet our goal OR this entity IS the searching animal..
                    if (!thisEntity.name.Contains(searchTarget)
                        || thisEntity == gameObject)
                    {
                        // ..skip this entity.
                        //Debug.Log($"{thisEntity.name} is not a good target. SKIPPING");
                        continue;
                    }
                    // Otherwise, this entity meets our goal. Mark target as valid.
                    validEntities.Add(thisEntity);
                    //Debug.Log($"{thisEntity.name} is going to {goal} {targetEntity.name}! ");
                    
                }
                if (validEntities.Count > 0) targetEntity = validEntities[Random.Range(0, validEntities.Count - 1)];
                break;
            // Wandering has no evident purpose.
            case "wander":
            default:
                break;
        }
    }

    // When an Animal eats, it moves to and destroys the target object.
    protected void Eat(GameObject target)
    {
        //Debug.Log($"Eating {target.name}!");
        // If we've reached the target entity and they still exist (haven't died or been eaten), then proceed.
        Vector3 targetPosition = new Vector3(
            target.transform.position.x,
            transform.position.y,
            target.transform.position.z
        );
        if (!GoTo(targetPosition))
        {
            //Debug.Log($"{target.name} has been eaten! NOM NOM NOM");
            if (targetEntity)
            {
                float targetCalories = target.GetComponent<Organism>().calories;
                calories += targetCalories;
                satiety = ((calories - caloricBase) / (calorieMax - caloricBase)) * 100;
                //Debug.Log($"{target.name} eaten by {gameObject.name} for {targetCalories} calories!");
                Destroy(target);
                targetEntity = null;
            }
        }
    }

    // When an Animal mates, it moves to the target Animal and a similar Animal instance is created.
    protected void Mate(GameObject target)
    {
        //Debug.Log($"Mating with {target.name}");
        // If we've reached the target entity and they still exist (haven't died or been eaten), then proceed.
        if (!GoTo(target.transform.position))
        {
            //Debug.Log($"{target.name} and {gameObject.name} made a baby!");
            if (targetEntity.name.Contains(gameObject.name))
            {
                calories -= (caloricBase * 2);
                satiety = ((calories - caloricBase) / (calorieMax - caloricBase)) * 100;
                Vector3 spawnPoint = new Vector3(
                    gameObject.transform.position.x + Random.Range(-2.5f, 2.5f),
                    gameObject.transform.position.y,
                    gameObject.transform.position.z + Random.Range(-2.5f, 2.5f)
                );
                int breedIndex = (gameObject.name.Contains("Rabbit") ? 1 : 2);
                Instantiate(GameManager.instance.organisms[breedIndex], spawnPoint, GameManager.instance.organisms[breedIndex].transform.rotation);
                targetEntity = null;
            }
        }
    }

    // When an Animal migrates, it moves to a random point on the edge of the map and destroys itself.
    protected void Migrate()
    {
        //Debug.Log($"Eating {target.name}!");
        if (!GoTo(geoPoints["actionPoint"]))
        {
            Destroy(gameObject);
        }
    }

    protected override IEnumerator Age()
    {
        yield return new WaitForSeconds(1 / GameManager.instance.gameSpeed);
        age += 1.0f;
        calories -= metabolism * 0.5f;
        satiety = ((calories - caloricBase) / (calorieMax - caloricBase)) * 100;
        canAge = true;
        //Debug.Log($"{gameObject.name} calories:{calories}");
    }

    // ================
    // HELPER FUNCTIONS
    // ================
    protected void InitializeAnimal()
    {
        //Debug.Log(canAge);
        geoPoints.Add("spawnPoint", Vector3.zero);
        geoPoints.Add("actionPoint", Vector3.zero);
        animalState.Add("moveCooldown", 0);
        // Record birthplace
        geoPoints["spawnPoint"] = transform.position;
    }

    protected Vector3 GenerateNavPoint(float navRadius)
    {
        Vector3 generatedPoint;
        while (true)
        {
            // Generate a new point
            generatedPoint = new Vector3
            (
                transform.position.x + Random.Range(-navRadius, navRadius),
                transform.position.y,
                transform.position.z + Random.Range(-navRadius, navRadius)
            );
            // Only allow points within the bounds set in GameManager.
            if (Mathf.Abs(generatedPoint.x) < GameManager.instance.groveBound 
                && Mathf.Abs(generatedPoint.z) < GameManager.instance.groveBound) break;
        }

        return generatedPoint;
    }

    protected Vector3 GenerateMigrationPoint()
    {
        Vector3 migrationPoint;
        if (Random.Range(1, 100) > 50)
        {
            migrationPoint = new Vector3(
                ((Random.Range(1, 100) > 50) ? -360 : 360),
                1,
                Random.Range(-360, 360)
            );
        } else
        {
            migrationPoint = new Vector3(
                Random.Range(-360, 360),
                1,
                ((Random.Range(1, 100) > 50) ? -360 : 360)
            );
        }
        return migrationPoint;
    }

    protected bool GoTo(Vector3 destination)
    {
        //Debug.Log($"Moving Object {name} to {destination} from {transform.position}");
        if (Vector3.Distance(transform.position, destination) > 0.5f)
        {
            transform.LookAt(destination);
            transform.Translate(Vector3.forward * (moveSpeed * GameManager.instance.gameSpeed) * Time.deltaTime);
            return true;
        }
        return false;
    }
}