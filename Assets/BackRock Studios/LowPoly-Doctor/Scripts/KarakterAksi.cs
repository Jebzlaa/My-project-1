using UnityEngine;

public class KarakterAksi : MonoBehaviour
{
    // Tempat naruh komponen Animator si athur
    public Animator animator;

    void Update()
    {
        // Kalau lu pencet tombol E di keyboard
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Unity bakal nyuruh Animator buat jalanin parameter "Pick Up"
            animator.SetTrigger("Pick Up");
        }
    }
}