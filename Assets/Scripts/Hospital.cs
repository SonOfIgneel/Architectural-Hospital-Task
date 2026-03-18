using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hospital : MonoBehaviour
{
    public Animator door;

    public Transform startPoint;
    public Transform endPoint;

    public Transform startWallPoint;
    public Transform endWallPoint;

    public GameObject wall, layer, wallPanel;

    public float moveDuration = 3f;

    private float timeElapsed = 0f;

    private bool isMoving = false, wallClick = false;

    public float targetAlpha = 0.25f;

    public GameObject layer1, layer2, layer3;

    private void Start()
    {

    }

    public void Layer1()
    {
        layer1.SetActive(true);
    }

    public void Layer2()
    {
        layer1.SetActive(false);
        layer2.SetActive(true);
    }

    public void Layer3()
    {
        layer1.SetActive(false);
        layer2.SetActive(false);
        layer3.SetActive(true);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == wall && !wallClick)
                {
                    isMoving = false;
                    timeElapsed = 0f;
                    moveDuration = 3f;
                    wallClick = true;
                    layer.SetActive(false);
                    hit.collider.enabled = false;
                }
            }
        }

        if (isMoving && timeElapsed < moveDuration)
        {
            timeElapsed += Time.deltaTime;

            float t = timeElapsed / moveDuration;

            Camera.main.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startPoint.rotation, endPoint.rotation, t);
        }
        else if (wallClick && timeElapsed < moveDuration)
        {
            timeElapsed += Time.deltaTime;

            float t = timeElapsed / moveDuration;

            Camera.main.transform.position = Vector3.Lerp(startWallPoint.position, endWallPoint.position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startWallPoint.rotation, endWallPoint.rotation, t);

            if(timeElapsed > 2.9f)
            {
                wallPanel.SetActive(true);
            }
        }
    }

    public void ClickDoor()
    {
        StartCoroutine(ClickDoorCoroutine());
        isMoving = true;
    }

    public IEnumerator ClickDoorCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        door.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
