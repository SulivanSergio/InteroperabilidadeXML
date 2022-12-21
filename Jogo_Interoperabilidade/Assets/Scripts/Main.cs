using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    public bool utilizaJson = true;

    public Json json;
    public TMP_Text erro;

    Xml xml;
    JogoXml jogoXml;
    string textoXml;

    public TMP_Text turno;
    public GameObject caixa;

    public Transform imagemForca;

    void Start()
    {
        caixa.SetActive(true);
        if (utilizaJson)
        {
            json = new Json(erro);
            
            json.Load();
        }
        else
        {

            jogoXml = new JogoXml();
            xml = new Xml();
            try
            {
                textoXml = xml.LendoXML();
                jogoXml = xml.converterXmlParaObjeto(textoXml);
            }
            catch (Exception e)
            {
                jogoXml.turno = -1;
                jogoXml.vencedor = " ";

                xml.EscreveXML(jogoXml.turno, jogoXml.vencedor);

            }


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

        Turno();

    }

    private void Turno()
    {
        if (utilizaJson)
        {
            if (json.turno == -1)
            {
                turno.SetText("Jogador 1");
            }
            if (json.turno == 1)
            {
                turno.SetText("Jogador 2");
            }
        }
        else
        {
            if (jogoXml.turno == -1)
            {
                turno.SetText("Jogador 1");
            }
            if (jogoXml.turno == 1)
            {
                turno.SetText("Jogador 2");
            }
        }
    }

    public void Continuar()
    {
        imagemForca.transform.localScale = new Vector3(0.1f, 0.2f, 1);

        caixa.SetActive(false);
    }
    public void Reiniciar()
    {
        if (utilizaJson)
        {
            json.turno = -1;
            json.vencedor = "";
            json.Save();
        }
        else
        {

            jogoXml.turno = -1;
            jogoXml.vencedor = " ";
            xml.EscreveXML(jogoXml.turno, jogoXml.vencedor);
        }

        caixa.SetActive(false);
        imagemForca.transform.localScale = new Vector3(0.1f, 0.2f, 1);
    }


}
