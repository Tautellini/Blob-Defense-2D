﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerActionMenu : MonoBehaviour
{
    public GameObject towerActionButtonPrefab;
    private List<GameObject> buttons;

    void Start() {
        buttons = new List<GameObject>();
    }

    // TODO: This Method has to be wired up to an Event
    public void ShowTowerActionMenu(AttributeManager attributeManager, Tower tower) 
    {
        /*var towerActions = tower.TowerActions;
        while(towerActions.MoveNext()) 
        {
            var towerActionButtonObject = Instantiate(towerActionButtonPrefab, transform);
            var towerActionButton = towerActionButtonObject.GetComponent<TowerActionButton>();

            towerActionButton.TowerAction = towerActions.Current;
            towerActionButton.tower = tower;
            towerActionButton.towerActionMenu = this;
            
            buttons.Add(towerActionButtonObject);
        }*/
        foreach(var towerAction in tower.towerActions) {
            var towerActionButtonObject = Instantiate(towerActionButtonPrefab, transform);
            var towerActionButton = towerActionButtonObject.GetComponent<TowerActionButton>();

            towerActionButton.TowerAction = towerAction;
            towerActionButton.tower = tower;
            towerActionButton.towerActionMenu = this;
            towerActionButton.attributeManager = attributeManager;
            
            buttons.Add(towerActionButtonObject);
        }
    }

    // TODO: This Method has to be wired up to an Event
    public void HideTowerActionMenu() {
        foreach(var button in buttons) {
            Destroy(button);
        }
        buttons.Clear();
    }
}
