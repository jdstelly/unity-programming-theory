using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Create static GameManager Instance for ease of access in other classes.
    // ENCAPSULATION
    public static GameManager instance { get; private set; }
    public List<GameObject> organisms = new List<GameObject>();
    private float gameTime;
    // ENCAPSULATION
    public float gameSpeed { get; private set; }
    private bool canSpawnCarrots;
    private bool canCheckWorld;
    private bool canPopulateWorld;
    private float spawnRateCarrots;
    // ENCAPSULATION
    public float groveBound { get; private set; }

    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    public TextMeshProUGUI textCarrots;
    public TextMeshProUGUI textRabbits;
    public TextMeshProUGUI textWolves;

    private GameObject[] totalCarrots;
    private GameObject[] totalRabbits;
    private GameObject[] totalWolves;


    private void Awake()
    {
        instance = this;
        canSpawnCarrots = true;
        canCheckWorld = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        canSpawnCarrots = true;
        canCheckWorld = true;
        gameTime = 0.0f;
        gameSpeed = 1.0f;
        groveBound = 180.0f;
        spawnRateCarrots = 180.0f;
        GenerateOrganisms(1, 20, 160);
        GenerateOrganisms(2, 2, 160);
        textCarrots.SetText($"Carrots: Loading..");
        textRabbits.SetText($"Rabbits: Loading..");
        textWolves.SetText($"Wolves: Loading..");
    }

    // Update is called once per frame
    void Update()
    {
        PopulateWorld();
        
        if (canCheckWorld)
        {
            //Debug.Log("Checking world");
            StartCoroutine(CheckWorld());
            canCheckWorld = false;
        }
        //Debug.Log(canPopulateWorld);

        ProgressTime();
    }

    void ProgressTime()
    {
        gameTime += 1.0f * gameSpeed;
    }

    public void GenerateOrganisms(int organismIndex, int amount, float radius)
    {
        for (int i = 0; i < amount; i++)
        {
            float xPos = Random.Range(-radius, radius);
            float zPos = Random.Range(-radius, radius);
            Instantiate(organisms[organismIndex], new Vector3(xPos, (organismIndex == 0 ? 0 : 1), zPos), organisms[organismIndex].transform.rotation);
        }
    }

    void PopulateWorld()
    {
        // Carrots
        if (canSpawnCarrots)
        {
            canSpawnCarrots = false;
            StartCoroutine(CarrotGenerator());
        }
    }

    IEnumerator CheckWorld()
    {
        //Debug.Log("Counting Organisms");
        totalCarrots = GameObject.FindGameObjectsWithTag("carrot");
        totalRabbits = GameObject.FindGameObjectsWithTag("rabbit");
        totalWolves = GameObject.FindGameObjectsWithTag("wolf");
        if (!canPopulateWorld) canPopulateWorld = true;
        //Debug.Log($"Carrots: {totalCarrots.Length}, Rabbits: {totalRabbits.Length}, Wolves: {totalWolves.Length}");
        textCarrots.SetText($"Carrots: {totalCarrots.Length}");
        textRabbits.SetText($"Rabbits: {(totalRabbits.Length > 0 ? totalRabbits.Length : "Extinct")}");
        textWolves.SetText($"Wolves: {(totalWolves.Length > 0 ? totalWolves.Length : "Extinct")}");
        yield return new WaitForSeconds(1 / gameSpeed);
        canCheckWorld = true;
    }

    IEnumerator CarrotGenerator()
    {
        GenerateOrganisms(0, Random.Range(100, 200), 160);
        yield return new WaitForSeconds(spawnRateCarrots / gameSpeed);
        canSpawnCarrots = true;
    }

    public void SetCamera(int cameraID)
    {
        switch (cameraID)
        {
            case 1:
                Camera1.gameObject.SetActive(true);
                Camera2.gameObject.SetActive(false);
                Camera3.gameObject.SetActive(false);
                break;
            case 2:
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(true);
                Camera3.gameObject.SetActive(false);
                break;
            case 3:
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(false);
                Camera3.gameObject.SetActive(true);
                break;
            default:
                Camera1.gameObject.SetActive(true);
                Camera2.gameObject.SetActive(false);
                Camera3.gameObject.SetActive(false);
                break;
        }
    }

    public void SetGameSpeed(float speed)
    {
        if (gameSpeed >= 1.0f && gameSpeed < 51f)
        gameSpeed = speed;
    }

    public void spawnUI(string organism)
    {
        switch (organism)
        {
            case "carrots":
                GenerateOrganisms(0, 100, 160);
                break;
            case "rabbits":
                GenerateOrganisms(1, 2, 160);
                break;
            case "wolves":
                GenerateOrganisms(2, 2, 160);
                break;
            default:
                break;
        }
    }

    public void destroyUI(string organism)
    {
        switch (organism)
        {
            case "carrots":
                foreach (GameObject carrot in totalCarrots)
                {
                    Destroy(carrot);
                }
                break;
            case "rabbits":
                foreach (GameObject rabbit in totalRabbits)
                {
                    Destroy(rabbit);
                }
                break;
            case "wolves":
                foreach (GameObject wolf in totalWolves)
                {
                    Destroy(wolf);
                }
                break;
            default:
                break;
        }
    }
}
