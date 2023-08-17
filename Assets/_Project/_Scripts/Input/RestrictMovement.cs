using UnityEngine;

public class RestrictMovement : MonoBehaviour
{
    public Transform staticSprite;
    public float smoothSpeed = 5f; // Adjust the speed of movement

    private Vector2 movementBoundsMin;
    private Vector2 movementBoundsMax;

    private Vector3 targetPosition;

    private void Start()
    {
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        SpriteRenderer staticSpriteRenderer = staticSprite.GetComponent<SpriteRenderer>();
        SpriteRenderer movingSpriteRenderer = GetComponent<SpriteRenderer>();

        if (staticSpriteRenderer == null || movingSpriteRenderer == null)
        {
            Debug.LogError("Both sprites must have SpriteRenderer components.");
            return;
        }

        Vector3 staticSpriteBounds = staticSpriteRenderer.bounds.extents;
        Vector3 movingSpriteBounds = movingSpriteRenderer.bounds.extents;

        movementBoundsMin = staticSprite.position - staticSpriteBounds + movingSpriteBounds;
        movementBoundsMax = staticSprite.position + staticSpriteBounds - movingSpriteBounds;

        // Initialize target position to current position
        targetPosition = transform.position;
    }
    public Vector3 offset;
    public GameObject go;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 desiredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            desiredPosition.z = 0f;
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, movementBoundsMin.x, movementBoundsMax.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, movementBoundsMin.y, movementBoundsMax.y);

            if (!go.activeInHierarchy) go.SetActive(true);
            PaintController.isControlEnable = true;


            targetPosition = desiredPosition + offset;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PaintController.isControlEnable = false;
            go.SetActive(false);
        }

        // Smoothly move the sprite towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
