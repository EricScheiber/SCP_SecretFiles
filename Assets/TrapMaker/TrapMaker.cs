using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class TrapMaker : MonoBehaviour
{
    /// <summary>
    /// Trap which block up part of corridor
    /// </summary>
    [Range(0,5)]
    public float UpTrapSpace = 1, DownTrapHeight = 1;
    public Transform Trap, TriggerZone;

    private Vector3 centerDownTrap, sizeDownTrap, centerUpTrap, sizeUpTrap;

    private void DrawTrap()
    {
        sizeDownTrap = Trap.transform.localScale;
        sizeDownTrap.y = DownTrapHeight;
        centerDownTrap = Trap.transform.position - Vector3.up * ((Trap.transform.localScale.y - DownTrapHeight) / 2);

        sizeUpTrap = Trap.transform.localScale;
        sizeUpTrap.y = Trap.transform.localScale.y - UpTrapSpace;
        centerUpTrap = Trap.transform.position + Vector3.up * ((Trap.transform.localScale.y - sizeUpTrap.y) / 2);

        //Draw shape of down trap
        Gizmos.color = new Color(0, 0, 1, .3f);
        Gizmos.DrawCube(centerDownTrap, sizeDownTrap);


        //Draw shape of up trap
        Gizmos.color = new Color(1, 0, 0, .3f);
        Gizmos.DrawCube(centerUpTrap, sizeUpTrap);

        //Draw wired shape of down trap
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(centerDownTrap, sizeDownTrap);

        //Draw wired shape of up trap
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerUpTrap, sizeUpTrap);
    }

    void DrawTrigger()
    {
        if(TriggerZone)
        {
            Gizmos.color = new Color(0,1,0,.3f);
            Gizmos.DrawCube(TriggerZone.transform.position, TriggerZone.transform.localScale);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(TriggerZone.transform.position, TriggerZone.transform.localScale);
        }
    }


    private void OnDrawGizmos()
    {
        List<GameObject> selectedObjects = new List<GameObject>();
        selectedObjects.AddRange(Selection.gameObjects);

        if (selectedObjects.Contains(gameObject) || selectedObjects.Contains(TriggerZone.gameObject) || selectedObjects.Contains(Trap.gameObject))
        {
            DrawTrap();
            DrawTrigger();
        }


        //Draw general wired shape of trap
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Trap.transform.position, Trap.transform.localScale);
    }

    // Start is called before the first frame update
    void Start()
    {
        Trap = transform.Find("Trap");
        if(!Trap)
        {
            Trap = new GameObject("Trap").transform;
            Trap.parent = transform;
        }
        TriggerZone = transform.Find("TriggerZone");
        if(!TriggerZone)
        {
            TriggerZone = new GameObject("TriggerZone").transform;
            TriggerZone.parent = transform;
            TriggerZone.gameObject.AddComponent<BoxCollider>();
        }
    }

}
