using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private static readonly float PanSpeed = 20f;

    private static readonly float[] BoundsX = new float[] {-2.5f, 2.5f};
    private static readonly float[] BoundsY = new float[] {-4f, 10f};

    private Camera _cam;

    private Vector3 _lastPanPosition;


    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleMouse();
    }


    private void HandleMouse()
    {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Input.GetMouseButtonDown(0))
        {
            _lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }
    }

    private void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        var offset = _cam.ScreenToViewportPoint(_lastPanPosition - newPanPosition);
        var move = new Vector3(offset.x * PanSpeed, offset.y * PanSpeed, 0);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, BoundsX[0], BoundsX[1]);
        pos.y = Mathf.Clamp(pos.y, BoundsY[0], BoundsY[1]);
        transform.position = pos;

        // Cache the position
        _lastPanPosition = newPanPosition;
    }
}