using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private float _rotateSpeed = 30.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private Player _player;
    private PolygonCollider2D _polygonCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 6.87f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        if (transform.position.y < -5.6f)
        {
            RespawnEnemy();
        }
    }
    private void RespawnEnemy()
    {
        float randomX = Random.Range(-8.0f, 8.0f);
        transform.position = new Vector3(randomX, 7.5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser" && transform.position.y <= 6.48)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
        else if (other.tag == "Player")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _player.Damage();
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
