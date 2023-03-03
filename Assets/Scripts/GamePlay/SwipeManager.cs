using System;
using UnityEngine;
using UnityEngine.Events;

namespace GamePlay
{
  public class SwipeManager : MonoBehaviour
  {
    [SerializeField ]private float swipeThreshold = 50f;
    [SerializeField] private float swipeXOffset;
    [SerializeField] private float xClamplimit;
    private Vector2 _mouseDownPosition;
    private Vector2 _mouseUpPosition;
   
    
    private void Update ()
    {
      if (Input.GetMouseButtonDown(0)) 
        _mouseDownPosition = Input.mousePosition;
      if (!Input.GetMouseButtonUp(0)) return;
      _mouseDownPosition = Input.mousePosition;
      CheckSwipe();

    }

    private void CheckSwipe()
    {
      SwipeInHorizontal();
      _mouseUpPosition =_mouseDownPosition;
    }
    
    private void SwipeInHorizontal()
    {
      var deltaX = this._mouseDownPosition.x - this._mouseUpPosition.x;
      if (!(Mathf.Abs(deltaX) > this.swipeThreshold)) return;
      switch (deltaX)
      {
        case > 0:
          Debug.Log("right");
          MoveToHorizontal(swipeXOffset);
          break;
        case < 0:
          Debug.Log("left");
          MoveToHorizontal(-swipeXOffset);
          break;
      }
    }

    private void SwipeInVertical()
    {
      var deltaY = _mouseDownPosition.y - _mouseUpPosition.y;
      if (!(Mathf.Abs(deltaY) > this.swipeThreshold)) return;
      switch (deltaY)
      {
        case > 0:
          Debug.Log("up");
          break;
        case < 0:
          Debug.Log("down");
          break;
      }
      
      
      
      
    }
    
    private void MoveToHorizontal(float xOffset)
    {
      var transform1 = transform;
      var position = transform1.position;
      
      position = new Vector2( Mathf.Clamp(position.x + xOffset,-xClamplimit,xClamplimit), position.y);
      transform1.position = position;
    }
    
    
    
    
    
  }


}
