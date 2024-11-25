using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody rb;

    /// <summary>
    /// 
    /// This is my showcase of  singleton
    /// 
    /// I have the scipt watching the bullet.
    /// it places values once it spawns and does calculations
    /// 
    /// </summary>


    // Start is called before the first frame update

    void Start()
    {
        rb.velocity = transform.right * speed;// set velocity of entity
    }

    private void OnTriggerStay(Collider collider)//hit collider
    {
    //Enemy enemy = hitInfo.GetComponent<Enemy>();//is it enemy?
    //enemy.Die();//enemy die
    //Destroy(gameObject);//destroy



        if (collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.Die();
            Destroy(gameObject);
        }

        else if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }


}
