using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SqeezeCreation : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Vector3 startPos;
    private Vector3 screenPoint;
    private RectTransform productRect;
    private Rigidbody2D rigidbody;
    private Vector3 rectWorld;
    private float yMax;
    private float yMin;

    public bool beingDragged = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = true;
        rigidbody.velocity = Vector2.zero;
        startPos = rigidbody.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (beingDragged)
        {
            /* Manage the state ALL Layout Elements are in. We need the objects to Obey at first to organize themselves using the Vertical Layout group of the parent.
             * After they are placed we then need to keep track if its being dragged, if it is, then we diasble the layout element so that they do not re-oganize and
             * mess up the list.
            */
            DisableAllElements(beingDragged);

            //Get the camera Screne to World point and then use ONLY the y axis. Maintain X axis from the OnBeginDrag function. Z does not change because this is 2D yo.
            var worldpoint = Camera.main.ScreenToWorldPoint(eventData.position);
            var position = new Vector2(startPos.x,worldpoint.y);

            rigidbody.MovePosition(position);
            rigidbody.velocity = Vector2.zero;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = false;
        DisableAllElements(beingDragged);
        rigidbody.MovePosition(startPos);
    }

    void Start()
    {
        productRect = GameObject.FindGameObjectWithTag("NewProduct").GetComponent<RectTransform>();
        rigidbody = GetComponent<Rigidbody2D>();
        rectWorld = Camera.main.ScreenToWorldPoint(productRect.rect.position);
    }

    void DisableAllElements(bool state)
    {
        var elements = transform.parent.GetComponentsInChildren<LayoutElement>();

        foreach (var item in elements)
        {
            item.ignoreLayout = state;
        }
    }

}
