using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost3TargetChooser : Ghost2TargetChooser
{
    public override Transform GetTarget()
    {
        return GameController.Instance.player.transform;
    }
}
