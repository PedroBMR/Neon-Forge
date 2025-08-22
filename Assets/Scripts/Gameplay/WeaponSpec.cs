using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Weapon Spec")]
public class WeaponSpec : ScriptableObject
{
    public WeaponType weaponType;
    public Rank rank;
    public double hp;
    public float time;
}
