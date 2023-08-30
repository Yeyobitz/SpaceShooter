using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAsteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private AudioClip _explosionSoundClip;
    [SerializeField]
    private GameObject _explosionPrefab;
    private Player _player;
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidBody2D;
    private int randomDirection;
    void Start()
    {
        randomDirection = Random.Range(0, 2) * 2 - 1; // Generates either -1 or 1

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        _rigidBody2D = GetComponent<Rigidbody2D>();
        if (_rigidBody2D == null)
        {
            Debug.LogError("The Rigidbody2D is NULL.");
        }
        _circleCollider2D = GetComponent<CircleCollider2D>();
        if (_circleCollider2D == null)
        {
            Debug.LogError("The CircleCollider2D is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }
    void AsteroidMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Vector3 movement = new Vector3(randomDirection, 1, 0) * _speed * Time.deltaTime;
        _rigidBody2D.MovePosition(transform.position + movement);

        if (transform.position.y < -5.6f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
        if (transform.position.x >= 9.72f)
        {
            transform.position = new Vector3(-9.72f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.72f)
        {
            transform.position = new Vector3(9.72f, transform.position.y, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            if (_player != null)
            {
                _player.AddScore(5);
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
