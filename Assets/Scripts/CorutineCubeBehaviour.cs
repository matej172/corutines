using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorutineCubeBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float rotationSpeed = 100;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 target = hit.point;

                target.y = transform.position.y;

                float angle = Vector3.SignedAngle(transform.forward, target, Vector3.up);
                StartCoroutine("RotateToTarget", angle);
                // StopCoroutine("WalkToTarget");
                // StartCoroutine("WalkToTarget", target);
            }
        }
    }

    IEnumerator WalkToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        while (Vector3.Distance(target, transform.position) > 0.1f)
        {
            transform.Translate(direction.normalized * (speed * Time.deltaTime));
            yield return null;
        }
    }

    IEnumerator RotateToTarget(float angle)
    {
        while (Mathf.Abs(angle) > 1f)
        {
            float rotationAmount = rotationSpeed * (angle/Mathf.Abs(angle)) * Time.deltaTime;
            transform.Rotate(transform.up, rotationAmount);
            angle -= rotationAmount;
            yield return null;
        }
    }
}
