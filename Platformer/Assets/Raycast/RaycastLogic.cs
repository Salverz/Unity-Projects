using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLogic : MonoBehaviour
{
    public GameObject package;
    public GameObject parachute;
    public float deploymentHeight = 7.5f;
    public float parachuteDrag = 5f;
    public float landingHeight = 1f;
    public float chuteOpenDuration = 0.5f;

    float originalDrag;

    // Start is called before the first frame update
    void Start()
    {
        // originalDrag = package.GetComponent<Rigidbody>().drag;
        // parachute.SetActive(false);

        parachute.SetActive(true);
        StartCoroutine(ExpandParachute());
    }

    IEnumerator ExpandParachute()
    {
        parachute.transform.localScale = Vector3.zero;
        float timeElapsed = 0f;

        while (timeElapsed < chuteOpenDuration)
        {
            float newScale = timeElapsed / chuteOpenDuration;
            parachute.transform.localScale = new Vector3(newScale, newScale, newScale);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        parachute.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(package.transform.position, Vector3.down, out hitInfo, deploymentHeight))
        {
            package.GetComponent<Rigidbody>().drag = parachuteDrag;
            parachute.SetActive(true);
            Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.red);

            if (hitInfo.distance < landingHeight)
            {
                Debug.Log("Landed");
                parachute.SetActive(false);
            }
        }
        else
        {
            parachute.SetActive(false);
            package.GetComponent<Rigidbody>().drag = originalDrag;
            Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.cyan);
        }

        
    }
}
