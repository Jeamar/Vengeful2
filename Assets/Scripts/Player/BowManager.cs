using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowManager : MonoBehaviour
{
    public Transform bow;
    public Transform STbow, CRbow;

    public PlayerMain playerM;
    private Vector3 difference;
    private bool isAiming;


    public void manageArm(bool check)
    {
        isAiming = check;
        bow.gameObject.SetActive(check); //the bow that is used is setted in the main script
                                         //this makes it visible (place holder)

        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - bow.position;
        Debug.DrawRay(playerM.firePoint.position, difference, Color.red);
        difference.Normalize(); 

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //this calculates the rotation that the arm should have, didn't put a limit to it yet so it goes 360
        //not a problem tho

        bow.rotation = Quaternion.Euler(0, 0, rotationZ); //sets the arm rotation
    }

}
