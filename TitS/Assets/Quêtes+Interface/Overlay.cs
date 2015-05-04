using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour
{
    private string Missions;
    private string Text;

    void OnGUI ()
    {        
        GUI.Box(new Rect(Screen.width - 250, 0, 250, 50), "MISSION PRINCIPALE: \n Dérober le tableau");        
    }
}
