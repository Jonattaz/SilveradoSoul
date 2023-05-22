using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAmmo : MonoBehaviour
{
    public static int ammo;
    [SerializeField]
    private GameObject ammoDisplay;
    [SerializeField]
    private GameObject ammoDisplay2;

    // Update is called once per frame
    void Update(){
        ammoDisplay.GetComponent<Text>().text = "" + ammo;
        ammoDisplay2.GetComponent<Text>().text = "" + ammo;
        
    }
}
