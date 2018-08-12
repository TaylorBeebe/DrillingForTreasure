using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
[System.Serializable]
public abstract class Upgrade : ScriptableObject {
    [SerializeField]
    public int cost;
    public abstract void Execute();
    public abstract void OnUpgradeEnable();
    public abstract void OnUpgradeDisable();
}
