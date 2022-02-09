using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ShootController : MonoBehaviourPunCallbacks
{
    public float shootDistance;

    public Transform crosshairAimShower;

    public Transform arrowForwardPointer;

    public Image crosshairImage;

    public float pullForce;

    public float stringDrawMax = -0.8f;

    public GameObject arrowPrefab;

    public Transform arrowSpawnPoint;

    public LineRenderer arrowString;

    public float shootForce;

    public GameObject arrowInstance;

    [SerializeField] private Camera playerCamera;

    private void OnDrawGizmos()
    {
        Ray arrowRay = new Ray(arrowForwardPointer.position, arrowForwardPointer.forward); //* shootDistance);
        Gizmos.color = Color.red;

        Gizmos.DrawRay(arrowForwardPointer.position,arrowForwardPointer.forward*shootDistance);
    }
    private void Start()
    {
        crosshairAimShower.transform.position = new Vector3(crosshairAimShower.transform.position.x, arrowForwardPointer.transform.position.y, arrowForwardPointer.transform.position.z + shootDistance);
    }

    private void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        SetCrosshairToTarget();
        if (Input.GetMouseButtonDown(0))
        {
            SpawnArrow();
        }
    }

    private void SetCrosshairToTarget()
    {
        Ray arrowRay = new Ray(arrowForwardPointer.position, arrowForwardPointer.forward); //* shootDistance);
        RaycastHit hit;
        if (Physics.Raycast(arrowRay,out hit))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Arrow"))
            {
                Debug.Log(Vector3.Distance(arrowForwardPointer.position, hit.point));
                crosshairImage.rectTransform.position = playerCamera.WorldToScreenPoint(hit.point);
            }
        }
        else
        {
            crosshairImage.rectTransform.position = playerCamera.WorldToScreenPoint(crosshairAimShower.position);
        }
    }

    private void SpawnArrow()
    {
        arrowInstance = Instantiate(arrowPrefab, arrowSpawnPoint);
        InvokeRepeating("AimArrow", 0, Time.deltaTime);
    }
    private void AimArrow()
    {
        float arrowDrawMaxValue = arrowSpawnPoint.transform.localPosition.z-Time.deltaTime;
        if (arrowDrawMaxValue < stringDrawMax || Input.GetMouseButtonUp(0))
        {
            InvokeRepeating("ReleaseArrow", 0, Time.deltaTime);
            pullForce = Mathf.Abs(arrowSpawnPoint.transform.localPosition.z);
            CancelInvoke("AimArrow");
           
        }
        arrowSpawnPoint.transform.localPosition = new Vector3(arrowSpawnPoint.transform.localPosition.x, arrowSpawnPoint.transform.localPosition.y, arrowDrawMaxValue);
        arrowString.SetPosition(1, arrowSpawnPoint.transform.localPosition);
    }


    private void ReleaseArrow()
    {

        if (Input.GetMouseButtonUp(0))
        {
            arrowInstance.transform.parent = null;
            arrowInstance.GetComponent<ArrowController>().LaunchArrow(shootForce*pullForce,shootDistance);
            ReplaceStringCenter();
        }
    }

    private void ReplaceStringCenter()
    {
        arrowSpawnPoint.transform.localPosition = new Vector3(arrowSpawnPoint.transform.localPosition.x, arrowSpawnPoint.transform.localPosition.y, -0.3f);
        arrowString.SetPosition(1, arrowSpawnPoint.transform.localPosition);
    }
}
