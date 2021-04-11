using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    private BuildingPreset curBuildingPreset;

    private float indicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    public GameObject placementIndicator;
    public GameObject bulldozeIndicator;

    public GroundGrid groundGrid;

    void Start() {
        CancelBuildingPlacement();
    }
    
    void Update () {
    // cancel building placement
    if(Input.GetKeyDown(KeyCode.Escape)){
        CancelBuildingPlacement();
    }
        
    // called every 0.05 seconds
    if(Time.time - lastUpdateTime > indicatorUpdateRate)
    {
        lastUpdateTime = Time.time;
 
        // get the currently selected tile position
        curIndicatorPos = Selector.instance.GetCurTilePosition();
 
        // move the placement indicator or bulldoze indicator to the selected tile
        if(currentlyPlacing){
            placementIndicator.transform.position = curIndicatorPos;
        }
            
        else if(currentlyBulldozering){
            bulldozeIndicator.transform.position = curIndicatorPos;
        }
            
    }

        if(Input.GetMouseButtonDown(0) && currentlyPlacing) {
            PlaceBuilding();
        } else if(Input.GetMouseButtonDown(0) && currentlyBulldozering) {
            Bulldoze();
    }

    // SetTilingGrid();
    }

    public void SetTilingGrid() {
        if(currentlyPlacing || currentlyBulldozering) {
            groundGrid.GetComponent<Renderer>().material.mainTextureScale = new Vector2(50, 50);
        }
        else {
            groundGrid.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0, 0);
        }
    }

    public bool GetCurrentlyPlacing() {
        return currentlyPlacing;
    }

    public void BeginNewBuildingPlacement(BuildingPreset preset) {
        // check money
        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementIndicator.SetActive(true);
        placementIndicator.transform.position = new Vector3(0, -99, 0);
    }

    void CancelBuildingPlacement() {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);

        currentlyBulldozering = false;
        bulldozeIndicator.SetActive(false);
    }

    public void ToggleBulldoze() {
        currentlyBulldozering = !currentlyBulldozering;
        bulldozeIndicator.SetActive(currentlyBulldozering);
        bulldozeIndicator.transform.position = new Vector3(0, -99, 0);
    }

    // places down the currently selected building
    void PlaceBuilding ()
    {
    GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
    City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());


    CancelBuildingPlacement();
    }

    void Bulldoze() {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == curIndicatorPos);

        if(buildingToDestroy != null) {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }
}
