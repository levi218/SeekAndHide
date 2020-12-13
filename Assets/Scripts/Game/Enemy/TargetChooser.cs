using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetChooser : MonoBehaviour
{
    public Transform checkpoints;

    public abstract Transform GetTarget();

}
