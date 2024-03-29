﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePatterns;
    public GameObject[] greenGemPatterns;

    private float spawnTimer;
    public float spawnTimerLowerBound;
    public float spawnTimerUpperBound;
    public float lowerBoundIncreaseTo;
    public float upperBoundDecreaseTo;
    public float timeShiftCoefficient;

    private float originalSpawnTimerLowerBound;
    private float originalSpawnTimerUpperBound;

    public double gemSpawnProbability;

    private float timeElapsed;

    private void Start() {
        timeElapsed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().timeElapsed;

        originalSpawnTimerLowerBound = spawnTimerLowerBound;
        originalSpawnTimerUpperBound = spawnTimerUpperBound;
    }

    private void Update() {

        timeElapsed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().timeElapsed;

        if (spawnTimer <= 0) {


            double randomSpawnType = Random.value;

            if (randomSpawnType <= gemSpawnProbability) {
                Debug.Log("Green! " + randomSpawnType.ToString());
                int randomPattern = Random.Range(0, greenGemPatterns.Length);
                Instantiate(greenGemPatterns[randomPattern], transform.position, Quaternion.identity);
            } 
            else {
                int randomPattern = Random.Range(0, obstaclePatterns.Length);
                Instantiate(obstaclePatterns[randomPattern], transform.position, Quaternion.identity);
            }

            spawnTimer = Random.Range(spawnTimerLowerBound, spawnTimerUpperBound);
        }
        else {

            spawnTimer -= Time.deltaTime;
        }

        if (lowerBoundIncreaseTo > spawnTimerLowerBound) {
            spawnTimerLowerBound = originalSpawnTimerLowerBound + (timeShiftCoefficient * timeElapsed);
        }

        if (upperBoundDecreaseTo < spawnTimerUpperBound) {
            spawnTimerUpperBound = originalSpawnTimerUpperBound - (timeShiftCoefficient * timeElapsed);
        }
    }
}
