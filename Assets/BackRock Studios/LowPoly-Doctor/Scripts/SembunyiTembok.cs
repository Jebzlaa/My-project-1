using UnityEngine;

public class SembunyiTembok : MonoBehaviour
{
    public Transform targetDokter; // Isi dengan si athur
    private GameObject tembokLama;

    void Update()
    {
        if (targetDokter == null) return;

        // Arah raycast dari kamera menuju si Dokter
        Vector3 arah = targetDokter.position - transform.position;
        float jarak = Vector3.Distance(transform.position, targetDokter.position);
        RaycastHit hit;

        // Tembak garis sensor gaib dari kamera ke Dokter
        if (Physics.Raycast(transform.position, arah, out hit, jarak))
        {
            // Kalau yang ketabrak sensor adalah tembok (bukan si Dokter sendiri)
            if (hit.collider.gameObject != targetDokter.gameObject && hit.collider.name != "Lantai")
            {
                GameObject tembokSekarang = hit.collider.gameObject;

                // Munculin lagi tembok yang sebelumnya diumpetin
                if (tembokLama != null && tembokLama != tembokSekarang)
                {
                    SetTembokAktif(tembokLama, true);
                }

                // Umpetin tembok yang lagi ngalangin sekarang
                SetTembokAktif(tembokSekarang, false);
                tembokLama = tembokSekarang;
            }
            else
            {
                // Kalau gak ada yang ngalangin, balikin semua tembok jadi muncul
                if (tembokLama != null)
                {
                    SetTembokAktif(tembokLama, true);
                    tembokLama = null;
                }
            }
        }
    }

    void SetTembokAktif(GameObject obj, bool status)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = status; // Matikan/nyalakan visualnya doang, fisiknya tetep padat!
        }
    }
}