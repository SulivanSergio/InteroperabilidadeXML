using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rig;
    
    
    public GameObject armTank;
    
    float angle = 0;
    
    public float speed = 10;

    public Json json;
    public TMP_Text erro;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        json = new Json(erro);
        
    }

   
    void Update()
    {
        json.Load();
        if (json.turno == -1)
        {
            movimentacao();
            ArmMovimento();
        }
    }

    private void movimentacao()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rig.velocity += new Vector2(speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rig.velocity -= new Vector2(speed * Time.deltaTime, 0);
        }
    }

    private void ArmMovimento()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (angle < 90)
            {
                angle += 100 * Time.deltaTime;
                armTank.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (angle > 0)
            {
              
                angle -= 100 * Time.deltaTime;
                armTank.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }
        }

       
    }


    

}
