using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject followTarget;
	private Vector3 _targetPos;
	public float moveSpeed;
	
	void Update () 
	{
		var targetPosition = followTarget.transform.position;
		
		_targetPos = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
		
		var velocity = _targetPos - transform.position;
		transform.position = Vector3.SmoothDamp (transform.position, _targetPos, ref velocity, 1.0f, moveSpeed * Time.deltaTime);	
	}
}
