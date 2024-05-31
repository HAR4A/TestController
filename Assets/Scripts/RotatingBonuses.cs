using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBonuses : MonoBehaviour
{
    private float speed = 150f;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
