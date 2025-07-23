using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using UnityEngine;

public class Portal_Controller : MonoBehaviour
{
    [SerializeField] Transform Destination;


void OnTriggerEnter(Collider other)
{
    
        if (other.CompareTag("Player") && other.TryGetComponent<ThirdPersonController>(out var player))
        {
            player.Teleport(Destination.position, Destination.rotation);
        }

}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Destination.position, .4f);
        var direction = Destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(Destination.position, direction);
       }
}
