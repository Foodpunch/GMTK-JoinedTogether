using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IndicatorScript : MonoBehaviour
{
    public GameObject IndicatorPrefab;
    public Image ImageToRotate;
    Canvas UICanvas;

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
        GameObject indicatorClone = Instantiate(IndicatorPrefab, transform.position, Quaternion.identity);
        indicatorClone.transform.SetParent(UICanvas.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
       // Vector3 uiPos = (screenPos)
    }
}
