using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    private int health;
    [SerializeField] Animator anim;
    [SerializeField] Image cusror;
    [SerializeField] AudioSource characterSounds;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if(direction.x !=0 || direction.z != 0)
        {
            if (!characterSounds.isPlaying)
            {
                // Sesi oynatma kýsmý
                characterSounds.Play();
            }
        }
        if(direction.x ==0 && direction.z == 0)
        {
            characterSounds.Stop();
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }
    public void ChangeHealth(int count)
    {
        // caný eksiltme kýsmý
        health -= count;
        // eðer can sýfýr veya altýna düþerse
        if (health <= 0)
        {
            //bir þeyler olacak
            Destroy(gameObject);
            this.enabled = false;
        }
    }
}
