using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PulpitSpawner : MonoBehaviour
{
    public static PulpitSpawner Instance;

    public GameObject pulpitPrefab;
    private GameObject currentPulpit;
    private GameObject previousPulpit;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnInitialPulpit();
    }

    void update(){
        Start();
    }

    void SpawnInitialPulpit()
    {
        // Spawn the first pulpit and initialize it
        currentPulpit = Instantiate(pulpitPrefab, transform.position, Quaternion.identity);
        currentPulpit.GetComponent<Pulpit>().Initialize(
            GameDataLoader.Instance.gameData.pulpit_data.min_pulpit_destroy_time,
            GameDataLoader.Instance.gameData.pulpit_data.max_pulpit_destroy_time
        );
    }

    public void OnPulpitDestroyed()
    {
        // Calculate a random direction to spawn the next pulpit
        Vector3 randomDirection = GetRandomDirection();
        Vector3 spawnPosition = currentPulpit.transform.position + randomDirection;

        // Spawn the new pulpit and destroy the previous one after a delay
        previousPulpit = currentPulpit;
        currentPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);
        currentPulpit.GetComponent<Pulpit>().Initialize(
            GameDataLoader.Instance.gameData.pulpit_data.min_pulpit_destroy_time,
            GameDataLoader.Instance.gameData.pulpit_data.max_pulpit_destroy_time
        );

        Destroy(previousPulpit);
    }

    Vector3 GetRandomDirection()
    {
        // Randomly pick a direction: left, right, forward, or backward
        Vector3[] directions = new Vector3[]
        {
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };
        return directions[Random.Range(0, directions.Length)] * pulpitPrefab.transform.localScale.x;
    }
}
