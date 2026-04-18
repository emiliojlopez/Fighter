using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_Spawner : MonoBehaviour
{
    public GameObject Enemy_2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy_2", 1f, 5f);
    }
    //Function that controls enemy spawn rates
    void SpawnEnemy_2()
    {
        //the Main Camera is tagged as cam
        Camera cam = Camera.main;

        //Establishes full vertical range
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        
        //Enemies spawn outside the range of view
        float spawnX = topRight.x + 2f;
        //Enemies have a random spawn with a vertical value
        float spawnY = Random.Range(bottomLeft.y, topRight.y);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        
        //Spawns the enemy
        Instantiate(Enemy_2, spawnPosition, Quaternion.identity);
    }
}
