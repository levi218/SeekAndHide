using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost2TargetChooser : TargetChooser
{
    public float hauntingTime = 3f;
    Enemy baseScript;

    Transform hauntingTarget = null;

    protected void Awake()
    {
        baseScript = GetComponent<Enemy>();
    }
    public override Transform GetTarget()
    {
        return checkpoints.GetChild(Random.Range(0, checkpoints.childCount));
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") &&
            Vector3.Angle(baseScript.direction, collider.transform.position - transform.position) < baseScript.visionAngle)
        {
            hauntingTarget = collider.transform;
            InvokeRepeating("ApplyHauntingPath", 0, 0.5f);
            Invoke("ApplyRandomPath", hauntingTime);
        }
    }

    protected void ApplyHauntingPath()
    {
        baseScript.UpdatePath(hauntingTarget);
    }

    protected void ApplyRandomPath()
    {
        hauntingTarget = null;
        baseScript.UpdatePath(GetTarget());
        CancelInvoke();
    }
}
