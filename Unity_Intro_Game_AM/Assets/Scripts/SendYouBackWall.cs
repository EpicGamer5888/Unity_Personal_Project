using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendYouBackWall : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("Cycle", 0, Random.Range( 30, 120 ));
    }

    private void Cycle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
