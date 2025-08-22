using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public static WeaponDatabase I;
    public WeaponSpec[] weaponSpecs;

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
        weaponSpecs = Resources.LoadAll<WeaponSpec>(string.Empty);
    }
}
