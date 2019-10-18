using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionScript : MonoBehaviour {


	public GameObject CharacBody;


	public Vector3 UpPosition = new Vector3 (0, 0.5f, 0);
	public Vector3 crouchPosition = new Vector3 (0, 0, 0);

	public float LocalX;
	public float LocalY;
	public float LocalZ;


	public void CrouchDown()
	{
		//CharacBody.transform.localScale = new Vector3 (1, 0.5f, 1);

		LocalX = CharacBody.transform.localPosition.x;

		LocalY = CharacBody.transform.localPosition.y;

		LocalZ = CharacBody.transform.localPosition.z;

		CharacBody.transform.localPosition = new Vector3 (LocalX, LocalY - 0.5f, LocalZ);





	}

	public void CrouchUp()
	{

		LocalX = CharacBody.transform.localPosition.x;

		LocalY = CharacBody.transform.localPosition.y;

		LocalZ = CharacBody.transform.localPosition.z;

		CharacBody.transform.localPosition = new Vector3 (LocalX, LocalY + 0.5f, LocalZ);


	}
}
