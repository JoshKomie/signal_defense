﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSite : MonoBehaviour {
	public GameObject TurretPrefab, BarracksPrefab, FlameTurretPrefab, SuperBarracksPrefab;
	private GameObject player;
	private PlayerInput playerInput;
	private ZoneGenerator zoneGenerator;
	public string occupant = "empty";
	private PlayerResources playerResources;
	private GameObject structure;
	private int turretCost = 3;
	private int barracksCost = 5;
	private int flameTurretCost = 20;
	private int superBarracksCost = 15;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		playerInput = player.GetComponent<PlayerInput>();
		playerResources = player.GetComponent<PlayerResources>();
		zoneGenerator = GameObject.Find("World").GetComponent<ZoneGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool BuildTurret() {
		if (playerResources.GetCurrency() > turretCost) {
			structure = Instantiate(TurretPrefab, transform.position, Quaternion.identity) as GameObject;
			occupant = "turret";
			playerResources.UseCurrency(turretCost);
			return true;
		} else {
			return false;
		}
	}

	public bool buildStructure(string type, GameObject prefab, int cost) {
		if (playerResources.GetCurrency() >= cost) {
			structure = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
			occupant = type;
			playerResources.UseCurrency(cost);
			return true;
		} else {
			return false;
		}
	}

	public void OnMouseDown() {
		Debug.Log("build site mouse down!");
		if ( playerInput.selectBuildSiteModeEnabled() && GetComponent<SpriteRenderer>().enabled) {
			string type = playerInput.structureType;
			switch (type) {
				case "turret":
					buildStructure("turret", TurretPrefab, turretCost);
					break;
				case "barracks":
					buildStructure("barracks", BarracksPrefab, barracksCost);
					break;
				case "flame_turret":
					buildStructure("flame_turret", FlameTurretPrefab, flameTurretCost);
					break;
				case "super_barracks":
					buildStructure("super_barracks", SuperBarracksPrefab, superBarracksCost);
					break;
			}
			// if (buildStructure("turret", TurretPrefab, turretCost)) {
			// 	// playerInput.DisableSelectHexMode();
			// }
		}
		playerInput.DisableSelectBuildSiteMode();
	}

	public void DestroyStructure()
    {
        Destroy(structure);
    }
}
