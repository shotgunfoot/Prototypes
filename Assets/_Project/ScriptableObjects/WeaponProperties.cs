using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="WeaponProperties", menuName ="Weapons/Properties")]
public class WeaponProperties : ScriptableObject
{
    public float NumberOfProjectilesPerShot;
    public float ShotsPerSecond;
    public float ProjectilesPerClip;
    public float TotalProjectilesRemaining;
    public float ReloadTime;
    public float RemainingProjectilesInClip;
}
