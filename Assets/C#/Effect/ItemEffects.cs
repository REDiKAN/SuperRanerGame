using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemEffects", menuName = "item/create effect")]
public class ItemEffects : ScriptableObject
{
    public string nameEffect;
    public effectsType effectType;
    public float forceEffect;
    public float time;
}

public enum effectsType{ upSpeed, upJump }

