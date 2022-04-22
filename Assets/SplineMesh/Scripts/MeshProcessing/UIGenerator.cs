using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class UIGenerator : MonoBehaviour
{
    [SerializeField]GameObject pref;
    LineRenderer lineRenderer;
    [SerializeField]List<Spline>sl;
    [SerializeField]float steps = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        //update the line renderer to follow the spline

        foreach (Spline s in sl)
        {
            CurveSample sample;
            GameObject temp = Instantiate(pref, transform);
            lineRenderer = temp.GetComponent<LineRenderer>();
            
            lineRenderer.positionCount = (int)(s.Length/steps);
            //int i = 0;
            //foreach (SplineNode node in s.nodes)
            //{
            //    lineRenderer.SetPosition(i, node.Position);

            //}
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                sample = s.GetSampleAtDistance(i * steps);
                lineRenderer.SetPosition(i, sample.location);
            }
        }


        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
