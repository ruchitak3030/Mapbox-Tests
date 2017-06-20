
namespace Mapbox.Unity.Map
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Mapbox.Map;
    using Mapbox.Unity.MeshGeneration.Interfaces;
    using Mapbox.Unity.MeshGeneration.Data;
    using Mapbox.Unity.MeshGeneration.Components;



    public class PlaceLabels : MonoBehaviour
    {

        float distance;

        [Multiline(10)]
        string buildingInformation;

        [SerializeField]
        AbstractTileProvider _tileprovider;   

        FeatureBehaviour feature;


        void Start()
        {
           
        }

      
        void Update()
        {
            UnityTile[] _tile;
            _tile = _tileprovider.GetComponentsInChildren<UnityTile>();
            foreach (UnityTile tile in _tile)
            {
                Transform poiChild = tile.gameObject.transform.GetChild(2);
                Transform[] others = poiChild.gameObject.GetComponentsInChildren<Transform>();

                foreach (Transform other in others)
                {
                    if (other)
                    {
                        distance = Vector3.Distance(other.position, transform.position);
                        if (distance < 5)
                        {
                            GameObject building = other.gameObject;
                            feature = building.GetComponent<FeatureBehaviour>();
                            buildingInformation = feature.DataString.ToString();
                            Debug.Log("Player is near the building now");
                            OnGUI();
                        }
                    }
                }
            }           
        }
        void OnGUI()
        {
            
            if (distance < 5)
                GUI.Box(new Rect(50, 50, 500, 500), buildingInformation);
        }

        
    }

}
