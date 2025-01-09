using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Boss
{
    void getDamage(int damage);
    void respawn();
}