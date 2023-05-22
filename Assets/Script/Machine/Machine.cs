using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { 
    VERTICAL,
    HORIZONTAL,
    DIRECTLY,

} 
public abstract  class  Machine : MonoBehaviour
{

   public virtual void Run() { }
}
