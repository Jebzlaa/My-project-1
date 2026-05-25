using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator; 
    public float speed = 6f;
    public float gravity = -9.81f;

    private Vector3 velocity;

    void Update()
    {
        // Pakai input standar yang dijamin jalan kalau settingannya "Both"
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            if (animator != null)
            {
                animator.SetFloat("Speed", 1f); 
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}