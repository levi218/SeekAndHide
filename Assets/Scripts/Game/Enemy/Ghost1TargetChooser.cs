using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost1TargetChooser : TargetChooser
{

    public override Transform GetTarget()
    {
        return checkpoints.GetChild(Random.Range(0, checkpoints.childCount));
    }
}
