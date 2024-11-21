using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable
{
    public void TakeDamage(float value);
    public void Destroy();
}
