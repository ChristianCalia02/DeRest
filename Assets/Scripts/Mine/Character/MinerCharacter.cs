using UnityEngine;
using Core;

namespace Mine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MinerCharacter : ControllableBase
    {
        [Header("Movimento")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float jumpForce = 9f;

        [Header("Ground check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.15f;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody2D rb;
        private float moveInput;
        private bool jumpRequested;
        private bool facingRight = true;

        private void Awake() => rb = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            bool isGrounded = groundCheck != null &&
                               Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (jumpRequested && isGrounded)
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpRequested = false;

            if (moveInput > 0.01f && !facingRight) Flip();
            else if (moveInput < -0.01f && facingRight) Flip();
        }

        public void Move(float horizontal)
        {
            moveInput = Mathf.Clamp(horizontal, -1f, 1f);
        }
        public void RequestJump() => jumpRequested = true;

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 s = transform.localScale;
            s.x *= -1;
            transform.localScale = s;
        }
    }
}