using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    
    public GameObject prefabBullet;
    public float Speed;
    public float JumpForce;
    

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float horizontal;
    private Boolean Grounded;
    private float LastShoot;

    private int Health = 5;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        // almacena -1 si pulsamos tecla a
        // almacena 0 si no pulsamos nada
        // almacena 1 si pulsamos tecla d

        // Controlamos la dirección donde mira el Personaje cuando
        // cambia de dirección izquierda o derecha
        if(horizontal < 0.0f) transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        Animator.SetBool("running",horizontal != 0.0f);

        //Lanzamos un rayo en la dirección que hemos indicado a la
        //distancia indicada si choca con algo devolvemos true sino false
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f)){
            Grounded = true; 
        }
        else{
            Grounded = false; 
        }

        if(Input.GetKeyDown(KeyCode.W) && Grounded){
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f){
            Shoot();  
            LastShoot = Time.time;

        }
        
    }

    private void Shoot(){
        Vector3 direction;

        if ( transform.localScale.x == 1.0f ) direction = Vector3.right;
        else direction = Vector3.left;

        // Pintamos el Prefab en scena, en la posición indicada y la rotación=0
        // La posición se calcula: 
        // transform.position -> centro de John
        // direction *0.1f -> offset de desplazamiento
        GameObject bullet = Instantiate(prefabBullet,transform.position + direction *0.1f, Quaternion.identity);

        bullet.GetComponent<BulletScript>().SetDirection(direction);

    }

    private void Jump(){
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void Hit(){
        Health = Health - 1;
        if(Health == 0) Destroy(gameObject);
    }
}
