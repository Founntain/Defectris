using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public Vector3 Value1;
    public Vector3 Value2;
    public bool Forward = true;

    // Update is called once per frame
    void Update()
    {
        if(Forward && transform.position != Value2){
            transform.position = Vector3.Lerp(transform.position, Value2, 10 * Time.deltaTime);

            return;
        }else if(Forward && transform.position == Value2){
            Forward = false;

            return;
        }

        if(!Forward && transform.position != Value1){
            transform.position = Vector3.Lerp(transform.position, Value1, 10 * Time.deltaTime);

            return;
        }else if(!Forward && transform.position == Value1){
            Forward = true;

            return;
        }

    }
}
