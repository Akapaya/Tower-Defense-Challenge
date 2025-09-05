using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerBuildData
{
    public GameObject InstantiatedObject { get; private set; }
    public Vector3 MousePosition { get; private set; }
    public Vector3 WorldPosition { get; set; }

    public Direction Direction { get; set; }

    public TowerBuildData(GameObject instantiatedObject, Vector3 mousePosition)
    {
        InstantiatedObject = instantiatedObject;
        MousePosition = mousePosition;
        WorldPosition = Vector3.zero;
    }
}