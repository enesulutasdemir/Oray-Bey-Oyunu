using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    protected GameObject player;
    protected Animator anim;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!dead)
        {
            Attack();
        }
    }
    public virtual void Move()
    {
    }
    public virtual void Attack()
    {
    }
    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
    }
    public void ChangeHealth(int count)
    {
        // can eksiltme kýsmý
        health -= count;
        // eðer can sýfýr veya altýna inerse...
        if (health <= 0)
        {
            // dead deðiþkeninin deðerini deðiþtirelim yani artýk Attack ve Move fonksiyonlarý çalýþmayacak
            dead = true;
            // düþmanýn colliderýný deaktive edelim
            GetComponent<Collider>().enabled = false;
            // ölüm animasyonunu aktive edelim
            anim.SetBool("Die", true);
        }
    }
}
