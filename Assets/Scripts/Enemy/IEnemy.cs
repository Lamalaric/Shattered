using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void MakeDamage(IDestroyable target, float damage);
}
