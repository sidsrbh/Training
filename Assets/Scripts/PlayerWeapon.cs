using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
   
    [SerializeField] BlasterShot _blasterShotPrefab;
   
    [SerializeField] Transform _firePoint;
   
    [SerializeField] LayerMask _aimLayerMask;
  
    // Update is called once per frame
    void Update()
    {

        if (GetComponent<PlayerMovement>().pv.IsMine)
        {
            AimTowardMouse();

            if (transform.rotation.eulerAngles.z > 0 || transform.rotation.eulerAngles.x > 0 || transform.rotation.eulerAngles.z < 0 || transform.rotation.eulerAngles.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask))
        {

            var destination = hitInfo.point;
            destination.y = transform.position.y;

            Vector3 direction = destination - transform.position;
            direction.Normalize();

            transform.rotation = Quaternion.LookRotation(direction *Time.deltaTime, Vector3.up);          
        }
    }

    void Fire()
    {
        BlasterShot shot = Instantiate(_blasterShotPrefab, _firePoint.position, transform.rotation);
        shot.Launch( transform.forward);

        Destroy(shot.gameObject, 3);
    }
}
