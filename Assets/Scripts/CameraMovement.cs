using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemnt : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.z = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move.z = -1;
        }


        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + move.x, transform.position.y + move.y, transform.position.z + move.z), moveSpeed);
    }
}
