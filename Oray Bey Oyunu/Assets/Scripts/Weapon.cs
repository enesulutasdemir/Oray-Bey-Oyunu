using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject particle;
    //Kamera (ekranýn merkezini bulmamýza yardýmcý olacaktýr)
    [SerializeField] protected GameObject cam;
    //Silahýn ateþleme modu
    protected bool auto = false;
    //Atýþlar arasýndaki aralýk ve süreyi sayan zamanlayýcý
    protected float cooldown = 0;
    protected float timer = 0;
    // Start is called before the first frame update
    private void Start()
    {
        timer = cooldown;
    }
    private void Update()
    {
        //Zamanlayýcýyý baþlatma
        timer += Time.deltaTime;
        //Eðer oyuncu farenin sol tuþuna basmýþsa, Shoot fonksiyonunu çaðýrýrýz
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    //Silahýn ateþlenip ateþlenemeyeceðinin kontrol edilmesi
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) || auto)
        {
            if (timer > cooldown)
            {
                OnShoot();
                timer = 0;
            }
        }
    }
    // Ve bu fonksiyon silah her vurulduðunda ne olacaðýný tanýmlayacaktýr. protected ve virtual deðiþtiricilere sahip olduðu için
    // bundan beslenen sýnýflar kendi atýþ mantýklarýný tanýmlayabileceklerdir
    protected virtual void OnShoot()
    {
    }
}
