using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Findtag : MonoBehaviour
{

    //public Start4 Satr4UI2; /// DEsactiva todo lo de escaneo en AR y lo vuelve a activar 
    public UiControl uicontrol;  //Scirpt de control ui para activar y desactivar canvas
    public ARPlaneManager planeManagerAR;
    public ARPointCloudManager pointCloudManager;

    List<ARPlane> allPlanes = new List<ARPlane>();/// Planos de AR en una lista para despues desctivarlos "no son los mesh del suelo" 
   



    public Material material = null; // material del suelo al crear un nuevo mesh
    public GameObject[] Meshes; // Array de pisos si llega existir alguno que se fue 
    public GameObject[] Level; /// Aqui estara todo lo que tenga el tag "cube"
    public GameObject Visual; /// El apuntador Visual 
    public LineRenderer line; // Linea que junta los objetos con el tag "cube"
    public LineRenderer line2; // Linea falsa que solo juntara el ultimo objecto con el tag cube y el gameobject visual de arriba
    public bool Instanciar4 = true;
    public Button BotonDeIntanciado; //Botton + ui
    public Button BotonDeResta;//Botton de - ui
    public Button BotondeFinalizarseleccion;//Botton de pasar a ui2



    void Start()
    {
        BotondeFinalizarseleccion.interactable = false;//desactiva al inicio el boton de ui2
        Find(); //Primero se llama para actualizar el GameObject
        
    }

   
    void Update()
    {
       
        if (Level.Length > 0 && uicontrol.Ui2.activeSelf == false) //Se actualiza la linea render cada vez por llamada
        {

            LineRenderer();
        }

    }


    private void SetShaderValue()
    {
      
        //var t : float = Mathf.PingPong(Time.time, duration) / duration;
        
       // _theMaterial.SetColor("Color", Color.red);
    }


    public void Find()
    {
        Level = GameObject.FindGameObjectsWithTag("Cube"); ///Adjunta en array todos los objectos que se tienen el tag cube;
        //LineRenderer();
        // Array.Reverse(Level);
        Debug.Log(Level.Length + " Level.Length "); 
        Visual.gameObject.SetActive(true); //El apuntador visual se activa por si se llegara a quedar apagado en la escena;
       
    }


    public void LineRenderer()
    {
        Level = GameObject.FindGameObjectsWithTag("Cube"); ///Adjunta en array todos los objectos que se tienen el tag cube para una doble comprobacion
        line.positionCount = Level.Length;
        line2.positionCount = 2;

        BotondeFinalizarseleccion.interactable = true;//Activa el boto de pasar ui2
        BotonDeResta.interactable = true; //Activa el boton de -
        BotonDeIntanciado.interactable = true;///Activa el boton para poder instanciar
        Visual.gameObject.SetActive(true); //Podria parecer que arriba se activo esto es por seguridad debido a que una funcion al regresar lo podria apagar 
        Instanciar4 = true; ///inica en true por si la persona regresa a editar algo 
        if (Level.Length > 1)  //Se activa  el line render si tiene mas de 1 objecto con el tag "cube"
        {
            line.gameObject.SetActive(true);
        }
        if (Level.Length > 0)  //Linea falsa del visual al ultimo objecto con el tag "cube"
        {
            line2.gameObject.SetActive(true);
            
        }

        

        for (int i = 0; i< Level.Length; i++) //Si bien esta en update este llama rapidamente cada tag para hacer un camino desde el punto 0del objeto tageado "cube" hasta el ultimo
        {
            line.SetPosition(i, Level[i].transform.position);
            line2.SetPosition(0, Level[i].transform.position);
            line2.SetPosition(1, Visual.transform.position);
           
            if (Level.Length > 1 && Level.Length<3) 
            {
                line2.SetPosition(0, Level[i].transform.position);
                line2.SetPosition(1, Visual.transform.position);
               


                float distance = Vector3.Distance(Level[0].transform.position, Level[1].transform.position);
                //Debug.Log(distance+ " Distancia ");
            }

            /// Estos dos ultimos if verifican si existen mas objetos para seguir instanciando 
            /// 
            if (Level.Length < 3)
            {
                BotondeFinalizarseleccion.interactable = false;


            }

            if ( Level.Length > 3)
            {
               
                line2.gameObject.SetActive(false);
                Instanciar4 = false;
                BotonDeIntanciado.interactable = false;//Desactiva el boton si exede el maximo 

            }
            

            if (Level.Length <= 0 )
            {
                line.gameObject.SetActive(false);
                line2.gameObject.SetActive(false);
            }

            


           
        }

        //Array.Reverse(Level);
        //Level.Sort();
        //line.loop = true;
        


    }


    


    public void Modificarmesh()  //Aqui se empieza a construir el mesh con las posiciones de los objetos "cube" en el array cube
    {
        if (Level.Length>2)
        {    // Se desactivan todo lo necesario para editar y pasar a la siguiente seccion 
            
            uicontrol.UI2Go();
            line2.gameObject.SetActive(false);
            Visual.gameObject.SetActive(false);
            line.gameObject.SetActive(false);


            int Cube = 0;
            int Cube1 = 1;
            int Cube2 = 2;
            int Cube3 = 3;
            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];
            vertices[0] = new Vector3(Level[Cube].gameObject.transform.position.x, Level[Cube].gameObject.transform.position.y, Level[Cube].gameObject.transform.position.z);
            vertices[1] = new Vector3(Level[Cube1].gameObject.transform.position.x, Level[Cube1].gameObject.transform.position.y, Level[Cube1].gameObject.transform.position.z);

            //Crea la figura de los vertices a partir de las posiciones de cada objecto en el array cube con el tag "cube"
            if (Level.Length == 3)
            {
                ///Cuando se crea un triangulo
                vertices[2] = new Vector3(Level[Cube2].gameObject.transform.position.x, Level[Cube2].gameObject.transform.position.y, Level[Cube2].gameObject.transform.position.z);
            }


            if (Level.Length == 4)
            {
                ////Cuando se crea un cuadrado
                vertices[3] = new Vector3(Level[Cube2].gameObject.transform.position.x, Level[Cube2].gameObject.transform.position.y, Level[Cube2].gameObject.transform.position.z);
                vertices[2] = new Vector3(Level[Cube3].gameObject.transform.position.x, Level[Cube3].gameObject.transform.position.y, Level[Cube3].gameObject.transform.position.z);
            }





            // Crea la triangulacion d elos uv
            Debug.Log("se instancio");

            uv[0] = new Vector2(0, 1);
            uv[1] = new Vector2(1, 1);
            uv[2] = new Vector2(0, 0);
            uv[3] = new Vector2(1, 0);



            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 2;////
            triangles[4] = 1;
            triangles[5] = 3;

            ///Crea el mesh apartitr de la triangulacion

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            // mesh.RecalculateNormals();
            //  mesh.RecalculateBounds();
            mesh.uv = uv;
            mesh.triangles = mesh.triangles.Reverse().ToArray();
            mesh.triangles = triangles;
            GameObject Meshii = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            Meshii.tag = "Mesh"; //Muchas veces por la realidad aumentada es dificil saber si el mesh se intancio debido al nivel de terreno esto ayudara despues.
            Meshii.transform.localScale = new Vector3(1, 1, 1);

            Meshii.GetComponent<MeshFilter>().mesh = mesh;

            Meshii.GetComponent<MeshRenderer>().material = material;

            DisablePlanes();// Desactiva las particulas y el escaneo d enueblos planos en ARPlane
        }
        




    }


    public void DestroyLast()
    {
        Level = GameObject.FindGameObjectsWithTag("Cube");///Busca el tag "cube" Para hacer un for y borrar el ultimo objeto 
        if (Level.Length>=1)
        {
            int lastobj = Level.Length;
            lastobj = lastobj - 1;
            Debug.Log("last obj " + lastobj);
            Destroy(Level[lastobj]);
            //Desactiva las lineas de render por si es el ultimo objecto 
            line.gameObject.SetActive(false);
            line2.gameObject.SetActive(false);
            
        }
        else
        {
            //Desactiva las lineas de render por si insiste la persona 
            line.gameObject.SetActive(false);
            line2.gameObject.SetActive(false);
            BotonDeResta.interactable = false;
            Debug.Log("Nada en la lista");
        }
        
    }


   public void DestroyMesh()
    {
        // Si no se llego a instanciar bien por el desnivel del terreno o objetos buca todos y los elimina
        Meshes = GameObject.FindGameObjectsWithTag("Mesh");
        for (int i = 0; i < Meshes.Length; i++)
        {
            
            Destroy(Meshes[i]);
           
        }
        ActivePlanes();// Vulve activar los planos y el escanero de Ar
    }

    public void DisablePlanes()
    {    ///Desactiva los planos de ARPlane aun no por completo debdio a lo dificl que e sque no colisionene los mesh 
        foreach (ARPlane plane in allPlanes)
        {
            plane.gameObject.SetActive(false);
        }
        foreach (var pointCloud in pointCloudManager.trackables)
        {
            pointCloud.gameObject.SetActive(false);
        }

        planeManagerAR.enabled = false;
        
    }


    public void ActivePlanes()
    {   ///Activa los planos de ARPlane aun no por completo debdio a lo dificl que e sque no colisionene los mesh 
        foreach (ARPlane plane in allPlanes)
        {
            plane.gameObject.SetActive(true);
        }
        foreach (var pointCloud in pointCloudManager.trackables)
        {
            pointCloud.gameObject.SetActive(true);
        }

        planeManagerAR.enabled = true;
      
    }




}
