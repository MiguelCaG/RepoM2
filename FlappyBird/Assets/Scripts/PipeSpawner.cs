using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;

    private List<GameObject> pipesPool = new List<GameObject>();
    private int maxPipes = 5;
    private int pipeCount = 0;
    private bool maxSpeed = false;

    void Start()
    {
        BirdController.Score += SpawnPipe;

        for (int i = 0; i < maxPipes; i++)
        {
            GameObject newPipe = Instantiate(pipePrefab, Vector3.right * (10 + 8 * i), Quaternion.identity);
            pipesPool.Add(newPipe);
        }
    }

    void Update()
    {

    }

    private void SpawnPipe()
    {
        if (pipeCount > 1)
        {
            if (pipeCount % maxPipes == 0 && !maxSpeed)
            {
                foreach (var pipe in pipesPool)
                {
                    pipe.GetComponent<PipeController>().speed -= 1f;
                    maxSpeed = pipe.GetComponent<PipeController>().speed == -15f;
                }
            }

            GameObject unusedPipe = pipesPool[(pipeCount - 2) % maxPipes];
            unusedPipe.transform.position = transform.position;

            Vector3 upperPipePos = unusedPipe.transform.GetChild(0).gameObject.transform.position;
            Vector3 lowerPipePos = unusedPipe.transform.GetChild(1).gameObject.transform.position;
            unusedPipe.transform.GetChild(0).gameObject.transform.position = new Vector3(upperPipePos.x, Random.Range(6f, 8f), upperPipePos.z);
            unusedPipe.transform.GetChild(1).gameObject.transform.position = new Vector3(lowerPipePos.x, Random.Range(-6f, -8f), lowerPipePos.z);
        }

        pipeCount++;
    }

    private void OnDisable()
    {
        BirdController.Score -= SpawnPipe;
    }
}
