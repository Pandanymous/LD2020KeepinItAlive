using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBehaviours : MonoBehaviour
{
    [SerializeField] private int sun, moon, wind, rain = 0;

    [SerializeField] Renderer myMat;

    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<Renderer>();
    }

    void add(string objectTag)
    {
        switch (objectTag)
        {
            case "SUN":
                sun++;
                break;

            case "MOON":
                moon++;
                break;

            case "WIND":
                wind++;
                break;

            case "RAIN":
                rain++;
                break;

            default:
                Debug.Log("stop");
                break;
        }
        VerifColor();
    }

    void VerifColor()
    {
        if(sun > 4)
        {
            myMat.material.SetColor("_Color", new Color(122,201,12));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != null)
        {
            add(other.tag);
        }
    }
}
