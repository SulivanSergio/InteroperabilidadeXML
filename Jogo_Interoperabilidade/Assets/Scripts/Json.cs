using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;



[Serializable]
public class Json 
{
   
    

    

    public int turno = -1;

    public string vencedor = "";

    private string caminho = @"D:\Documentos\Ifrj\Sexto periodo\Interoperabilidade\Projeto\Build\Json.txt";

    private TMP_Text texto;

    public Json(TMP_Text texto)
    {
        this.texto = texto;
       

    }

    public void Load()
    {

        try
        {
            var content = File.ReadAllText(caminho);
            var p = JsonUtility.FromJson<Json>(content);
            turno = p.turno;
            vencedor = p.vencedor;

        }catch (Exception e)
        {
            texto.SetText("Arquivo não existe");

            Save();
        }
        
        

        


    }

    public void Save()
    {

        var content = JsonUtility.ToJson(this,true);
        File.WriteAllText(caminho,content);
        texto.SetText("Salvou");
    }

    public void SetTurno()
    {
        turno = turno * -1;
        
    }
    



}
