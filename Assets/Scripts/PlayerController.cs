using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 _velocity = Vector2.zero;
    private Rigidbody2D _rb2d;

    // Start is called before the first frame update
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        UpdateVelocity();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Find all interactable objects nearby
            var nearest = FindNearbyObjects();
            if (nearest != null)
            {
                var script = nearest.GetComponentInChildren(typeof(IInteractable)) as IInteractable;
                script.Interact();
            }
        }
    }

    private Transform FindNearbyObjects()
    {
        var colliders = Physics2D.OverlapCircleAll(this.transform.position, 1);
        foreach (var collider in colliders)
        {
            if (collider.tag.Equals("Resource"))
            {
                Debug.Log("Found!!");
                return collider.transform;
            }
        }
        return null;
    }

    private void UpdateVelocity()
    {
        _velocity = _rb2d.velocity;
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var newVelocity = new Vector2(deltaX, deltaY);

        if (Mathf.Abs(newVelocity.magnitude - _velocity.magnitude) > Mathf.Epsilon)
        {
            _velocity = newVelocity;
            
        }
        _rb2d.velocity = _velocity;
    }

 
}