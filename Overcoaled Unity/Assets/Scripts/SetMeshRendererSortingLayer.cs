using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMeshRendererSortingLayer : MonoBehaviour
{
    
    private Renderer renderer;

    [SerializeField]
    private string sortingLayerName;

    [SerializeField]
    private int sortingOrder;

    public void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.sortingLayerName = sortingLayerName;
        renderer.sortingOrder = sortingOrder;
    }
}
