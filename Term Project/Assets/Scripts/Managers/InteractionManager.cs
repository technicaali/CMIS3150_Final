using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    public Letter hoveredLetter = null;
    public Flashlight hoveredFlashlight = null;
    public Batteries hoveredBatteries = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                // Disable the outline of the previously selected item
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }

                hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }

            // Ammo Box
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {
                // Disable the outline of the previously selected item
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }

                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                    Destroy(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }

            // Letter
            if (objectHitByRaycast.GetComponent<Letter>())
            {
                // Disable the outline of the previously selected item
                if (hoveredLetter)
                {
                    hoveredLetter.GetComponent<Outline>().enabled = false;
                }

                hoveredLetter = objectHitByRaycast.gameObject.GetComponent<Letter>();
                hoveredLetter.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Letter.Instance.ReadLetter(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredLetter)
                {
                    hoveredLetter.GetComponent<Outline>().enabled = false;
                }
            }

            // Flashlight
            if (objectHitByRaycast.GetComponent<Flashlight>())
            {
                // Disable the outline of the previously selected item
                if (hoveredFlashlight)
                {
                    hoveredFlashlight.GetComponent<Outline>().enabled = false;
                }

                hoveredFlashlight = objectHitByRaycast.gameObject.GetComponent<Flashlight>();
                hoveredFlashlight.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Flashlight.Instance.SetPickedUp();
                    Flashlight.Instance.FlashlightOn(objectHitByRaycast.gameObject);
                    MeshRenderer mr = objectHitByRaycast.GetComponent<MeshRenderer>();
                    CapsuleCollider bc = objectHitByRaycast.GetComponent<CapsuleCollider>();
                    mr.enabled = false;
                    bc.enabled = false;
                }
            }
            else
            {
                if (hoveredFlashlight)
                {
                    hoveredFlashlight.GetComponent<Outline>().enabled = false;
                }
            }

            if (objectHitByRaycast.GetComponent<Batteries>())
            {
                // Disable the outline of the previously selected item
                if (hoveredBatteries)
                {
                    hoveredBatteries.GetComponent<Outline>().enabled = false;
                }

                hoveredBatteries = objectHitByRaycast.gameObject.GetComponent<Batteries>();
                hoveredBatteries.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Batteries.Instance.PickupBatteries(objectHitByRaycast.gameObject);
                    Destroy(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredBatteries)
                {
                    hoveredBatteries.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}