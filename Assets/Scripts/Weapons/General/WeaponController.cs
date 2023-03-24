using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{   public GameObject emptySlot;
    public GameObject handgunItem;
    public GameObject rifleItem;
    public static bool handgunEquiped;
    public static bool rifleEquiped;

    // Update is called once per frame
    void Update()
    {
        EquipInput();
    }

    void EquipHandgun()
    {
        emptySlot.SetActive(false);
        rifleItem.SetActive(false);
        rifleEquiped = false;
        handgunEquiped = true;
        handgunItem.SetActive(true);
    }

    void EquipRifle()
    {
        
        emptySlot.SetActive(false);
        handgunItem.SetActive(false);
        handgunEquiped = false;
        rifleEquiped = true;
        rifleItem.SetActive(true);
    }

    void EquipEmptySlot()
    {
        handgunItem.SetActive(false);
        handgunEquiped = false;
        rifleItem.SetActive(false);
        rifleEquiped = false;
        emptySlot.SetActive(true);
        
    }

    private void EquipInput(){
        
        if(Input.GetKeyDown("1") && HandgunPickup.handgunCollected)
        {
            EquipHandgun();
        }

        if(Input.GetKeyDown("2") && RiflePickup.rifleCollected)
        {
            EquipRifle();
        }

        if(Input.GetKeyDown("3"))
        {
            EquipEmptySlot();
        }
    }
}
