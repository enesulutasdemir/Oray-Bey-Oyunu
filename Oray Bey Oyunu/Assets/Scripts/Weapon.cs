using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    protected int ammoCurrent;
    //�arj�r�n maksimum kapasitesi
    protected int ammoMax;
    //yedekteki mermi miktar�
    protected int ammoBackPack;
    //aray�zde g�z�kecek yaz� i�in bir de�i�ken
    [SerializeField] TMP_Text ammoText;
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioClip bulletSound, noBulletSound, reload;
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
        AmmoTextUpdate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            //�arj�r�m�z dolu de�ilse VEYA yedekte en az bir mermimiz varsa
            if (ammoCurrent != ammoMax || ammoBackPack != 0)
            {
                ///yeniden y�kleme i�levinin hafif bir gecikmeyle etkinle�tirilmesi
                //gecikmeyi istedi�iniz herhangi bir say�ya ayarlayabilirsiniz
                shoot.PlayOneShot(reload);
                Invoke("Reload", 1);
            }
        }
    }
    //Silah�n ate�lenip ate�lenemeyece�inin kontrol edilmesi
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) || auto)
        {
            if (timer > cooldown)
            {
                if (ammoCurrent > 0)
                {
                    OnShoot();
                    timer = 0;
                    ammoCurrent = ammoCurrent - 1;
                    shoot.PlayOneShot(bulletSound);
                    shoot.pitch = Random.Range(1f, 1.5f);
                }
                else
                {
                    shoot.PlayOneShot(noBulletSound);
                }
            }
        }
    }
    // Ve bu fonksiyon silah her vuruldu�unda ne olaca��n� tan�mlayacakt�r. protected ve virtual de�i�tiricilere sahip oldu�u i�in
    // bundan beslenen s�n�flar kendi at�� mant�klar�n� tan�mlayabileceklerdir
    protected virtual void OnShoot()
    {
    }
    private void AmmoTextUpdate()
    {
        ammoText.text = ammoCurrent + " / " + ammoBackPack;
    }
    private void Reload()
    {
        //bir de�i�ken tan�mlamak ve �arj�re eklememiz gereken mermi say�s�n� hesaplamak
        int ammoNeed = ammoMax - ammoCurrent;
        //Elimizdeki yedek mermi miktar�, yeniden doldurmak i�in gereken mermi miktar�na e�it veya daha fazla ise
        if (ammoBackPack >= ammoNeed)
        {
            //�htiya� duyulan mermi say�s�n�n yedeklerden ��kar�lmas�
            ammoBackPack -= ammoNeed;
            //�arj�re gerekli say�da mermi eklemek
            ammoCurrent += ammoNeed;
        }
        //aksi takdirde ( yedeklerde tam bir yeniden doldurma i�in gerekenden daha az mermi varsa)
        else
        {
            //t�m yedek cephanemizi �arj�re ekleyemek
            ammoCurrent += ammoBackPack;
            //yedek cephaneyi 0'a ayarlama
            ammoBackPack = 0;
        }
    }
}
