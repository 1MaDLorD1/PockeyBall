using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private void Update()
    {
        transform.position = new Vector3(_ball.transform.position.x, _ball.transform.position.y, transform.position.z);
    }
}
