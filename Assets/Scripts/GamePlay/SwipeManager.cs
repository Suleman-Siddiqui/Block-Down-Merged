using System;
using UnityEngine;
using UnityEngine.Events;

namespace GamePlay
{
  public class SwipeManager : MonoBehaviour
  {
    [SerializeField] private float swipeXOffset;
    [SerializeField] private float xClamplimit;
    private Vector2 _mouseDownPosition;
    private Vector2 _mouseUpPosition;
    // If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
	private const float MAX_SWIPE_TIME = 0.5f; 
	// Factor of the screen width that we consider a swipe
	// 0.17 works well for portrait mode 16:9 phone
	private const float MIN_SWIPE_DISTANCE = 0.17f;

	private Vector2 _startPos;
	private float _startTime;

	public void Update()
	{
			if(Input.GetMouseButtonDown(0))
			{
				Vector2 mouseTouchPos = Input.mousePosition;
				_startPos = new Vector2(mouseTouchPos.x/(float)Screen.width, mouseTouchPos.y/(float)Screen.width);
				_startTime = Time.time;
			}
			if (!Input.GetMouseButtonUp(0)) return;
			if (Time.time - _startTime > MAX_SWIPE_TIME) // press too long
				return;
				
			var mouseTouchUpPos = Input.mousePosition;
			var endPos = new Vector2(mouseTouchUpPos.x/(float)Screen.width, mouseTouchUpPos.y/(float)Screen.width);
			var swipe = new Vector2(endPos.x - _startPos.x, endPos.y - _startPos.y);

			if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
				return;

			if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) { // Horizontal swipe
				if (swipe.x > 0) {
					print("Swipe Right ");
					MoveToHorizontal(swipeXOffset);
				}
				else {
					print("Swipe Left ");
					MoveToHorizontal(-swipeXOffset);
				}
			}
			else { // Vertical swipe
				if (swipe.y > 0) {
					print("Swipe Up ");
				}
				else {
					print("Swipe down ");
				}
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
