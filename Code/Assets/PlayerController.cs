using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8; // velocidade do jogador
    public float gravity = -9.8f; // valor da gravidade
    public LayerMask groundMask;
    CharacterController character;
    Vector3 velocity;
    bool isGrounded;
    public bool travarMouse = true; //Controla se o cursor do mouse é exibido
    public float sensibilidade = 2.0f; //Controla a sensibilidade do mouse
    private float mouseX = 0.0f, mouseY = 0.0f; //Variáveis que controla a rotação do mouse
    private Animator anim;
    public SphereCollider sphere;
    private List<GameObject> enemyActiveAttack = new List<GameObject>();


    void Start()
    {
        anim = GetComponent<Animator>();
        character = gameObject.GetComponent<CharacterController>();

        if (travarMouse)
        {
            Cursor.visible = true; //Oculta o cursor do mouse
            Cursor.lockState = CursorLockMode.Locked; //Trava o cursor do centro
        }

    }




    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * sensibilidade; // Incrementa o valor do eixo X e multiplica pela sensibilidade
        mouseY -= Input.GetAxis("Mouse Y") * sensibilidade; // Incrementa o valor do eixo Y e multiplica pela sensibilidade. (Obs. usamos o - para inverter os valores)

        // Verifica se encostando no chão (o centro do objeto deve ser na base)
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundMask);

        // Se no chão e descendo, resetar velocidade
        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -1.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        anim.SetBool("walking", z != 0);
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("active");
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("passive");
        }
        // Rotaciona personagem
        // transform.Rotate(0, mouseX * speed * 10 * Time.deltaTime, 0);
        transform.eulerAngles = new Vector3(0, mouseX, 0);

        // Move personagem
        Vector3 move = transform.forward * z + transform.right * x;
        character.Move(move * Time.deltaTime * speed);

        // Aplica gravidade no personagem
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemyActiveAttack.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyActiveAttack.Remove(other.gameObject);
        }
    }
}

