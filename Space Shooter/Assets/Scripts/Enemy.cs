using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private int _scoreValue = 10;
    [SerializeField] private AudioClip _explosionSoundClip;
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] private float _fireRate =3.0f;
    private float _canFire = -1f;

    private Player _player;
    private Animator _animator;
    private PolygonCollider2D _polygonCollider2D;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        if (_animator == null)
        {
            Debug.LogError("The Animator is NULL.");
        }

        if (_polygonCollider2D == null)
        {
            Debug.LogError("The BoxCollider2D is NULL.");
        }

        if (_spriteRenderer == null)
        {
            Debug.LogError("The SpriteRenderer is NULL.");
        }

        if (_player._score > 100)
        {
            if (Time.time > _canFire)
                {
                    _fireRate = Random.Range(3.0f, 7.0f);
                    _canFire = Time.time + _fireRate;
                    GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                    Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
                    for (int i = 0; i < lasers.Length; i++)
                    {
                        lasers[i].AssignEnemyLaser();
                    }
               }
        }
    }

         void Update()
        {
            EnemyMovement();
        }

        void EnemyMovement()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y < -5.6f)
            {
                RespawnEnemy();
            }
        }

         void RespawnEnemy()
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }

         void HandleCollision(Collider2D collision)
        {

            if (collision.CompareTag("Laser") && transform.position.y <= 5.14f)
            {
                if (_player != null)
                {
                    _player.AddScore(_scoreValue);
                }
                Destroy(collision.gameObject);
                AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
                _spriteRenderer.sortingLayerName = "Shield";
                _polygonCollider2D.enabled = false;
                _animator.SetTrigger("OnEnemyDeath");
                _speed = 0;

                Destroy(gameObject, 2.0f);
            }
            else if (collision.CompareTag("Player"))
            {
                if (_player != null)
                {
                    _player.Damage();
                }

                AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
                _spriteRenderer.sortingLayerName = "Shield";
                _polygonCollider2D.enabled = false;
                _animator.SetTrigger("OnEnemyDeath");
                _speed = 0;
                Destroy(gameObject, 2.0f);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            HandleCollision(collision);
        }
    }




 
