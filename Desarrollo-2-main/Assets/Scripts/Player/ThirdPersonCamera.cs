using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform combatLookAt;
    [SerializeField] GameObject crossHair;

    [SerializeField] private GameObject basicCam;
    [SerializeField] private GameObject shootCam;

    private CameraStyle currentStyle;

    bool isAming;
    AnimationHandler animationHandler;
    [SerializeField] InputActionAsset actionMap;
    [SerializeField] PlayerInput input;

    private enum CameraStyle
    {
        Basic,
        Combat
    }

    private void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        animationHandler = player.GetComponent<AnimationHandler>();
        input.currentActionMap.FindAction("Aim").started += ShootingCamera_started;
        input.currentActionMap.FindAction("Aim").canceled += ShootingCamera_canceled;
    }

    private void OnDisable()
    {     
        if (actionMap != null)
        {
            var aimAction = actionMap.FindAction("Aim");
            if (aimAction == null)
                Debug.LogError($"{nameof(aimAction)} is null!");
            else
            {
                aimAction.started -= ShootingCamera_started;
                aimAction.canceled -= ShootingCamera_canceled;
            }
        }
        else
            Debug.LogError($"{nameof(actionMap)} is null!");
    }


    private void Update()
    {
        UpdateOrientation();
        UpdateCombatLookAt();
    }

    /// <summary>
    /// Updates the orientation of the camera to face the player and combat look at direction
    /// </summary>
    private void UpdateOrientation()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        orientation.forward = dirToCombatLookAt.normalized;

        playerObj.forward = dirToCombatLookAt.normalized;
    }

    /// <summary>
    /// Updates the direction the combat look at object is facing
    /// </summary>
    private void UpdateCombatLookAt()
    {
        combatLookAt.forward = Camera.main.transform.forward - shootCam.transform.position + combatLookAt.position;
    }

    public void DeactivateMap()
    {
        if (actionMap != null)
        {
            actionMap.FindAction("Aim").started -= ShootingCamera_started;
            actionMap.FindAction("Aim").canceled -= ShootingCamera_canceled;
        }
    }

    /// <summary>
    /// Handles the event when the aim action is started
    /// </summary>
    private void ShootingCamera_started(InputAction.CallbackContext obj)
    {
        SwitchCameraStyle(CameraStyle.Combat);
    }

    /// <summary>
    /// Handles the event when the aim action is canceled
    /// </summary>
    private void ShootingCamera_canceled(InputAction.CallbackContext obj)
    {
        SwitchCameraStyle(CameraStyle.Basic);
    }

    /// <summary>
    /// Switches the camera style between basic and combat
    /// </summary>
    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        if (newStyle == CameraStyle.Basic)
        {
            basicCam.SetActive(true);
            shootCam.SetActive(false);
            crossHair.SetActive(false);
            isAming = false;
        }

        if (newStyle == CameraStyle.Combat)
        {
            shootCam.SetActive(true);
            basicCam.SetActive(false);
            crossHair.SetActive(true);
            isAming = true;
        }

        animationHandler.SetAmingBoolAnimation(isAming);

        currentStyle = newStyle;
    }
}
