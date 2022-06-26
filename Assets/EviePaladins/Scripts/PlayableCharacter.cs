using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour
{
    public abstract void M1Skill();
    public abstract void M2Skill();
    public abstract void QSkill();
    public abstract void FSkill();
    public abstract void UltimateSkill();
}
