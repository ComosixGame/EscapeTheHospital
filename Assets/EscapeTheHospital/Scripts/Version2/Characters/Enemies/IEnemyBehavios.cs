using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehavios
{
    void Idle();
    void Patrol();
    void PatrolWhenDetected();
    void PatrolWhenLostElectric();
    void PatrolWhenLostKey();
}
