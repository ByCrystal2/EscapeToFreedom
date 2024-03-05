using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private bool _IsLook = false;
    public void SetLook(bool _isLook)
    {
        _IsLook = _isLook;
    }
    private void LateUpdate()
    {
        if (_IsLook)
        {
            // Obje konumu ve oyuncu konumu aras�ndaki vekt�r� hesapla
            Vector3 yon = PlayerManager.instance.player.transform.position - transform.position;

            // Yaln�zca y�n�n x bile�enini al
            float yonX = yon.normalized.x;

            // Yaln�zca x ekseni etraf�nda d�nme i�in yeni rotasyonu belirle
            Quaternion yeniRotasyon = Quaternion.Euler(yonX, 0f, 0f);

            // Objeyi yeni rotasyona d�nd�r
            transform.rotation = yeniRotasyon;
        }
    }
}
