using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{

    public Json json;
    public TMP_Text erro;



    public TMP_Text turno;
    public GameObject caixa;

    public Transform imagemForca;

    void Start()
    {
        json = new Json(erro);
        caixa.SetActive(true);
        json.Load();
        

    }

    
    void Update()
    {
        json.Load();

        Turno();

    }

    private void Turno()
    {

        if(json.turno == -1)
        {
            turno.SetText("Jogador 1");
        }
        if (json.turno == 1)
        {
            turno.SetText("Jogador 2");
        }
    }

    public void Continuar()
    {
        imagemForca.transform.localScale = new Vector3(0.1f, 0.2f, 1);

        caixa.SetActive(false);
    }
    public void Reiniciar()
    {
        json.turno = -1;
        json.vencedor = "";
        json.Save();
        caixa.SetActive(false);

        imagemForca.transform.localScale = new Vector3(0.1f, 0.2f, 1);
    }


}
