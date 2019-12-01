﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class DebugTowerActionExecutor : MonoBehaviour
{
    private Tower tower;
    private AttributeManager attributeManager;

    void Start() 
    {
        tower = GetComponent<Tower>();
    }

    void Update() 
    {
        if(Input.GetButtonDown("Jump")) 
        {
            /*IEnumerator<TowerAction> towerActions = tower.towerActions;
            if(towerActions.MoveNext()) 
            {
                tower.DoAction(attributeManager, towerActions.Current);
            }*/
            if(tower.towerActions.Length > 0) {
                tower.DoAction(attributeManager, tower.towerActions[0]);
            }
        }
    }
}
