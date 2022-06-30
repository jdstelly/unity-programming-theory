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
        //Instantiate(organisms[1], new Vector3(0, 1, 0), organisms[1].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //ProgressTime();
    }

    void ProgressTime()
    {
        gameTime += 0.1f * gameSpeed;
    }
}
