using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RotateMaterial : MonoBehaviour
{
    
    public Material Piso2; //El Material de todos los pisos esto es para editarlo y que todos los objetos 3d se guarden con este material
    public Texture []Textura; //Todas las texturas de los pisos ojo no son o mismo que los sprites
    public string[] NombreDelpiso; //Nombre de cada piso
    public TMP_Text textNombredelpiso; //Texto de cada nombre del piso 
    public Sprite []SpriteButton; //He puesto los sprites por separado debido a que es mas facil poner una imagen pequeña que escribir lineas de codigo y tomar memoria 
    public Image ImagenBotton; //La imagen de la textura con la que se cambiua el piso al presionarlo 
    public Image ImagenSlider; //La imagen del slider esta tambien se cambia al cambiar de piso 
    public int Numero=0;     //valor de el numero de piso que se asignara en el array

    /////

    public Slider sliderValue;
    private float previousValue;

 
    
    void Start()
    {
        CambiarPisoTextura();///Inicia desde la textura numeroint
    }

    
    void Update()
    {
        
    }

    void Awake()
    {
        // Asigna un valor cuando el slider cambia 
        this.sliderValue.onValueChanged.AddListener(this.OnSliderChanged);

        //El valor actual del slider
        this.previousValue = this.sliderValue.value;
    }

  


    void OnSliderChanged(float value)
    {/// Funcion en la cual si cambia el slider asigna el angulo al shader de -5 a 5 esto es un Range 
       
        Piso2.SetFloat("_Angle", value);
        this.previousValue = value;
    }



    public void CambiarPisoTextura()
    { //Al presionar el boton del sprite del piso cambia el piso y tambien el nombre como la textura basicamente es un ++


        if(Numero> Textura.Length -1)
        {
            Numero = 0;
            
        }
        Piso2.SetTexture("_MainTex", Textura[Numero]);
        textNombredelpiso.text = NombreDelpiso[Numero];
        ImagenBotton.sprite= SpriteButton[Numero];
        ImagenSlider.sprite = SpriteButton[Numero];
        Numero ++;
        
    }
}
