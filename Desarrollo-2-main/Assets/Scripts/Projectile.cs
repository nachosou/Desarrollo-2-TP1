using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectilesFactory projectileFactory;

    public float speed { get; internal set; }
}
