using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gun : WieldableObject
{
    public WeaponProperties properties;
    public GameObject ProjectilePrefab;
    public Transform ProjectileOrigin;
    public float ProjectileForce;
    public AudioSource audio;
    public AudioClip clip;

    public TextMeshProUGUI[] texts;

    private bool reloading;
    private bool canShoot;
    private TimeSince timeSince;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        UpdateGunUI();
    }

    private void ShootProjectile()
    {
        GameObject obj = Instantiate(ProjectilePrefab, ProjectileOrigin.position, ProjectileOrigin.rotation);
        obj.GetComponent<Rigidbody>().AddForce(ProjectileOrigin.forward * ProjectileForce, ForceMode.Impulse);
        audio.PlayOneShot(clip);
        properties.RemainingProjectilesInClip--;
    }

    public override void ObjectAction()
    {       
        if(properties.RemainingProjectilesInClip > 0 && !reloading && timeSince > properties.ShotsPerSecond)
        {
            ShootProjectile();
            timeSince = 0;
        }
        else if(properties.RemainingProjectilesInClip == 0)
        {
            if (!reloading)
            {
                reloading = true;
                StartCoroutine(ReloadGun());
            }            
        }
        else
        {
            ShootEmpty();
        }
        UpdateGunUI();
    }

    private void ShootEmpty()
    {
        //play gun empty firing noise.
    }

    private void UpdateGunUI()
    {
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = properties.RemainingProjectilesInClip + "/" + properties.TotalProjectilesRemaining;
        }
    }

    /// <summary>
    /// Basic reload functionality going on here, if the clip is empty attempt to fill it otherwise it never runs, cannot prematurely reload this gun.
    /// </summary>
    private IEnumerator ReloadGun()
    {    
        //we have enough to fill a clip so fill it and end early.
        if(properties.TotalProjectilesRemaining > properties.ProjectilesPerClip)
        {
            properties.RemainingProjectilesInClip = properties.ProjectilesPerClip;
            properties.TotalProjectilesRemaining -= properties.ProjectilesPerClip;                        
        }
        //we cant fill the clip but we can put whatevers left into it.
        else if (properties.TotalProjectilesRemaining > 0)
        {
            properties.RemainingProjectilesInClip = properties.TotalProjectilesRemaining;
            properties.TotalProjectilesRemaining = 0;            
        }
        yield return new WaitForSeconds(properties.ReloadTime);
        reloading = false;
    }
}
