using UnityEngine;

public class TempScript : MonoBehaviour
{
    Animator anim;
    ButtonDoor script; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        script = GetComponent<ButtonDoor>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
