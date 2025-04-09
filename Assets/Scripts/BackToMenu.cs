using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] string filter;
    void OnTriggerEnter(Collider other){
        if (!string.IsNullOrEmpty(filter) && !other.gameObject.CompareTag(filter)) return;
        SceneManager.LoadScene("StartMenu");
    }
}
