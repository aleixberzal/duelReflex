using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [Header("Kick Settings")]
    [SerializeField] private float kickRange = 1.5f;
    [SerializeField] private float kickForce = 15f;
    /*Both public variables to diferenciate both player's inputs and direction each one are looking at*/
    [SerializeField] private KeyCode kickKey = KeyCode.E;
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private Transform kickOrigin;
    [SerializeField] private float kickCooldown = 0.5f;
    [SerializeField] private float kickWidth = 0.5f;

    private float lastKickTime;
    private bool isFacingRight = true;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /*In case the origin point where our players will kick will be instantiated*/
        if (kickOrigin == null) kickOrigin = transform;
    }

    private void Update()
    {
        /*We read the direction the player is looking at with the horizontal axis to then kick that direction*/
        float moveInput = Input.GetAxisRaw(horizontalAxis);
        if (moveInput > 0) isFacingRight = true;
        else if (moveInput < 0) isFacingRight = false;

        /*We read the kick with its own key to distinguish between both players*/
        if (Input.GetKeyDown(kickKey) && Time.time > lastKickTime + kickCooldown)
        {
            TryKick();
            lastKickTime = Time.time;
        }
    }

    private void TryKick()
    {
        /*Depending on the direction our player is looking we make the kick left or right*/
        Vector2 kickDirection = isFacingRight ? Vector2.right : Vector2.left;
        /*We make our kick a box of a rational scale*/
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            kickOrigin.position,
            new Vector2(kickWidth, kickWidth),
            /*It is straight so we put no angle on the box*/
            0f,
            kickDirection,
            kickRange
        );
        /*Depending on what we are hitting it will apply the force or not (only if it detects a rigidBody being hit)*/
        foreach (RaycastHit2D hit in hits)
        {
            Rigidbody2D hitRb = hit.collider.GetComponent<Rigidbody2D>();
            if (hitRb != null)
            {
                hitRb.AddForce(kickDirection * kickForce, ForceMode2D.Impulse);
                if (rb != null)
                {
                    rb.AddForce(-kickDirection * kickForce * 0.2f, ForceMode2D.Impulse);
                }
            }
        }
    }
}
