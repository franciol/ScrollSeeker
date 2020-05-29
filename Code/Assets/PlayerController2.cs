using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    private float Life = 5;
    public float speed = 8;
    public float gravity = -9.8f; 
    public LayerMask groundMask;
    CharacterController character;
    Vector3 velocity;
    bool isGrounded;
    public bool travarMouse = true; 
    public float sensibilidade = 2.0f;
    private float mouseX = 0.0f, mouseY = 0.0f;
    private Animator anim;
    private int curAttack = 0;
    private bool canAttack = true;
    private bool canHurt = true;
    private int scrolls = 0;
    private bool dead = false;
    public Text DeadText,WinTEXT;


    void Start()
    {
        DeadText.enabled = false;
        WinTEXT.enabled = false;
        anim = GetComponent<Animator>();
        character = gameObject.GetComponent<CharacterController>();

        if (travarMouse)
        {
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.Locked; 
        }

    }




    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scene1");
        }else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!dead)
        {
            mouseX += Input.GetAxis("Mouse X") * sensibilidade;
            mouseY -= Input.GetAxis("Mouse Y") * sensibilidade;

            isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundMask);

            if (isGrounded && velocity.y < 0.0f)
            {
                velocity.y = -1.0f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            anim.SetFloat("PosX", x);
            anim.SetFloat("PosY", z);


            if (canAttack)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Attack");
                    curAttack = 1;
                    StartCoroutine("attackTime");
                }
                else if (Input.GetButtonDown("Fire2"))
                {
                    anim.SetTrigger("360Attack");
                    curAttack = 2;
                    StartCoroutine("attackTime");
                }
            }
            transform.eulerAngles = new Vector3(0, mouseX, 0);

            Vector3 move = transform.forward * z + transform.right * x;
            character.Move(move * Time.deltaTime * speed);

            velocity.y += gravity * Time.deltaTime;
            character.Move(velocity * Time.deltaTime);


        }
        else
        {
            StartCoroutine("deadTime");
            DeadText.enabled = true;
        }
        
    }
    public void hurt(float damage)
    {
        if (!dead)
        {
            anim.SetTrigger("hurt");
            Life -= damage;
            canHurt = false;
            if(Life <= 0)
            {
                dead = true;
                print(Life);
                anim.SetTrigger("Death");
            }
        }
    }
    public int getAttack()
    {
        int temp = curAttack;
        return temp;
    }

    public void pickScroll()
    {
        scrolls += 1;
        if(scrolls == 2)
        {
            WinTEXT.enabled = true;
            StartCoroutine("winTime");
        }
    }
    public int getScrolls()
    {
        return scrolls;
    }

    public float getLife()
    {
        return Life;
    }

    IEnumerator attackTime()
    {
        yield return new WaitForSeconds(2.0f);
        curAttack = 0;
        canAttack = true;
    }

    IEnumerator hurtTime()
    {
        yield return new WaitForSeconds(7.0f);
        canHurt = true;
    }

    IEnumerator deadTime()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Scene1");
    }

    IEnumerator winTime()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Menu");
    }

}

