using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject basicCam;
    public GameObject shootCam;

    private CameraStyle currentStyle;

    private enum CameraStyle
    {
        Basic,
        Combat
    }

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            SwitchCameraStyle(CameraStyle.Combat);
        }
        else
        {
            SwitchCameraStyle(CameraStyle.Basic);
        }

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        orientation.forward = dirToCombatLookAt.normalized;

        playerObj.forward = dirToCombatLookAt.normalized;

        combatLookAt.forward = Camera.main.transform.forward - shootCam.transform.position + combatLookAt.position;
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        basicCam.SetActive(false);
        shootCam.SetActive(false);

        if (newStyle == CameraStyle.Basic)
        {
            basicCam.SetActive(true);
        }

        if (newStyle == CameraStyle.Combat)
        {
            shootCam.SetActive(true);
        }

        currentStyle = newStyle;
    }
}
