using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private InputActions inputActions;
    private InputAction move;
    private InputAction rotate;
    private InputAction zoom;
 
    [SerializeField] private float movementSpeed;
    [SerializeField] private float zoomAmount;
    [SerializeField] private float movementTime;
    [SerializeField] private float maxZoomIn;
    [SerializeField] private float maxZoomOut;

    private Camera mainCamera;
    private float movementMultiplier;
    private Vector3 newPosition;
    private Vector3 newZoom;
    private Quaternion newRotation;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;
    private Vector3 mousePosition;

    private void OnEnable()
    {
        inputActions = new InputActions();
        move = inputActions.Camera.Move;
        rotate = inputActions.Camera.Rotate;
        zoom = inputActions.Camera.Zoom;
        inputActions.Enable();

        mainCamera = GetComponentInChildren<Camera>();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = mainCamera.transform.localPosition;
    }

    private void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    private void HandleMouseInput()
    {
        mousePosition = Mouse.current.position.ReadValue();
        movementMultiplier = mainCamera.transform.position.y;

        if (rotate.WasPressedThisFrame())
        {
            rotateStartPosition = mousePosition;
        }
        if (rotate.IsPressed())
        {
            rotateCurrentPosition = mousePosition;

            var difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }

        var zoomNormalized = zoom.ReadValue<float>();
        newZoom.y += -zoomAmount * zoomNormalized;
        newZoom.z += zoomAmount * zoomNormalized;

        if(newZoom.y < maxZoomIn)
        {
            newZoom.y = maxZoomIn;
            newZoom.z = -maxZoomIn;
        }
        if(newZoom.y > maxZoomOut)
        {
            newZoom.y = maxZoomOut;
            newZoom.z = -maxZoomOut;
        }

        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, newZoom, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }

    private void HandleMovementInput()
    {
        var moveNormalized = move.ReadValue<Vector3>();
        newPosition += movementMultiplier/100 * movementSpeed * (transform.forward * moveNormalized.z + transform.right * moveNormalized.x);
     
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }
}