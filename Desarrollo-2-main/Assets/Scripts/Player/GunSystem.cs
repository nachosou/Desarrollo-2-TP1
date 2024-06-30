using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{
    [SerializeField] GameObject shootingCamera;
    [SerializeField] Transform pivot;

    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft;
    int bulletsShot;

    bool readyToShoot;
    bool reloading;

    public GameObject scope;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    Vector3 direction;

    public GameObject bulletHole;
    public GameObject blood;

    public ProjectilesSO projectileData;
    private ProjectilesFactory projectileFactory;
    public float projectileCooldown;
    private float lastFireTime;

    [SerializeField] public TextMeshProUGUI bulletsMagazine;
    [SerializeField] PlayerInput input;
    [SerializeField] GameObject shootPivot;
    [SerializeField] LineRenderer lineRenderer;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        input.currentActionMap.FindAction("Shoot").started += GunSystem_performed;
        input.currentActionMap.FindAction("Reload").started += GunSystem_started;
        input.currentActionMap.FindAction("ThrowProjectile").started += GunSystem_initiate;
        projectileFactory = new ProjectilesFactory(projectileData);
        lastFireTime = -projectileCooldown;
    }

    private void Update()
    {
        UpdateBulletsMagazine();
    }

    /// <summary>
    /// Updates the text displaying the number of bullets left in the magazine
    /// </summary>
    private void UpdateBulletsMagazine()
    {
        bulletsMagazine.text = (bulletsLeft + " / " + magazineSize);
    }

    private void OnDestroy()
    {
        if (input.currentActionMap != null)
        {
            input.currentActionMap.FindAction("Shoot").started -= GunSystem_performed;
            input.currentActionMap.FindAction("Reload").started -= GunSystem_started;
            input.currentActionMap.FindAction("ThrowProjectile").started -= GunSystem_initiate;
        }
    }

    /// <summary>
    /// Called when the shoot input action is performed
    /// </summary>
    private void GunSystem_performed(InputAction.CallbackContext obj)
    {
        CheckShoot();
    }

    /// <summary>
    /// Checks if the player is ready to shoot and performs the shooting action if possible
    /// </summary>
    private void CheckShoot()
    {
        if (IsShootingCameraActive())
        {
            if (readyToShoot && !reloading && bulletsLeft > 0)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }
        }
    }

    /// <summary>
    /// Called when the throwing projectile input action is performed
    /// </summary>
    private void GunSystem_initiate(InputAction.CallbackContext obj) 
    {
        FireProjectile();
    }

    /// <summary>
    /// Fires a projectile if the cooldown period has passed
    /// </summary>
    private void FireProjectile()
    {
        if (Time.time >= lastFireTime + projectileCooldown)
        {
            Vector3 position = transform.position + transform.forward;
            Quaternion rotation = transform.rotation;
            Projectile projectile = projectileFactory.CreateProjectile(position, rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            Vector3 force = transform.forward * projectileData.speed + Vector3.up * (projectileData.speed / 1.2f);
            rb.AddForce(force, ForceMode.VelocityChange);

            lastFireTime = Time.time;
        }
    }

    /// <summary>
    /// Shoots a bullet, handling raycasting and bullet effects
    /// </summary>
    public void Shoot()
    {
        readyToShoot = false;

        lineRenderer.SetPosition(0, shootPivot.transform.position);

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        direction = pivot.forward;

        if (Physics.Raycast(pivot.position, direction, out rayHit, range))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, rayHit.point);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
                var newBlood = Instantiate(blood, rayHit.point, Quaternion.identity);
                newBlood.transform.forward = rayHit.normal;
            }
            else
            {
                Instantiate(bulletHole, rayHit.point, Quaternion.Euler(0, 180, 0));
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShoot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    /// <summary>
    /// Resets the shooting ability
    /// </summary>
    private void ResetShoot()
    {
        readyToShoot = true;
        lineRenderer.enabled = false;
    }

    /// <summary>
    /// Called when the reload input action is performed
    /// </summary>
    private void GunSystem_started(InputAction.CallbackContext obj)
    {
        Reload();
    }

    /// <summary>
    /// Initiates the reloading process
    /// </summary>
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    /// <summary>
    /// Completes the reloading process
    /// </summary>
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    /// <summary>
    /// Checks if the shooting camera is active
    /// </summary>
    public bool IsShootingCameraActive()
    {
        return shootingCamera.activeInHierarchy;
    }
}
