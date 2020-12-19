using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovaFrameTool : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    [SerializeField]private RectTransform self;
    private bool isMove;
    private Vector2 Indent;

    private void LateUpdate(){
        if(isMove){
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector3)Indent;
            self.position = new Vector3(
                pos.x , 
                pos.y , 
                0);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isMove = true;
        Indent = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)self.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMove = false;
        Indent = Vector2.zero;
    }
}
