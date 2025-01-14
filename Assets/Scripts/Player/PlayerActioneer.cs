﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActioneer : MonoBehaviour
{
    [SerializeField] Tile_Targeting targeter = null;
    [SerializeField] Player_Mining mining = null;
    [SerializeField] PlayerTowerAction playerTowerAction = null;
    [SerializeField] PlayerBuildAction playerBuildAction = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 targetLoc = targeter.gettargetLoc() + new Vector3(0.5f, 0.5f, 0);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(targetLoc, 0.3f);
            Action(colliders, targetLoc);
        }
    }
    string Action(Collider2D[] colliders, Vector3 targetLoc)
    {
        string result = "";
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.name == "Tilemap_noPlacement")
            {
                return "NoActions";
            }
            else if (colliders[i].gameObject.tag == "Resource")
            {
                if (colliders[i].gameObject.GetComponent<Resource>().type == "Stone") {
                    if (gameObject.GetComponent<AttributeManager>().energy.value >= 30) {
                        mining.Mine(colliders[i].gameObject);
                    }
                } else if (colliders[i].gameObject.GetComponent<Resource>().type == "Tree") {
                    if (gameObject.GetComponent<AttributeManager>().energy.value >= 20) {
                        mining.Mine(colliders[i].gameObject);
                    }
                }
                result += "Mining";
            }
            else if (colliders[i].gameObject.tag == "Tower")
            {
                playerTowerAction.Actioning(colliders[i].gameObject);
                result += "TowerAction";
            }
        }
        if (result == "")
        {
            playerBuildAction.Actioning(targetLoc);
            result += "Build";
        }
        return result;
    }
}
