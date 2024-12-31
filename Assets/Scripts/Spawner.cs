using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;

    [Header("References")]
    public GameObject[] gameObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Invoke("Spawn", UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable(){
        CancelInvoke();
    }

    void Spawn(){
        GameObject randomObject=gameObjects[UnityEngine.Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject,transform.position, Quaternion.identity);
        Invoke("Spawn", UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
