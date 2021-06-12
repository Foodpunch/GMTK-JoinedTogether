using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera cam;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ShittyCamFollow();
    }
    void ShittyCamFollow()
    {
        Vector3 playerPos = PlayerScript.instance.transform.position;
        playerPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, playerPos, 1.95f*Time.deltaTime);

    }
}
