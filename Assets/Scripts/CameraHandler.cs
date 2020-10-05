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
        HandleKeyboard();
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var pos = GameController.Instance.playerController.transform.position;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
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

    private void HandleKeyboard()
    {
        bool moved = false;
        Vector3 pos = _cam.transform.position;
        float strength = PanSpeed / 5f * Time.deltaTime;
        if (Input.GetKey(KeyCode.K))
        {
            pos.y += strength;
            moved = true;
        } 
        if (Input.GetKey(KeyCode.J))
        {
            pos.y -= strength;
            moved = true;
        }
        if (Input.GetKey(KeyCode.L))
        {
            pos.x += strength;
            moved = true;
        }
        if (Input.GetKey(KeyCode.H))
        {
            pos.x -= strength;
            moved = true;
        }
    
        if (moved)
            _cam.transform.position = EnsureBounds(pos);
    }

    private void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        var offset = _cam.ScreenToViewportPoint(_lastPanPosition - newPanPosition);
        var move = new Vector3(offset.x * PanSpeed, offset.y * PanSpeed, 0);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        transform.position = EnsureBounds(transform.position);

        // Cache the position
        _lastPanPosition = newPanPosition;
    }

    private Vector3 EnsureBounds(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, BoundsX[0], BoundsX[1]);
        pos.y = Mathf.Clamp(pos.y, BoundsY[0], BoundsY[1]);
        return pos;
    }
}