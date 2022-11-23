using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

   
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : MonoBehaviour
    {
    public UiControl Uicontrol;
    public Findtag Findtag;///El script para ver si se puede instanciar
    [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;

    UnityEvent placementUpdate;

    [SerializeField]

    //public Findtag Findtag2;
    
    GameObject visualObject;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

   
    public void Buttunpressed()  ///Instancia en las cordenadas de el visual 
    {
       if (Findtag.Instanciar4 == true)
        {
            spawnedObject = Instantiate(m_PlacedPrefab, visualObject.transform.position, Quaternion.identity);/// Comprueba si existe un minimo o un maximo para instanciar el suelo
        }
        Uicontrol.DesactivarInstrucciones();


    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }