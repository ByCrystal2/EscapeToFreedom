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
            // Obje konumu ve oyuncu konumu arasýndaki vektörü hesapla
            Vector3 yon = PlayerManager.instance.player.transform.position - transform.position;

            // Yalnýzca yönün x bileþenini al
            float yonX = yon.normalized.x;

            // Yalnýzca x ekseni etrafýnda dönme için yeni rotasyonu belirle
            Quaternion yeniRotasyon = Quaternion.Euler(yonX, 0f, 0f);

            // Objeyi yeni rotasyona döndür
            transform.rotation = yeniRotasyon;
        }
    }
}
