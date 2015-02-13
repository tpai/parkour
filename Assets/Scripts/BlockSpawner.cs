﻿using UnityEngine;
using System.Collections;

public class BlockSpawner : MonoBehaviour {

	Timer timer;
	public int spawnTime = 0;
	public GameObject[] blocks;

	void Start () {
		timer = GameObject.Find ("Timer").GetComponent<Timer> ();
		SpawnBlocks ();
		InvokeRepeating("CheckBlocks", 0, 1);
	}

	void CheckBlocks () {
		Vector3 playerPos = GameObject.Find ("Player").transform.position;
		if(playerPos.x > 80 * spawnTime) {
			SpawnBlocks ();
		}
	}

	void SpawnBlocks () {
		int range;
		if(timer.nowTime >= 100f)range = blocks.Length;
		else if(timer.nowTime >= 50f)range = blocks.Length - 3;
		else range = blocks.Length - 6;


		int from = spawnTime * 10 + 1;
		int end = from + 10;
		for(int i=from;i<end;i++) {
			GameObject obj = (GameObject)Instantiate (
				blocks[Random.Range (0, range)], 
				new Vector3(10*i, 0, 0), 
				Quaternion.identity
			);
			obj.transform.parent = transform;
			obj.name = "Block"+i;
		}
		spawnTime ++;

		if(spawnTime > 2) {
			from = (spawnTime - 3) * 10 + 1;
			end = from + 10;
			for(int i=from;i<end;i++) {
				Destroy (transform.Find ("Block"+i).gameObject);
			}
		}
	}
}
