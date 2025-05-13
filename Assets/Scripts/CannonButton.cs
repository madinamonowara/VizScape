using Oculus.Interaction;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class CannonButton : MonoBehaviour
{


    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed = 15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawn.forward * bulletSpeed;
    }
}
