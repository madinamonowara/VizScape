using Oculus.Interaction;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;
using System.Security.Cryptography;


public class CannonButton : MonoBehaviour
{

    bool canShoot = true;
    bool canMove = true;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public GameObject cannon;
    public AudioSource sound;
    public float bulletSpeed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("allowedToMove", 1f, 6f);
        InvokeRepeating("allowedToShoot", 1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "button"){
            shootCannon();
        }
        if(hit.gameObject.tag == "leftbutton"){
            if(canMove == true){
                cannon.transform.position = new Vector3(cannon.transform.position.x + 1, cannon.transform.position.y, cannon.transform.position.z);
                canMove = false;
            }
        }
        if(hit.gameObject.tag == "rightbutton"){
            cannon.transform.position = new Vector3(cannon.transform.position.x - .3f, cannon.transform.position.y, cannon.transform.position.z);
            canMove = false;
        }

    }

    void shootCannon(){
        if(canShoot == true){
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = -bulletSpawn.forward * bulletSpeed;
            sound.Play();
            canShoot = false;
        } 
    }

    void allowedToShoot(){
        canShoot = true;
    }
    void allowedToMove(){
        canMove = true;
    }
}
