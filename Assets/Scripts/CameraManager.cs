using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera cam;
    public Camera effectsCam;

    [SerializeField]
    [Range(0f, 1f)]
    float trauma;

    float shake;
    float slow;
    float slowDuration;

    /*  Camera Shake Implementation from GDC
     *  trauma -= Time.deltaTime; 
     *  shake = trauma^ or trauma^3
     *  angle = maxAngle * shake * Rand(-1,1)
     *  offsetX = maxOffset * shake * Rand(-1,1)
     *  offsetY = maxOffset * shake * Rand(-1,1)
     *  shakeCam.rot = originalRot + angle
     *  shakeCame.pos = originalPos + Vector2(offsetX,offsetY)
     */

    //Good values to set for Angle and offet are 50 angle ?? 2 offet??

    float angle, offsetX, offsetY;
    public float maxAngle, maxOffset;
    GameObject player;
    float offset = 0f;              //offsets perlin noise, changing Y axis
    float mult = 55f;               //multiplier for how drastic to scroll perlin
    bool isSustained;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    public void Shake(float _trauma, bool _isSustained = false)
    {
        trauma += _trauma;
        slowDuration = 0.1f;
        if (_isSustained) trauma = Mathf.Clamp(trauma, 0f, _trauma);
        offset++;
    }
    // Update is called once per frame
    void Update()
    {
        ShittyCamFollow();
        if (trauma <= 0f)
        {
            trauma = 0f;
            effectsCam.transform.position = Camera.main.transform.position;
            effectsCam.transform.rotation = Camera.main.transform.rotation;
        }
        if (trauma > 0)
        {
            trauma -= Time.deltaTime;       //decreases linearly
            CameraShake();
        }
        trauma = Mathf.Clamp(trauma, 0f, 1f);
        SetCamSize();
      //  TimeSlow();
    }
    void CameraShake()
    {
        shake = trauma * trauma;
        angle = maxAngle * shake * GetPerlinNoise(trauma);
        offsetX = maxOffset * shake * GetPerlinNoise(trauma + 1f);
        offsetY = maxOffset * shake * GetPerlinNoise(trauma + 2f);

        effectsCam.transform.rotation = cam.transform.rotation * Quaternion.Euler(new Vector3(0f, 0f, angle));
        effectsCam.transform.position = cam.transform.position + new Vector3(offsetX, offsetY, 0f);
    }
    void ShittyCamFollow()
    {
        Vector3 playerPos = PlayerScript.instance.transform.position;
        playerPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, playerPos, 1.95f*Time.deltaTime);
    }
    float GetPerlinNoise(float time)
    {
        return Mathf.PerlinNoise(time * mult, offset) - 0.5f;
    }
    void TimeSlow()
    {
        if (slowDuration > 0f)
        {
            Time.timeScale = 0.05f;
            slowDuration -= Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale = 1f;
            slowDuration = 0f;
        }
    }
    void SetCamSize()
    {
        cam.orthographicSize = 8 + (0.1f * PlayerScript.instance.playerBirdManager.birdList.Count);
        if (cam.orthographicSize > 11) cam.orthographicSize = 11;
        effectsCam.orthographicSize =8+ (0.1f * PlayerScript.instance.playerBirdManager.birdList.Count);
        if (effectsCam.orthographicSize > 11) effectsCam.orthographicSize = 11;
    }
}
