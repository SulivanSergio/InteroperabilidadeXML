using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tiro : MonoBehaviour
{

    Rigidbody2D rig;
    public GameObject ponto;
    public GameObject arm;

    public Transform imagemForca;

    public TMP_Text textJogador;
    public string nomeJogador;

    private Vector3 direction;
    public float force;

    public KeyCode teclaTiro;
    

    public float[] minMax = new float[2];

    public bool atirou = false;
    public bool diminui = false;

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

    }
    private void FixedUpdate()
    {
        
        tirinho();
    }

    private void tirinho()
    {
        if(atirou == false)
        {
            rig.Sleep();
        }
        else
        {
            
        }
        
        if(Input.GetKey(KeyCode.F))
        {

            if(diminui == false)
            {
                force += Time.fixedDeltaTime *10f;
                imagemForca.transform.localScale += new Vector3(force / 1000f,0,0);
                if(force > minMax[1])
                {
                    diminui = true;
                }
            }
            if (diminui == true)
            {
                force -= Time.fixedDeltaTime * 10f;
                imagemForca.transform.localScale -= new Vector3(force / 1000f, 0, 0);
                if (force < minMax[0])
                {
                    diminui = false;
                }
            }


        }

        if (Input.GetKey(teclaTiro))
        {
            if (atirou == false)
            {
                transform.position = arm.transform.position;
                direction = transform.position - ponto.transform.position;

                rig.AddForce(new Vector2(-direction.normalized.x * force, -direction.normalized.y * force), ForceMode2D.Impulse);

                force = minMax[0];
                imagemForca.transform.localScale = new Vector3(0.1f, 0.2f, 1);
                atirou = true;
                

            }
            
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(nomeJogador))
        {
            json.vencedor = nomeJogador;
            textJogador.text = "Acertou";
            SetPositionTiro();


        }
        else
        {
            if (collision.gameObject.CompareTag("Jogador1") || collision.gameObject.CompareTag("Jogador2"))
            {

            }
            else
            {
                textJogador.text = "Errou";
                SetPositionTiro();
                json.SetTurno();
                json.Save();
            }
        }

    }

    public void SetPositionTiro()
    {

        transform.position = new Vector3(1000, 1000,0);
        atirou = false;
    }
            


}
