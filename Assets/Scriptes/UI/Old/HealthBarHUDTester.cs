/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class HealthBarHUDTester : MonoBehaviour
{
    public void AddHealth()
    {
        PlayerStatManagment.instance.AddHealth();
    }

    public void Heal(float health)
    {
        PlayerStatManagment.instance.Heal(health);
    }

    public void Hurt(float dmg)
    {
        PlayerStatManagment.instance.TakeDamage(dmg, "");
    }
}
