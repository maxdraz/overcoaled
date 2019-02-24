using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammoInGun = 15;

    public void SetAmmo(int ammo)
    {
        ammoInGun = ammo;
    }
}
