using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUnitRaycastInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float maxRayDistance = 5f;
    public LayerMask interactableLayer;
    public GameObject camControlUnit;
    public Image camControlUnitPromptUi;
    public SecCamMan camManager;
    private bool isLookingAtControlUnit = false;


    // Start is called before the first frame update
    void Start()
    {
		camControlUnitPromptUi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxRayDistance, interactableLayer))
        {
            if (hit.collider.gameObject == camControlUnit)
            {
				if (!isLookingAtControlUnit)
				{
					isLookingAtControlUnit = true;
					camControlUnitPromptUi.gameObject.SetActive(true);
				}




				if (Input.GetMouseButtonDown(0))
                {
                    camManager.SwitchTvOnOFF();
                }
            }
            else
            {
                if (isLookingAtControlUnit)
                {
                    isLookingAtControlUnit = false;
                    camControlUnitPromptUi.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (isLookingAtControlUnit)
            {
                isLookingAtControlUnit = false;
                camControlUnitPromptUi.gameObject.SetActive(false);
            }
        }
    }
}
