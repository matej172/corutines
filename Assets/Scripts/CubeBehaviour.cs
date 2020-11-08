using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    // Update is called once per frame

    private Vector3 target;
    private bool isTargetSet = false;
    private bool isFacingTarget = false;

    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float rotationSpeed = 100;
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                target = hit.point;
                target.y = transform.position.y;
                isTargetSet = true;
                isFacingTarget = false;
            }
        }

        if (isTargetSet)
        {
            Vector3 direction =  target - transform.position;
            Debug.DrawLine(transform.position, target);
            if (isFacingTarget)
            {
                transform.Translate(Vector3.forward * (speed * Time.deltaTime));
                if (Vector3.Distance(target, transform.position) < 0.2f)
                {
                    isTargetSet = false;
                    
                }
            }
            else
            {
                float angle = Vector3.SignedAngle(transform.forward, direction, transform.up);
                if ( Mathf.Abs(angle) > 1)
                {
                    transform.Rotate(transform.up, angle/Mathf.Abs(angle) * rotationSpeed * Time.deltaTime); 
                }
                else
                {
                    isFacingTarget = true;
                }
                
            }
        }
    }
}
