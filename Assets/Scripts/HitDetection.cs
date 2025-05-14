using Unity.VisualScripting;
using UnityEngine;


public class HitDetection : MonoBehaviour
{
    public static float counter = 0;
    public GameObject returnObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("destroyBall", 3f, 12f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy"){
            Destroy(collision.gameObject);
            counter = counter + 1;
            if(counter >= 4){
                Vector3 location = new Vector3(2, 1, -22.6f);
                Instantiate(returnObj, location, Quaternion.Euler(90f, 90f, 90f));
            }
        }
    }

void destroyBall(){
        Destroy(gameObject);
    }
}
