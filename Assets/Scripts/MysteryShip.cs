using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    public float speed = 5f;
    public float cycleTime = 30f;
    public int score = 300;

    private Vector2 leftDestination;
    private Vector2 rightDestination;
    private int direction = -1;
    private bool spawned;


    private void Start()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        leftDestination = new Vector2(leftEdge.x - 1f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 1f, transform.position.y);

        Despawn();
    }

    private void Update()
    {
        if (!spawned)
        {
            return;
        }

        if (direction == 1)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x >= rightDestination.x)
        {
            Despawn();
        }
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= leftDestination.x)
        {
            Despawn();
        }
    }

    [SerializeField] private AudioClip SFX;

    private void Spawn()
    {
        SFXManager.instance.PlaySFX(SFX, transform);

        direction *= -1;

        if (direction == 1)
        {
            transform.position = leftDestination;
        }
        else
        {
            transform.position = rightDestination;
        }

        spawned = true;
    }

    private void Despawn()
    {
        spawned = false;

        if (direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }

        Invoke(nameof(Spawn), cycleTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Despawn();
            GameManager.Instance.OnMysteryShipKilled(this);
        }
    }
}
