using System.Collections;
using UnityEngine;

public class Hero_Controls : MonoBehaviour
{
    public float use_distance = 1.5f;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, use_distance);

            foreach (Collider hitCollider in hitColliders)
            {
                hitCollider.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
            }
        }

    }
}
