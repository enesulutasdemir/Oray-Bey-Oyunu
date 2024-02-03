using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    void Start()
    {
        //At��lar aras�nda gecikme yok
        cooldown = 0;
        //Bu otomatik bir silah de�il, yani her ate� etmek istedi�imizde ate� d��mesine t�klamam�z gerekiyor
        auto = false;
        ammoCurrent = 20;
        ammoMax = 20;
        ammoBackPack = 60;
    }
    protected override void OnShoot()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            if (hit.collider.CompareTag("enemy"))
            {
                // 10 say�s�n� istedi�iz �ekilde de�i�tirebilirsiniz bir merminin verece�i zarar� belirtiyor
                hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(10);
            }
            Destroy(gameBullet, 1);
        }
    }
}

