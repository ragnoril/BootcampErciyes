using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderControl : MonoBehaviour
{
    public Material DissolveMaterial;

    public GameObject ObjectToChange;

    private void Start()
    {
        DissolveMaterial = ObjectToChange.GetComponent<Renderer>().sharedMaterial;
    }

    public void UpdateDissolveAmount(float value)
    {
        if (DissolveMaterial != null)
        {
            DissolveMaterial.SetFloat("_Cutoff", value);
        }
    }
}
