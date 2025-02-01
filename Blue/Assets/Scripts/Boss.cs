using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Boss
{
    void getDamage(float damage);
    void respawn();
}