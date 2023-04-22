using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapObjects : MonoBehaviour
{

    public GameObject obj1, obj2;

    private BoxCollider col;

    public Transform other;

    public enum State {
        Wolf,
        Sheep
    }

    public enum HuntState{
        LookTowardsPrey,
        MoveTowardsPrey
    }

    public State currentState;
    public HuntState currentHuntState;
    
    void Start(){
        col = GetComponent<BoxCollider>();
        CalculateBounds(obj1);
        obj2.SetActive(false);
        StartCoroutine(Movement());
    }

    void Update(){
        switch(currentState){
            case State.Wolf:
                Hunt();
                break;
            case State.Sheep:
            //do nothing
                break;
        }

        if(Input.GetAxis("Vertical") != 0){
            //StartCoroutine(Movement());
        }else{
            //StopCoroutine(Movement());
        }
    }

    IEnumerator Movement(){
        Debug.Log("Active Coroutine");
        
        transform.Translate(Vector3.forward * 0.02f * Input.GetAxis("Vertical"));
        yield return new WaitForSeconds(0.2f);
        
    }

    void OnTriggerEnter(){
        Debug.Log("Show object 1");
        currentState = State.Sheep;
        //CalculateBounds(obj1);
        obj1.SetActive(true);
        obj2.SetActive(false);
    }

    void OnTriggerExit(){
        Debug.Log("Show obj 2");
        //CalculateBounds(obj2);
        currentState = State.Wolf;
        obj2.SetActive(true);
        obj1.SetActive(false);
    }

    [ContextMenu("Recalculate Box Collider Bounds")]
    public void CalculateBounds(GameObject obj){
        obj.SetActive(true);
        
        col.center = transform.InverseTransformPoint(obj.GetComponentInChildren<Renderer>().bounds.center);
        col.size = obj.GetComponentInChildren<Renderer>().bounds.size;
        //Bounds bon = new Bounds()
       // obj1.GetComponent<Renderer>().bounds
    }

    void Hunt(){
        switch(currentHuntState){
            case HuntState.LookTowardsPrey:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(other.position - transform.position), 10f * Time.deltaTime);
                if(Vector3.Distance(Vector3.Normalize(transform.forward), Vector3.Normalize(other.position - transform.position)) < 0.05f){
                    Debug.Log("Close aligned");
                    currentHuntState = HuntState.MoveTowardsPrey;
                }
                break;
            case HuntState.MoveTowardsPrey:
                transform.Translate(Vector3.forward * 5f * Time.deltaTime);
                break;
        }
    }

    void OnDrawGizmosSelected(){
        //
        //Gizmos.DrawWireCube(obj2.GetComponentInChildren<Renderer>().bounds.center, obj2.GetComponentInChildren<Renderer>().bounds.size);
    }
}
