using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public Transform target;
    public bool updateEachFrame;

    private void Start()
    {
        if(target == null)
        {
            target = Camera.main.transform;
        }

        transform.LookAt(target);
    }

    // Update is called once per frame
    void OnEnable()
    {
        transform.LookAt(target);

       
    }

    private void Update()
    {
        if (updateEachFrame)
        {
            StartCoroutine(LookAtEachFrame());
        }
    }

    IEnumerator LookAtEachFrame()
    {
        transform.LookAt(target);
        yield return null;
    }
}
