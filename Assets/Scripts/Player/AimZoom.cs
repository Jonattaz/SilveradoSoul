using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimZoom : MonoBehaviour
{
    [SerializeField] private int zoom = 20;
    [SerializeField] private int normal = 60;
    [SerializeField] private float smooth = 5;
    private bool isZoomed = false;
    private bool notZoomed = true;

    void Update()
    {
        ZoomController();
    }

    private void ZoomController(){
        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = !isZoomed;
        }

        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView,
                zoom, Time.deltaTime * smooth);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            isZoomed = !notZoomed;
        }

        if (notZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView,
                normal, Time.deltaTime * smooth);
        }
    }

}
