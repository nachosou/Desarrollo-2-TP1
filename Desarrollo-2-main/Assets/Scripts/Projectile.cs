using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float gravity = -9.8f;
    private Vector3 velocity;

    private void Start()
    {
        velocity = transform.forward * speed;
    }

    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}
