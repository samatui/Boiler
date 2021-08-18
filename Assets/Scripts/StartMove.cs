using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{
    private Vector3 position;
    public float speed;
    private Hashtable ht = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        MoveTest1.isMove = false;
        position = this.transform.position;
        this.GetComponent<MoveTest1>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position += new Vector3(0,0,-Time.deltaTime*speed);
        iTween.MoveTo(this.gameObject, new Vector3(0, 0, 0), 3f);
        if (this.transform.position.z <= 0)
        {
            this.GetComponent<MoveTest1>().enabled = true;
            this.GetComponent<StartMove>().enabled = false;
        }

    }
}
