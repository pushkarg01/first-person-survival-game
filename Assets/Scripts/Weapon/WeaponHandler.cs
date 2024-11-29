using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,SELF_AIM,AIM
}

public enum WeaponFireType
{
    SINGLE,MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,ARROW,SPEAR,NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField]private AudioSource shootSound, reloadSound;

    public WeaponAim weaponAim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;

    public GameObject attackPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER,canAim);
    }

    void OnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void OffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        shootSound.Play();
    }

    void PlayReloadSound()
    {
        reloadSound.Play();
    }

    void OnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void OffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        { 
            attackPoint.SetActive(false);
        }
    }
}
