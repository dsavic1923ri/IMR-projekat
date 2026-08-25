using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    
    // Add a slot to link your LSL Manager script
    [SerializeField] private LSLReceiver lslReceiver; 

    void Update()
    {
        if (lslReceiver != null)
        {
            // Read the live simulated numbers coming out of openViBE
            float openVibeSignal = lslReceiver.CurrentValue;

            // The openViBE sine wave smoothly oscillates between -1 and 1.
            // We map that directly to your movement system:
            if (openVibeSignal > 0.2f)       horizontal = 1f;  // Move Right
            else if (openVibeSignal < -0.2f) horizontal = -1f; // Move Left
            else                             horizontal = 0f;  // Stand Still
        }
        else
        {
            // Fallback to your old keyboard controls if LSL isn't connected or assigned
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}