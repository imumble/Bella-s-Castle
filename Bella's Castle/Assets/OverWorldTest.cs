using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        SceneManager.LoadScene(1);
    }
}
