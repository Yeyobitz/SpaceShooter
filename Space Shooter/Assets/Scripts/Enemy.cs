using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.0f;
    [SerializeField]
    private AudioClip _explosionSoundClip;

    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
        _boxCollider2D = GetComponent<BoxCollider2D>();
        if (_boxCollider2D == null)
        {
            Debug.LogError("The BoxCollider2D is NULL.");
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("The SpriteRenderer is NULL.");
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
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
        if (transform.position.y <= 6.47f)
        {
            _boxCollider2D.enabled = true;
        }
        else
        {
            _boxCollider2D.enabled = false;
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
                _player.AddScore(10);
            }
            AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
            _spriteRenderer.sortingLayerName = "Shield";
            _boxCollider2D.enabled = false;
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(gameObject, 2.0f);
        }
        else if (collision.tag == "Player")
        {
            if (player != null)
            {
                player.Damage();
            }
            AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
            _spriteRenderer.sortingLayerName = "Shield";
            _boxCollider2D.enabled = false;
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(gameObject, 2.0f);
        }
    }
}
