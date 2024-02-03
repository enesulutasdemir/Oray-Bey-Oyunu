using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    void Start()
    {
        //Atýþlar arasýnda gecikme yok
        cooldown = 0;
        //Bu otomatik bir silah deðil, yani her ateþ etmek istediðimizde ateþ düðmesine týklamamýz gerekiyor
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
                // 10 sayýsýný istediðiz þekilde deðiþtirebilirsiniz bir merminin vereceði zararý belirtiyor
                hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(10);
            }
            Destroy(gameBullet, 1);
        }
    }
}

