using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject particle;
    //Kamera (ekran�n merkezini bulmam�za yard�mc� olacakt�r)
    [SerializeField] protected GameObject cam;
    //Silah�n ate�leme modu
    protected bool auto = false;
    //At��lar aras�ndaki aral�k ve s�reyi sayan zamanlay�c�
    protected float cooldown = 0;
    protected float timer = 0;
    // Start is called before the first frame update
    private void Start()
    {
        timer = cooldown;
    }
    private void Update()
    {
        //Zamanlay�c�y� ba�latma
        timer += Time.deltaTime;
        //E�er oyuncu farenin sol tu�una basm��sa, Shoot fonksiyonunu �a��r�r�z
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    //Silah�n ate�lenip ate�lenemeyece�inin kontrol edilmesi
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
    // Ve bu fonksiyon silah her vuruldu�unda ne olaca��n� tan�mlayacakt�r. protected ve virtual de�i�tiricilere sahip oldu�u i�in
    // bundan beslenen s�n�flar kendi at�� mant�klar�n� tan�mlayabileceklerdir
    protected virtual void OnShoot()
    {
    }
}
