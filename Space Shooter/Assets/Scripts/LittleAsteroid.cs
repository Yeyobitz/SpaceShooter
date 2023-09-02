using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAsteroid : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _rotationSpeed = 20.0f;
    [SerializeField] private int _scoreValue = 5;

    [SerializeField] private AudioClip _explosionSoundClip;
    [SerializeField] private GameObject _explosionPrefab;
    private Player _player;
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidBody2D;
    private int _randomDirection;

    void Start()
    {
        _randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        _rigidBody2D = GetComponent<Rigidbody2D>();
        if (_rigidBody2D == null)
        {
            Debug.LogError("The RigidBody2D is NULL.");
        }
        _circleCollider2D = GetComponent<CircleCollider2D>();
        if (_circleCollider2D == null)
        {
            Debug.LogError("The CircleCollider2D is NULL.");
        }
    }

     void Update()
    {
        AsteroidMovement();
    }

    private void AsteroidMovement()
    {
        Vector3 movement = new Vector3(_randomDirection, -1, 0) * _speed * Time.deltaTime;
        _rigidBody2D.MovePosition(transform.position + movement);
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        if (transform.position.y < -6.0f)
        {
            RespawnAsteroid();
        }

        if (transform.position.x > 9.72f)
        {
            transform.position = new Vector3(-9.72f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.72f)
        {
            transform.position = new Vector3(9.72f, transform.position.y, 0);
        }
    }

    void RespawnAsteroid()
    {
        float randomX = Random.Range(-4.0f, 4.0f);
        transform.position = new Vector3(randomX, 6.0f, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (collision.tag == "Laser" && transform.position.y <= 5.14f)
        {
            Destroy(collision.gameObject);
            if (_player != null)
            {
                _player.AddScore(_scoreValue);
            }
            AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _circleCollider2D.enabled = false;
            Destroy(collision.gameObject);
            _speed = 0;
            Destroy(gameObject, 0.25f);
        }
        else if (collision.tag == "Player")
        {
            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
            _circleCollider2D.enabled = false;
            _speed = 0;
            Destroy(gameObject, 0.25f);
        }
    }
    
    
}
