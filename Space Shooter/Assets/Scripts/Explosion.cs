using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionSoundClip;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(_explosionSoundClip, transform.position);
        Destroy(this.gameObject, 2.5f);
    }

   
}
