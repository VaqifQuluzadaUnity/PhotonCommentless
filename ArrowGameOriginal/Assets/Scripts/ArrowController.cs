using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Rigidbody arrowRb;

    public float despawnTime;

    public float maxFlyDistance;

    Vector3 initialPosition;

    private void Start()
    {
        arrowRb = GetComponent<Rigidbody>();
        arrowRb.isKinematic = true;
        initialPosition = transform.position;
    }

    public void LaunchArrow(float shootForce,float maxDistance)
    {
        maxFlyDistance = maxDistance;
        transform.parent = null;
        arrowRb.isKinematic = false;
        arrowRb.velocity = arrowRb.transform.forward * shootForce;

        InvokeRepeating("MoveShootDistance", 0, Time.deltaTime);
    }

    /// <summary>
    /// We have a max shoot distance. If we pass this distance the 
    /// arrow begins to move parobolic.
    /// </summary>
    private void MoveShootDistance()
    {
        //Debug.Log(Vector3.Distance(initialPosition, transform.position));
        if (Vector3.Distance(initialPosition, transform.position) >= maxFlyDistance)
        {
            InvokeRepeating("ParabolicMovement", 0, Time.deltaTime);
            CancelInvoke("MoveShootDistance");
        }
    }

    private void ParabolicMovement()
    {
        arrowRb.useGravity = true;
        arrowRb.transform.rotation = Quaternion.LookRotation(arrowRb.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        CancelInvoke("ParabolicMovement");
        StickPlace();
    }

    private void StickPlace()
    {
        arrowRb.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(DespawnArrow());
    }

    IEnumerator DespawnArrow()
    {
        yield return new WaitForSecondsRealtime(despawnTime);

        Destroy(gameObject);
    }
}
