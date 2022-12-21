using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public bool utilizaJson = true;

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

    Xml xml;
    JogoXml jogoXml;
    string textoXml;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        if (utilizaJson)
        {
            json = new Json(erro);
        }
        else
        {
            xml = new Xml();
            jogoXml = new JogoXml();
        }
    }

    
    void Update()
    {
        if (utilizaJson)
        {
            json.Load();
        }
        else
        {

            textoXml = xml.LendoXML();
            jogoXml = xml.converterXmlParaObjeto(textoXml);
        }


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
            if(nomeJogador == "Jogador1")
            {
                if (utilizaJson)
                {
                    json.vencedor = "Jogador2";
                    json.Save();
                }
                else
                {
                    jogoXml.vencedor = "Jogador2";
                    xml.EscreveXML(jogoXml.turno, jogoXml.vencedor);
                }
            }
            else
            {
                if (utilizaJson)
                {
                    json.vencedor = "Jogador1";
                    json.Save();
                }
                else
                {
                    jogoXml.vencedor = "Jogador1";
                    xml.EscreveXML(jogoXml.turno, jogoXml.vencedor);
                }

            }
            
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

                if (utilizaJson)
                {
                    json.SetTurno();
                    json.Save();
                }
                else
                {

                    jogoXml.SetTurno();
                    xml.EscreveXML(jogoXml.turno, jogoXml.vencedor);
                }
            }
        }

    }

    public void SetPositionTiro()
    {

        transform.position = new Vector3(1000, 1000,0);
        atirou = false;
    }
            


}
