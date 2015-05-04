using UnityEngine;
using System.Collections;

public class PrintMiniMap : MonoBehaviour
{
    public Camera Main;
    public Camera MiniMap;

	void Update ()
    {
        MiniMap.depth = Main.depth;
	}
}
