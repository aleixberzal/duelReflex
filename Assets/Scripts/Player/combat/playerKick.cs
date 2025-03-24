using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [Header("Kick Settings")]
    [SerializeField] private float kickRange = 1.5f;
    [SerializeField] private float kickForce = 15f;
    [SerializeField] private KeyCode kickKey = KeyCode.E;
    [SerializeField] private Transform kickOrigin;
    [SerializeField] private float kickCooldown = 0.5f;
    [SerializeField] private float kickWidth = 0.5f; 
    /*To add a cooldown for the kicks*/
    private float lastKickTime;
    private bool isFacingRight = true;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /*If there is no kick origin we make sure to instantiate it to its default position*/
        if (kickOrigin == null) kickOrigin = transform;
    }

    private void Update()
    {
        /*It kicks facing the direction the player is looking*/
        if (Input.GetAxisRaw("Horizontal") > 0) isFacingRight = true;
        else if (Input.GetAxisRaw("Horizontal") < 0) isFacingRight = false;
        /*If the kick key is pressed and the cooldown has already passed we kick and add a cooldown again*/
        if (Input.GetKeyDown(kickKey) && Time.time > lastKickTime + kickCooldown)
        {
            TryKick();
            lastKickTime = Time.time;
        }
    }

    private void TryKick()
    {
        /*Depending on the direction our player is looking the vector direction will be left or right */
        Vector2 kickDirection = isFacingRight ? Vector2.right : Vector2.left;

        /*We create a box to detect any body in its area and kick it*/
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            kickOrigin.position,
            /*The size of the detection box will be the kick vector*/
            new Vector2(kickWidth, kickWidth), 
            /*No angle attribute*/
            0f,
            kickDirection,
            kickRange
        );

        foreach (RaycastHit2D hit in hits)
        {
            /*If the hit collides with ourselves we skip it with continue*/
            if (hit.collider.gameObject == gameObject) continue;

            Rigidbody2D hitRb = hit.collider.GetComponent<Rigidbody2D>();
            if (hitRb != null)
            {
                /*If there is a rigidBody, we apply force to it, if there is not we apply ourselves an optional recoil*/
                hitRb.AddForce(kickDirection * kickForce, ForceMode2D.Impulse);


                if (rb != null)
                {
                    rb.AddForce(-kickDirection * kickForce * 0.2f, ForceMode2D.Impulse);
                }
            }
        }
    }


}