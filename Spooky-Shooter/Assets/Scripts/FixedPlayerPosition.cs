using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FixedPlayerPosition : MonoBehaviour
{
    //This is the script we use to make sure the position on the Y-axis is maintained at '0', because, for some reason, the Rigidbody makes the player mesh slowly fall constantly

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
    }

}
