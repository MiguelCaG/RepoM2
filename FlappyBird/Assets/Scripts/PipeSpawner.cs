using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipe;

    public List<GameObject> pipesPool;
    private int maxPipes = 5;
    private int pipeCount = 0;

    void Start()
    {
        BirdController.Score += SpawnPipe;
        //SpawnPipe(); // If no pipe was created on the Editor
        pipeCount = pipesPool.Count;
    }

    void Update()
    {

    }

    private void SpawnPipe()
    {
        GameObject newPipe;

        if (pipesPool.Count < maxPipes)
        {
            newPipe = Instantiate(pipe);
            pipesPool.Add(newPipe);
            pipeCount++;
        }
        else
        {
            newPipe = pipesPool[pipeCount % maxPipes];
            newPipe.transform.position = transform.position;
            pipeCount++;
        }

        Vector3 upperPipePos = newPipe.transform.GetChild(0).gameObject.transform.position;
        Vector3 lowerPipePos = newPipe.transform.GetChild(1).gameObject.transform.position;
        newPipe.transform.GetChild(0).gameObject.transform.position = new Vector3(upperPipePos.x, Random.Range(6.5f, 9.5f), upperPipePos.z);
        newPipe.transform.GetChild(1).gameObject.transform.position = new Vector3(lowerPipePos.x, Random.Range(-6.5f, -9.5f), lowerPipePos.z);
    }

    private void OnDisable()
    {
        BirdController.Score -= SpawnPipe;
    }
}
