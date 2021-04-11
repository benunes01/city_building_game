using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour
{
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJob;

    public TextMeshProUGUI statsText;

    public List<Building> buildings = new List<Building>();

    public static City instance;

    void Awake() {
        instance = this;
    }

    public void OnPlaceBuilding(Building building) {
        money -= building.preset.cost;

        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;
        buildings.Add(building);

        UpdateStatText();
    }

    public void OnRemoveBuilding(Building building) {
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);

        Destroy(building.gameObject);

        UpdateStatText();
    }

    void UpdateStatText() {

    }
}
