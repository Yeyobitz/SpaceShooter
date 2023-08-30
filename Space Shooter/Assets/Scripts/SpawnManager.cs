using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _littleAsteroid;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUp;
    private bool _stopSpawningEnemy = false;
    [SerializeField]
    private bool _stopSpawningPowerUp = false;
    private Player _player;
    

    public void StartSpawning()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        StartCoroutine(SpawnRoutineEnemy());
        StartCoroutine(SpawnRoutinePowerUp());
    }

    IEnumerator SpawnRoutineEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        Vector3 posToSpawn1 = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
        Vector3 posToSpawn2 = new Vector3(Random.Range(-4.5f, 4.5f), 7.5f, 0);
        while (_stopSpawningEnemy == false)
        {
            GameObject newEnemy1 = Instantiate(_enemyPrefab, posToSpawn1, Quaternion.identity);
            newEnemy1.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3, 6));
            GameObject newEnemy2 = Instantiate(_enemyPrefab, posToSpawn2, Quaternion.identity);
            newEnemy2.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1, 2));
            if (_player._score > 100)
            {
                GameObject newEnemy3 = Instantiate(_littleAsteroid, posToSpawn2, Quaternion.identity);
                newEnemy3.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
            }
        }
        while (_stopSpawningEnemy == false)
        {
            GameObject newEnemy2 = Instantiate(_enemyPrefab, posToSpawn2, Quaternion.identity);
            newEnemy2.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1, 2));
            if (_player._score > 100)
            {
                GameObject newEnemy3 = Instantiate(_littleAsteroid, posToSpawn2, Quaternion.identity);
                newEnemy3.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
            }
        }

        // else if score is between 100 and 200
        // also spawn enemies with lasers
        // else if score is more than 200
        // also spawn asteroids
    }
    IEnumerator SpawnRoutinePowerUp()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawningPowerUp == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUp[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10,21));
        }
    }
    public void OnPlayerDeath()
    {
         _stopSpawningEnemy = true;
        _stopSpawningPowerUp = true;
    }
}   
