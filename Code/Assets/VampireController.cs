using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireController : MonoBehaviour
{

    private GameObject player;
    private Animator anim;
    private bool canAttack = true;
    private bool Alive = true;
    public float life = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Alive)
        {
            if (life > 0)
            {
                if (player != null)
                {
                    GetComponent<Patrol>().playerPlaceMent(true, player.transform);
                }
                else
                {
                    GetComponent<Patrol>().playerPlaceMent(false, null);
                }
            }
            else
            {
                Alive = false;
                GetComponent<Patrol>().death();
                anim.SetTrigger("Death");
                StartCoroutine("DeathTimer");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }

    public void attack()
    {
        if (canAttack)
        {
            canAttack = false;
            anim.SetTrigger("attack");
            player.GetComponent<PlayerController2>().hurt(0.58f);
            StartCoroutine("attackTime");
        }
    }

    public void getHurt(float damage)
    {
        life -= damage;
        print("VampireLife ");
        print(life);
    }


    IEnumerator attackTime()
    {
        yield return new WaitForSeconds(3.0f);
        canAttack = true;
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
}
