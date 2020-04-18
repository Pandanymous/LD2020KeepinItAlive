using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    #region Param
    [SerializeField]private float viewRadius;

    [Range (0,360)]
    [SerializeField] private float viewAngle;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private List<Transform> visibleTarget = new List<Transform>();
    #endregion

    #region UPDATE
    private void FixedUpdate()
    {
        FindTarget();
    }
    #endregion


    #region FindTarget
    public Vector3 DirFromAngle( float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3( Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    void FindTarget()
    {
        visibleTarget.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, viewRadius);
        Debug.Log(targetsInRadius.Length);
         for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position,dirToTarget,distanceToTarget,obstacleMask))
                    visibleTarget.Add(target);
            }
        }
    }

    #endregion


    #region GETTER && SETTER
    public float ViewAngle { get => viewAngle; set => viewAngle = value; }
    public LayerMask TargetMask { get => targetMask; set => targetMask = value; }
    public List<Transform> VisibleTarget { get => visibleTarget; set => visibleTarget = value; }
    public float ViewRadius { get => viewRadius; set => viewRadius = value; }
    #endregion


}
