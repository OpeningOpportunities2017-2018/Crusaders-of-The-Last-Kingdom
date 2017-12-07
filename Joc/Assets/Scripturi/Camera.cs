using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour 
{
    public GameObject obiect_urmarit,cub;
    public Vector2 dif;
    public Quaternion rotatie;
    void Start()
    {
        
    }
    public void UrmaresteObiect(GameObject g)
    {
        obiect_urmarit = g;
        dif = this.transform.position-g.transform.position;
    }
    Vector2 Convert(Vector3 vec)
    {
        Vector2 r;
        r.x = vec.x;
        r.y = vec.y;
        return r;
    }
    void LateUpdate()
    {
        if(obiect_urmarit!=null)
            this.transform.position = Convert(obiect_urmarit.transform.position) + dif;
    }
}
