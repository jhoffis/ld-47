using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BuildingPlace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mPos.x, mPos.y, 0);
        Debug.Log(transform.position.ToString());
    }

    public void Create()
    {
        Debug.Log("CREATE");
        
    }
}
