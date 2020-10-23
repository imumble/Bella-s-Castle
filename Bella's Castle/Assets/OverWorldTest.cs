using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldTest : MonoBehaviour
{
    public Collider battleStarter;
    public Collider chasser;
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("BattleStart!");
        //SceneManager.LoadScene(0);
    }

}
