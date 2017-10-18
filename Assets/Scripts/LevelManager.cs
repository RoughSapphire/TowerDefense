using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : MonoBehaviour {
	[SerializeField]
	private GameObject[] tilePrefabs;
	public float TileSize{
		get{return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x;}
	}
	// Use this for initialization
	void Start () {
		CreateLevel ();

		}

	
	// Update is called once per frame
	void Update () {
		
	}
	private void CreateLevel(){
		Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));
		string[] mapData=ReadLevelText();
		int mapX = mapData[0].ToCharArray().Length;
		int mapY = mapData.Length;

		for (int y = 0; y < mapY; y++) {
			char[] newTiles = mapData[y].ToCharArray();
			for (int x = 0; x < mapX; x++) {
				PlaceTile (newTiles[y].ToString(),x, y,worldStart);

			}
		}
	}
	private void PlaceTile(string TileType, int x , int y,Vector3 worldStart){
		int tileIndex = int.Parse(TileType);
		TileScript newTile = Instantiate (tilePrefabs[tileIndex]).GetComponent<TileScript>();
		newTile.transform.position = new Vector3 (worldStart.x + (TileSize * x), worldStart.y -(TileSize * y),0);
		newTile.Setup (new Point (x, y));
	}
	private string[] ReadLevelText (){
		TextAsset Binddata = Resources.Load("Level") as TextAsset;
		string data = Binddata.text.Replace (Environment.NewLine, string.Empty);
			return data.Split('-');
	}
}
