using Oculus.Interaction;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class CannonButton : MonoBehaviour
{

    bool canShoot = true;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("allowedToMove", 1f, 5f);
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
    }

    void shootCannon(){
        if(canShoot == true){
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = -bulletSpawn.forward * bulletSpeed;
            canShoot = false;
        } 
    }

    void allowedToMove(){
        canShoot = true;
    }
}
