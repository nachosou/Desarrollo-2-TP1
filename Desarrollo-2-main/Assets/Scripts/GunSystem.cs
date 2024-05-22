using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    [SerializeField] GameObject shootingCamera;

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

    bool shooting;
    bool readyToShoot;
    bool reloading;

    public GameObject scope;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    Vector3 direction;

    public GameObject bulletHole;
    public GameObject blood;

    [SerializeField] public TextMeshProUGUI bulletsMagazine;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        bulletsMagazine.text = (bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        if (IsShootingCameraActive())
        {
            if (allowButtonHold)
            {
                shooting = Input.GetMouseButton(0);
            }
            else
            {
                shooting = Input.GetMouseButtonDown(0);
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            {
                Reload();
            }

            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        direction = (scope.transform.forward - (shootingCamera.transform.position - scope.transform.position));

        if (Physics.Raycast(scope.transform.position, direction, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
                Instantiate(blood, rayHit.point, Quaternion.Euler(0, 180, 0));
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

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private bool IsShootingCameraActive()
    {
        if(shootingCamera.activeInHierarchy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
