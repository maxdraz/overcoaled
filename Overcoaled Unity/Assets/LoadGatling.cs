using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGatling : MonoBehaviour
{
    [SerializeField] private GatlingGun gatlingGun;

    public void LoadGatlingGun()
    {
        gatlingGun.LoadAmmo();
    }
}
