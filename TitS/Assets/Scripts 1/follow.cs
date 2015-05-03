using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	public Transform target;
	public float distance = 1.6f;
	public float height = 1f;
	public float heightDamping = 1.0f;
	public float rotationDamping = 3.0f;
    private NetworkView _ntView; 
	// Use this for initialization
	void Awake () {
        _ntView = GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_ntView.isMine)
        {
            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.transform.position.y;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            //Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

            transform.position = target.position;
            Vector3 temp = transform.position;
            temp.y = 0;
            transform.position = temp;
        }
	}
}
