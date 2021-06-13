using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IndicatorScript : MonoBehaviour
{
    public GameObject IndicatorPrefab;
    public GameObject ImageToRotate;
    Canvas UICanvas;
    GameObject indicatorClone;
    public bool flockCollected;
    public float offset = -20f;     //how much to offset the indicator by.
    public float indicatorDistance = 20f;
    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
        indicatorClone = Instantiate(IndicatorPrefab, transform.position, Quaternion.identity);
        indicatorClone.transform.SetParent(UICanvas.transform);
        ImageToRotate = indicatorClone.transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        float sqrDistToPlayer = (transform.position - PlayerScript.instance.transform.position).sqrMagnitude;
        if (sqrDistToPlayer > (indicatorDistance * indicatorDistance) || flockCollected)
        {
            indicatorClone.SetActive(false);
            
        }
        else IndicatorStuff();

    }
    void IndicatorStuff()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 uiPos = (screenPos - UICanvas.transform.position); //somehow doing this makes it work idk why but it does
        indicatorClone.transform.position = screenPos;
        bool exceedLeft = uiPos.x < -Screen.width / 2;
        bool exceedRight = uiPos.x > Screen.width / 2;
        bool exceedsUp = uiPos.y > Screen.height / 2;
        bool exceedsDown = uiPos.y < -Screen.height / 2;
        //Debug.Log("ExLEft : " + exceedLeft + "\n ExRight : " + exceedRight + "\n ExUp : " + exceedsUp + "\n ExDown : " + exceedsDown);
        if ((uiPos.x > -Screen.width / 2 && uiPos.x < Screen.width / 2) && (uiPos.y > -Screen.height / 2 && uiPos.y < Screen.height / 2))
        {
            indicatorClone.SetActive(false);
        }
        else
        {
            float clampX = screenPos.x;
            float clampY = screenPos.y;
            indicatorClone.SetActive(true);
            if (exceedLeft)
            {
                clampX = -Screen.width / 2 + UICanvas.transform.position.x;
                indicatorClone.transform.position = new Vector3(clampX - offset, screenPos.y, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            if (exceedRight)
            {
                clampX = Screen.width / 2 + UICanvas.transform.position.x;
                indicatorClone.transform.position = new Vector3(clampX + offset, screenPos.y, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, -90);
            }
            if (exceedsDown)
            {
                clampY = -Screen.height / 2 + UICanvas.transform.position.y;
                indicatorClone.transform.position = new Vector3(screenPos.x, clampY - offset, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
            if (exceedsUp)
            {
                clampY = Screen.height / 2 + UICanvas.transform.position.y;
                indicatorClone.transform.position = new Vector3(screenPos.x, clampY + offset, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (exceedsUp && exceedRight)
            {
                clampY = Screen.height / 2 + UICanvas.transform.position.y;
                clampX = Screen.width / 2 + UICanvas.transform.position.x;
                indicatorClone.transform.position = new Vector3(clampX + offset, clampY + offset, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, -45);
            }
            if (exceedsUp && exceedLeft)
            {
                clampY = Screen.height / 2 + UICanvas.transform.position.y;
                clampX = -Screen.width / 2 + UICanvas.transform.position.x;
                indicatorClone.transform.position = new Vector3(clampX - offset, clampY + offset, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, 45);
            }
            if (exceedsDown && exceedRight)
            {
                clampY = -Screen.height / 2 + UICanvas.transform.position.y;
                clampX = Screen.width / 2 + UICanvas.transform.position.x;
                indicatorClone.transform.position = new Vector3(clampX + offset, clampY - offset, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, -135);
            }
            if (exceedsDown && exceedLeft)
            {
                clampY = -Screen.height / 2 + UICanvas.transform.position.y;
                clampX = -Screen.width / 2 + UICanvas.transform.position.x;
                indicatorClone.transform.position = new Vector3(clampX - offset, clampY - offset, 0);
                ImageToRotate.transform.localEulerAngles = new Vector3(0, 0, 135);
            }
        }

    }
}
