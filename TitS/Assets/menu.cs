using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	public static float inter;

	// Use this for initialization
public void ChangeToScene (string scenes)
	{
		Application.LoadLevel(scenes);
	}
public void Quitsc()
{
    Application.Quit();
}
	public void Changeint (float inten)
	{
		//rotat.intensity = inten;
        
		inter = inten;
	}
}
