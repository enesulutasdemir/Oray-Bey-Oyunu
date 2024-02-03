using UnityEngine;
public class Turret : Enemy
{
    [SerializeField] Transform bulletPos;
    RaycastHit hit;    
    public override void Attack()
    {        
        timer += Time.deltaTime;
        transform.LookAt(player.transform);

        if (distance < attackDistance && timer > cooldown)
        {      
            timer = 0;

            if (Physics.Raycast(bulletPos.position, transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                { 
                    hit.collider.gameObject.GetComponent<PlayerController>().ChangeHealth(damage);
                }
            }
        }  
    }
}