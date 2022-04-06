using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layer
{
    Enemy = 7,
    Walkablearea = 6,
    raycastendstop = -1
}
public class CameraRayCast : MonoBehaviour
{
    public Layer[] layerPros = { Layer.Enemy, Layer.Walkablearea };
    public Camera cam;
    RaycastHit hit;
    public RaycastHit m_hit { get { return hit; } }
    Layer layerHit;
    public Layer m_LayerHit { get { return layerHit; } }
    public float maxRayDistance = 100;
    void Start()
    {

    }

    void Update()
    {
        foreach (Layer item in layerPros)//look for and return preiorty layer hit
        {
            var r_hit = RayCastForLayer(item);
            if(r_hit.HasValue)
            {
                hit = r_hit.Value;
                layerHit = item;
                return;
            }
        }
        hit.distance = maxRayDistance;
        layerHit = Layer.raycastendstop;
    }
    RaycastHit? RayCastForLayer(Layer lay)
    {
        int layerMask = 1 << (int)lay;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,maxRayDistance,layerMask))
        {
            return hit;
        }
        return null;
    }
}
