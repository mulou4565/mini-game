using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public float hp;
    public virtual void Attack() { }
    public virtual void Hurt(float value) { }
}
