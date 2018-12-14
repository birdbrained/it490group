using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIElementDrag : MonoBehaviour
{
    [SerializeField]
    private string draggableTag = "draggable";
    private bool isDragging = false;
    private Vector2 originalPosition;
    private Transform objectToDrag;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetTransformOnMouse();
            if (objectToDrag != null)
            {
                isDragging = true;
                objectToDrag.SetAsLastSibling();
                originalPosition = objectToDrag.position;
            }
        }

        if (isDragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (objectToDrag != null)
            {
                Transform objectToReplace = GetTransformOnMouse();
                if (objectToReplace != null)
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToReplace.position = originalPosition;
                }
                else
                {
                    objectToDrag.position = originalPosition;
                }

                objectToDrag = null;
            }
            isDragging = false;
        }
	}

    private GameObject GetObjectOnMouse()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0)
        {
            return null;
        }
        foreach (RaycastResult result in hitObjects)
        {
            Debug.Log(result.gameObject.name);
            if (result.gameObject.tag == draggableTag)
            {
                return result.gameObject;
            }
        }
        return null;
    }

    private Transform GetTransformOnMouse()
    {
        GameObject clickedObject = GetObjectOnMouse();
        if (clickedObject != null && clickedObject.tag == draggableTag)
        {
            return clickedObject.transform;
        }
        return null;
    }
}
