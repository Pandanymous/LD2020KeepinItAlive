using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotativeObject : MonoBehaviour
{
    public float radius, maxTurnRate, deltaTurnRate;
    public GameObject aim;
    public GameObject planete;


    private Rigidbody rig;
    private float currentTurnRate;
    void Start(){
        FetchComponents();
    }

    void FetchComponents(){
        rig = GetComponent<Rigidbody>();
    }

    void Update(){

    }

    void FixedUpdate(){
        CheckPosition();
    }

    void CheckPosition(){
        Vector3 aimedDirection = (aim.transform.position - planete.transform.position).normalized;
        if(transform.position + planete.transform.position != aimedDirection * radius)
            Turn(aimedDirection* radius);
    }

    void Turn(Vector3 aimedPosition){
        Vector3 velocity = Vector3.zero;
        
        //Altitude
        velocity += (radius - (transform.position - planete.transform.position).magnitude) * Up();
        
        //Rotation
        float degAimed =    Mathf.Atan2(aimedPosition.y         - planete.transform.position.y, aimedPosition.x         - planete.transform.position.x);
        float degCurrent =  Mathf.Atan2(transform.position.y    - planete.transform.position.y, transform.position.x    - planete.transform.position.x);
        if(Mathf.Abs(degCurrent - degAimed) > Mathf.PI || Mathf.Abs(degCurrent - degAimed) < -Mathf.PI)
            velocity += -(Mathf.Atan2(transform.position.x    - planete.transform.position.y, transform.position.y    - planete.transform.position.x) - degAimed) * Right();
        else velocity += (degCurrent - degAimed) * Right();

        rig.velocity = velocity * (currentTurnRate + deltaTurnRate);
    }

    Vector3 Right(){
        Vector3 up= Up();
        return new Vector3(up.y, -up.x, up.z);
    }

    Vector3 Up(){
        return (transform.position - planete.transform.position).normalized;
    }
}
