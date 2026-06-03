using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    [Header("Movement Speeds")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    private float currentSpeed;

    [Header("Gravity")]
    public float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        // KUNCI PERGERAKAN: Cek apakah si dokter lagi muter animasi mengambil barang
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("mixamo_com"))
        {
            ApplyGravity();
            return; 
        }

        // 1. Logika Tombol Shift (Lari / Jalan)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
            animator.SetInteger("Run", 2);
        }
        else
        {
            currentSpeed = walkSpeed;
            animator.SetInteger("Run", 0);
        }

// 2. Logika Input WASD Mutlak Global
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        
        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 12f);

            // --- TAMBAHKAN BARIS INI: Kirim perintah jalan ke Animator ---
            animator.SetFloat("Speed", 1f); 
        }
        else
        {
            // --- TAMBAHKAN BARIS INI: Balik ke idle kalau gak pencet keyboard ---
            animator.SetFloat("Speed", 0f); 
        }

        ApplyGravity();
    }

    // Fungsi pembantu buat nyatuin logika gravitasi (Udah gua benerin jadi ApplyGravity)
    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}