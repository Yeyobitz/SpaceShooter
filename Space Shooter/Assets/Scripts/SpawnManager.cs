using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _littleAsteroidPrefab;
    //[SerializeField] private GameObject _spaceDogPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerUps;

    private bool _stopSpawningEnemy = false;
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
        yield return new WaitForSeconds(1.0f);

        while (!_stopSpawningEnemy)
        {
            Vector3 posToSpawn1 = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            Vector3 posToSpawn2 = new Vector3(Random.Range(-4.5f, 4.5f), 7.5f, 0);

            SpawnEnemy(_enemyPrefab, posToSpawn1);
            yield return new WaitForSeconds(Random.Range(3, 6));
            SpawnEnemy(_enemyPrefab, posToSpawn2);
            yield return new WaitForSeconds(Random.Range(1, 2));

            if (_player._score > 50)
            {
                SpawnEnemy(_littleAsteroidPrefab, posToSpawn2);
                yield return new WaitForSeconds(Random.Range(0, 0.3f));
            }

            // Añadir más patrones de generación de enemigos según el puntaje del jugador
            if (_player._score > 75)
            {
                // Generar un grupo de enemigos verticales
                for (int i = 0; i < 3; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
                    SpawnEnemy(_enemyPrefab, spawnPosition);
                }
                yield return new WaitForSeconds(2.0f);
            }
            if (_player._score > 100)
            {
                // Generar un grupo de enemigos horizontales
                for (int i = 0; i < 3; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
                    SpawnEnemy(_enemyPrefab, spawnPosition);
                }
                yield return new WaitForSeconds(2.0f);
            }
            if (_player._score > 125)
            {
                // Generar un grupo de asteroides
                for (int i = 0; i < 4; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
                    SpawnEnemy(_littleAsteroidPrefab, spawnPosition);
                    yield return new WaitForSeconds(0.5f); // Pequeño intervalo entre asteroides
                }
                yield return new WaitForSeconds(5.0f); // Esperar antes de generar otro grupo
            }
            if (_player._score > 150)
            {
                // Generate a group of enemies from the sides moving in the opposite direction
                for (int i = 0; i < 3; i++)
                {
                    Vector3 spawnPosition = Vector3.zero;
                    if (i == 0) // Spawn from the left side
                    {
                        spawnPosition = new Vector3(-9f, Random.Range(-4f, 4f), 0);
                    }
                    else if (i == 1) // Spawn from the right side
                    {
                        spawnPosition = new Vector3(9f, Random.Range(-4f, 4f), 0);
                    }
                    else if (i == 2) // Spawn from the bottom side
                    {
                        spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), -5.5f, 0);
                    }
                    SpawnEnemy(_enemyPrefab, spawnPosition);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            if (_player._score > 200)
            {
                Vector3 startPos = new Vector3(-6.0f, 7.5f, 0);
                float spacing = 2.0f;
                for (int i = 0; i < 5; i++)
                {
                    Vector3 spawnPosition = startPos + Vector3.right * (spacing * i);
                    SpawnEnemy(_enemyPrefab, spawnPosition);
                }
                yield return new WaitForSeconds(3.0f);
            }
            if (_player._score > 300)
            {
                float startY = Random.Range(5.0f, 7.5f);
                Vector3 startSpawn = new Vector3(-9.5f, startY, 0);
                Vector3 endSpawn = new Vector3(9.5f, startY, 0);
                int asteroidCount = 8;
                float spacing = (endSpawn.x - startSpawn.x) / (asteroidCount - 1);

                for (int i = 0; i < asteroidCount; i++)
                {
                    Vector3 spawnPosition = startSpawn + Vector3.right * (spacing * i);
                    SpawnEnemy(_littleAsteroidPrefab, spawnPosition);
                    yield return new WaitForSeconds(0.1f); // Pequeño intervalo entre asteroides
                }
                yield return new WaitForSeconds(5.0f); // Esperar antes de generar otro grupo
            }
            

        }
    }

    void SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }
    //void SpawnSpaceDog(Vector3 spawnPosition)
    //{
      //  Instantiate(_spaceDogPrefab, spawnPosition, Quaternion.identity);
    //}

    IEnumerator SpawnRoutinePowerUp()
    {
        yield return new WaitForSeconds(5.0f);

        while (!_stopSpawningPowerUp)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            int randomPowerUp = Random.Range(0, _powerUps.Length);
            Instantiate(_powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 21));

            // Ajustar la frecuencia de generación de power-ups según el puntaje del jugador
            if (_player._score > 150)
            {
                yield return new WaitForSeconds(Random.Range(5, 16));
            }
        }
    }
        

    public void OnPlayerDeath()
    {
        _stopSpawningEnemy = true;
        _stopSpawningPowerUp = true;
    }
}