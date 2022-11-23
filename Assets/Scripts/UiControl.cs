using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControl : MonoBehaviour
{
    public GameObject Instrucciones;
    public GameObject Ui1;
    public GameObject Ui2;
    // Activa en el inicio el ui1 y las instrucciones 
    void Start()
    {

        Ui1.gameObject.SetActive(true);
        Instrucciones.gameObject.SetActive(true);
    }


    /// Activa el UI1 y desactiva el UI2
    public void UI1Go()
    {
       
            Ui2.gameObject.SetActive(false);
            Ui1.gameObject.SetActive(true);
        
    }
    /// Activa el UI2 y desactiva el UI1
    public void UI2Go()
    {
        
            Ui2.gameObject.SetActive(true);
            Ui1.gameObject.SetActive(false);
        
    }

    ///Desactiva las instrucciones
    public void DesactivarInstrucciones()
    {

        Instrucciones.gameObject.SetActive(false);
        

    }
    ///Sale de la app
    public void ExitApp()
    {
        Application.Quit();
    }
    ///Toma una screen Shot en windows me parece que en android es algo diferente en cada modelo
    public void ScreenShot()
    {
        ScreenCapture.CaptureScreenshot("Contratado.png");
    }
    ///Abre una url desde unity para ver el producto del catalogo en este caso abre mi portafolio 
    public void Producto()
    {
        Application.OpenURL("http://alberto-leal.vercel.app/");
    }

    public void SocialShare()
    {
        ///Application.OpenURL("");
    }

}
