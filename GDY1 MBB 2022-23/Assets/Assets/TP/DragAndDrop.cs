using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragAndDrop : MonoBehaviour
{
    // Stores the touch input
    private Touch _touch;
    // Stores the hit item
    private Transform _hitItem;
    // Stores the touch location
    private Vector3 _touchLoc;
    // Update is called once per frame
    void Update()
    {
        // Convert screen space to world space aka converts pixels to units
        _touchLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // zero out the z value since its 2d
        _touchLoc.z = 0f;

        // When Button is pressed once
        if (Input.GetMouseButtonDown(0))
        {
            // send out a ray and store its information
            // Casts a ray against Colliders in the Scene
            // This function returns a RaycastHit object with a reference to the Collider that is hit by the ray
            RaycastHit2D hit = Physics2D.Raycast(_touchLoc, Vector2.zero);

            // checks if it hit 
            if (hit.collider != null)
            {
                // if it did hit something, store it in transform
                _hitItem = hit.transform;
            }
        }

        // When button is held
        if (Input.GetMouseButton(0))
        {
            // set the hit item's position to the touch location
            _hitItem.position = _touchLoc;
        }
    }
} 
