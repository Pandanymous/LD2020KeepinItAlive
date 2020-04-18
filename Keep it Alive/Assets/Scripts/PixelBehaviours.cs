using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBehaviours : MonoBehaviour
{

    #region param
    [Header ("Les éléments affectant la cellule")]
    [SerializeField] private List<string> affect;

    [Space]
    [Header ("Incrementation")]
    [SerializeField] private float sun, moon, wind, rain = 0;

    [Space]
    [Header("limite pour changer la couleur")]
    [SerializeField] private int valForChangeColor = 4;

    [Space]
    [Header("retour à 0")]
    public AnimationCurve reduce ;

    [Space]
    [Header("Les couleurs")]
    [SerializeField] MyColor[] mycolor;

    //Variable pour modifier les couleurs
    Renderer myMat;

    #endregion


    #region Update ||Start
    void Start()
    {
        myMat = GetComponent<Renderer>();
    }
    private void Update()
    {
        /*if (affect.Count == 0 && sun > 0)
        {
            Debug.Log("ok");

            sun -= reduce.Evaluate(Time.deltaTime);
        }*/
    }
    #endregion

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
        if(sun >= valForChangeColor && sun > rain && sun > wind && sun > moon)
        {
            UpdateLand("desert");
        }
        else if (moon >= valForChangeColor && moon >= rain && moon > wind && moon > sun)
        {
            UpdateLand("montagne");
        }
        else if (rain >= valForChangeColor && rain >= moon && rain > wind && rain > sun)
        {
            UpdateLand("lac");
        }
        else if (wind >= valForChangeColor && wind >= moon && wind > rain && wind > sun)
        {
            UpdateLand("tundra");
        }
        else if ((sun >= valForChangeColor && moon >= valForChangeColor) && (sun > rain && moon > rain) && (sun > wind && moon > wind))
        {
            UpdateLand("neutral");
        }
        else if ((sun >= valForChangeColor && wind >= valForChangeColor) && (sun > rain && wind > rain) && (sun > moon && wind > moon))
        {
            UpdateLand("prairie");
        }
        else if ((sun >= valForChangeColor && rain >= valForChangeColor) && (sun > wind && rain > wind) && (sun > moon && rain > moon))
        {
            UpdateLand("foret");
        }
        else if ((moon >= valForChangeColor && wind >= valForChangeColor) && (moon > sun && wind > sun) && (moon > rain && wind > rain))
        {
            UpdateLand("glacier");
        }
        else if ((moon >= valForChangeColor && rain >= valForChangeColor) && (moon > sun && rain > sun) && (moon > wind && rain > wind ))
        {
            UpdateLand("ocean");
        }
        else if ((rain >= valForChangeColor && wind >= valForChangeColor) && (rain > sun && wind > sun) && (rain > moon && wind > moon))
        {
            UpdateLand("ocean");
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

            case "neutral":
                myMat.material.SetColor("_Color", mycolor[7].Color);
                break;
        }
    }

    #region Ontrigger
    private void OnTriggerEnter(Collider other)
    {
        //ajoute l'élément dans la liste affect
        if(other.tag != null)
        {
            affect.Add(other.gameObject.name);
            add(other.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //retire l'élément dans la liste affect
        if (other.tag != null)
        {
            affect.Remove(other.gameObject.name);
        }
    }
    #endregion
}
