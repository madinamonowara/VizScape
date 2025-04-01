using UnityEngine;

public class LightActivator : MonoBehaviour
{
    public Light targetLight;

    public void TurnOn()
    {
        targetLight.enabled = true;
    }

    public void TurnOff()
    {
        targetLight.enabled = false;
    }
}