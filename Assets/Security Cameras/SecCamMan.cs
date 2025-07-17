using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecCamMan : MonoBehaviour
{
    public GameObject tvScreen;
    public Material tvScreenOnMaterial;
    public Material tvScreenOffMaterial;
    private bool isOn;
    private MeshRenderer screenMeshRenderer;

	private void Awake()
	{
		screenMeshRenderer = tvScreen.GetComponent<MeshRenderer>();
        screenMeshRenderer.sharedMaterial = tvScreenOnMaterial;
	}
	
    public void SwitchTvOnOFF()
    {
        isOn = !isOn;
        if (isOn)
        {
            screenMeshRenderer.sharedMaterial = tvScreenOnMaterial;
        }

        else
        {
			screenMeshRenderer.sharedMaterial = tvScreenOffMaterial;

		}
	}
}
