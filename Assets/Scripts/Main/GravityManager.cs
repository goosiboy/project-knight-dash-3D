using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public float GRAVITY_Y = 4;
    public float GRAVITY_X = 1;

    // Start is called before the first frame update
    void Start() {
        Physics.gravity = new Vector3(0, Physics.gravity.y * GRAVITY_Y);
        Physics.gravity = new Vector3(0, Physics.gravity.y * GRAVITY_X);
    }

}
