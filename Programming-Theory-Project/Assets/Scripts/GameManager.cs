using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Create static GameManager Instance for ease of access in other classes.
    public static GameManager instance { get; private set; }
    public List<GameObject> organisms = new List<GameObject>();
    private float gameTime;
    private float gameSpeed;
    private bool canSpawnCarrots;
    private float spawnRateCarrots;
    public float groveBound { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0.0f;
        gameSpeed = 1.0f;
        groveBound = 360.0f;
        canSpawnCarrots = true;
        spawnRateCarrots = 3.0f;
        GenerateOrganisms(1, 10, 15);
        GenerateOrganisms(2, 1, 5);

    }

    // Update is called once per frame
    void Update()
    {
        //ProgressTime();
        if (canSpawnCarrots)
        {
            StartCoroutine(CarrotGenerator(spawnRateCarrots));
            canSpawnCarrots = false;
        }
    }

    void ProgressTime()
    {
        gameTime += 0.1f * gameSpeed;
    }

    void GenerateOrganisms(int organismIndex, int amount, float radius)
    {
        for (int i = 0; i < amount; i++)
        {
            float xPos = Random.Range(-radius, radius);
            float zPos = Random.Range(-radius, radius);
            Instantiate(organisms[organismIndex], new Vector3(xPos, 1, zPos), organisms[organismIndex].transform.rotation);
        }
    }

    IEnumerator CarrotGenerator(float spawnRate)
    {
        yield return new WaitForSeconds(spawnRate);
        GenerateOrganisms(0, 50, 340);
        canSpawnCarrots = true;
    }

}
