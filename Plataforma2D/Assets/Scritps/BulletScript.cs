using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip sonido;
    private Vector2 Direction;
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    // Start is called before the first frame update
    void Start()    
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sonido);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Rigidbody2D.velocity = Vector2.right * Speed;
        Rigidbody2D.velocity = Direction * Speed;
    }
    public void SetDirection(Vector2 direction){
		Direction = direction;
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        GruntScript grunt = other.GetComponent<GruntScript>();
        JohnMovement john = other.GetComponent<JohnMovement>();
        
        if (grunt != null)
        {
            grunt.Hit();
        }
        if (john != null)
        {
            john.Hit();
        }
        DestroyBullet();
    }*/


    // se lanza cada vez que el objeto Bullet colisiona
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //si usamos OnTriggerEnter2D en collision ya nos proviene un collider, por lo que cambiamos las lineas de abajo
        JohnMovement john = collider.GetComponent<JohnMovement>();
        GruntScript grunt = collider.GetComponent<GruntScript>();
        
        if(john != null){ //hemos impactado con john
            john.Hit();
        }
        if(grunt != null){//hemos impactado con grunt
            grunt.Hit();
        }
        DestroyBullet();
    }
}
