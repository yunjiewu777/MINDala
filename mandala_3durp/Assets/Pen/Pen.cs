using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;

    [Header("Hands & Grabbable")]
    public OVRGrabber rightHand;
    public OVRGrabber leftHand;
    public OVRGrabbable grabbable;
    public AudioClip audioClip;
    private AudioSource audioSource;

    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;

    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void Update()
    {
        bool isGrabbed = grabbable.isGrabbed;
        bool isRightHandDrawing = isGrabbed && grabbable.grabbedBy == rightHand && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        bool isLeftHandDrawing = isGrabbed && grabbable.grabbedBy == leftHand && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        if (isRightHandDrawing || isLeftHandDrawing)
        {
            Draw();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
            audioSource.Stop();
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            SwitchColor();
        }
    }

    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    public void ClearStrokes()
    {
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
        lineRenderers.Clear();
    }

    private void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            GameObject lineObject = new GameObject("Line");
            lineObject.transform.SetParent(transform.parent); // Set the parent of the new line to the same parent as the pen
            lineObject.layer = gameObject.layer; // Set the layer of the new line to the same layer as the pen

            currentDrawing = lineObject.AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.position);
            lineRenderers.Add(currentDrawing);
            audioSource.Play();
        }
        else
        {
            var currentPos = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPos, tip.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.position);
            }
        }
    }
    // private void Draw()
    // {
    //     if (currentDrawing == null)
    //     {
    //         index = 0;
    //         currentDrawing = new GameObject().AddComponent<LineRenderer>();
    //         currentDrawing.material = drawingMaterial;
    //         currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
    //         currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
    //         currentDrawing.positionCount = 1;
    //         currentDrawing.SetPosition(0, tip.position);

    //         audioSource.Play();
    //     }
    //     else
    //     {
    //         var currentPos = currentDrawing.GetPosition(index);
    //         if (Vector3.Distance(currentPos, tip.position) > 0.01f)
    //         {
    //             index++;
    //             currentDrawing.positionCount = index + 1;
    //             currentDrawing.SetPosition(index, tip.position);
    //         }
    //     }
    // }

    private void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }
}