using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    protected int ammoCurrent;
    //þarjörün maksimum kapasitesi
    protected int ammoMax;
    //yedekteki mermi miktarý
    protected int ammoBackPack;
    //arayüzde gözükecek yazý için bir deðiþken
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
        //Zamanlayýcýyý baþlatma
        timer += Time.deltaTime;
        //Eðer oyuncu farenin sol tuþuna basmýþsa, Shoot fonksiyonunu çaðýrýrýz
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        AmmoTextUpdate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            //þarjörümüz dolu deðilse VEYA yedekte en az bir mermimiz varsa
            if (ammoCurrent != ammoMax || ammoBackPack != 0)
            {
                ///yeniden yükleme iþlevinin hafif bir gecikmeyle etkinleþtirilmesi
                //gecikmeyi istediðiniz herhangi bir sayýya ayarlayabilirsiniz
                shoot.PlayOneShot(reload);
                Invoke("Reload", 1);
            }
        }
    }
    //Silahýn ateþlenip ateþlenemeyeceðinin kontrol edilmesi
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
    // Ve bu fonksiyon silah her vurulduðunda ne olacaðýný tanýmlayacaktýr. protected ve virtual deðiþtiricilere sahip olduðu için
    // bundan beslenen sýnýflar kendi atýþ mantýklarýný tanýmlayabileceklerdir
    protected virtual void OnShoot()
    {
    }
    private void AmmoTextUpdate()
    {
        ammoText.text = ammoCurrent + " / " + ammoBackPack;
    }
    private void Reload()
    {
        //bir deðiþken tanýmlamak ve þarjöre eklememiz gereken mermi sayýsýný hesaplamak
        int ammoNeed = ammoMax - ammoCurrent;
        //Elimizdeki yedek mermi miktarý, yeniden doldurmak için gereken mermi miktarýna eþit veya daha fazla ise
        if (ammoBackPack >= ammoNeed)
        {
            //Ýhtiyaç duyulan mermi sayýsýnýn yedeklerden çýkarýlmasý
            ammoBackPack -= ammoNeed;
            //þarjöre gerekli sayýda mermi eklemek
            ammoCurrent += ammoNeed;
        }
        //aksi takdirde ( yedeklerde tam bir yeniden doldurma için gerekenden daha az mermi varsa)
        else
        {
            //tüm yedek cephanemizi þarjöre ekleyemek
            ammoCurrent += ammoBackPack;
            //yedek cephaneyi 0'a ayarlama
            ammoBackPack = 0;
        }
    }
}
