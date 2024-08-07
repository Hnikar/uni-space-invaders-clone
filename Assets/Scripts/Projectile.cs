using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.up;
    public float speed = 20f;

    [SerializeField] private AudioClip projectileSFX;

    private new BoxCollider2D collider;

    private void Start(){
        SFXManager.instance.PlaySFX(projectileSFX, transform);
    }

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckCollision(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CheckCollision(other);
    }

    private void CheckCollision(Collider2D other)
    {
        Bunker bunker = other.gameObject.GetComponent<Bunker>();

        if (bunker == null || bunker.CheckCollision(collider, transform.position))
        {
            Destroy(gameObject);
        }
    }
}
