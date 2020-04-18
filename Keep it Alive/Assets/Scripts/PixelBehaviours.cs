using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBehaviours : MonoBehaviour
{
    [SerializeField] private List<string> affect;

    [SerializeField] private float sun, moon, wind, rain = 0;
     Renderer myMat;
    float reduce = 0;
    [SerializeField] MyColor[] mycolor;

    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<Renderer>();
    }
    private void Update()
    {

        reduce = Mathf.Lerp(0, sun, .5f);
        Debug.Log(reduce);
        /*if (affect.Count == 0 && sun > 0)
        {
            Debug.Log("ok");
            float reduce = Mathf.Lerp(0, sun, 20);
            Debug.Log(reduce);
            sun -= reduce;
        }*/
    }

    void add(string objectTag)
    {
        if(affect.Count != 0)
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
        }
        else
        {
            sun = (int)Mathf.Lerp(sun, 0, 10);
        }
        
        VerifColor();
    }

    void VerifColor()
    {
        if(sun > 4)
        {
            UpdateLand("foret");
        }
    }

    private void UpdateLand(string newLand)
    {
        switch (newLand) {
            case "montagne":
                myMat.material.SetColor("_Color", mycolor[0].Color);
                break;

            case "tundra":
                myMat.material.SetColor("_Color", mycolor[1].Color);
                break;

            case "lac":
                myMat.material.SetColor("_Color", mycolor[2].Color);
                break;

            case "desert":
                myMat.material.SetColor("_Color", mycolor[3].Color);
                break;

            case "prairie":
                myMat.material.SetColor("_Color", mycolor[4].Color);
                break;

            case "foret":
                myMat.material.SetColor("_Color", mycolor[5].Color);
                break;

            case "ocean":
                myMat.material.SetColor("_Color", mycolor[6].Color);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != null)
        {
            affect.Add(other.gameObject.name);
            add(other.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != null)
        {
            affect.Remove(other.gameObject.name);
        }
    }
}
