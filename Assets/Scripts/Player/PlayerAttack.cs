using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    [SerializeField] private GameObject spearPrefab, arrowPrefab;
    [SerializeField] private Transform arrowBowStartPos;

    public float fireRate = 15f;
    public float damage = 20f;
    private float nextTimeToFire;

    private Animator zoomAnim;
    private Camera mainCam;
    private GameObject crosshair;

    private bool isZoomed;
    private bool isAiming;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();

        zoomAnim = transform.Find(Tag.LOOK_ROOT).transform.Find(Tag.ZOOM_FP_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tag.CROSSHAIR);

        mainCam = Camera.main;
    }

    private void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        // Assault Riffle
        if(weaponManager.GetCurrentSelectedWeapon().fireType==WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f/fireRate;

                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFired();
            } 
        }
        // Other Weapons
        else
        {
            if (Input.GetMouseButtonDown(0) )
            {
                // Shoot Bullet Weapon
                if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }
                // Arrow & Spear
                else
                {
                    if (isAiming)
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            ThrowArrowOrSpear(true);
                        }
                        else if(weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            ThrowArrowOrSpear(false);
                        }
                    }
                }

                // Axe
                if (weaponManager.GetCurrentSelectedWeapon().tag == Tag.AXE_TAG)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }
            }
        }
    }

    void BulletFired()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if(hit.transform.tag == Tag.ENEMY_TAG)
            {
                hit.transform.GetComponent<Health>().ApplyDamage(damage);
            }
        }
    }

    // ZOOMING EFFECT
    void ZoomInAndOut()
    {
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            // Press and Hold
            if (Input.GetMouseButtonDown(1))
            {
                zoomAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                zoomAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }

        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                isAiming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                isAiming = false;
            }
        }

    }

    // BOW and SPEAR
    void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = arrowBowStartPos.position;
            arrow.GetComponent<ArrowAndBow>().Launch(mainCam);
        }
        else
        {
            GameObject spear = Instantiate(spearPrefab);
            spear.transform.position = arrowBowStartPos.position;
            spear.GetComponent<ArrowAndBow>().Launch(mainCam);
        }
    }
}
