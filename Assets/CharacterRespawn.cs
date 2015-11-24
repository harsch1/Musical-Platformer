using UnityEngine;
using System.Collections;

public class CharacterRespawn : MonoBehaviour {
	private float _spawnAlarm;
	private GameObject _toSpawnp1;
	private GameObject _toSpawnp2;
	private bool _respawn;
	// Use this for initialization
	void Start(){
		_spawnAlarm = 0f;
		_toSpawnp1 = null;
		_toSpawnp2 = null;
		_respawn = false;
	}
	public void Respawn(GameObject g, float Time){
		if(g.GetComponent<Player1Movement>()!=null){
			_toSpawnp1 = g ;
		}
		else{
			_toSpawnp2 = g;
		}
        _spawnAlarm = Time;
		_respawn = true;
	}
	void Update(){
		if(_respawn && Time.time >= _spawnAlarm){
			if(_toSpawnp1!=null){
				_toSpawnp1.SetActive(true);
				_toSpawnp1.GetComponent<Player1Movement>().Spawn();
				_toSpawnp1 = null;
			}
			if(_toSpawnp2!=null){
				_toSpawnp2.SetActive(true);
				_toSpawnp2.GetComponent<Player2Movement>().Spawn();
				_toSpawnp2 = null;
            }
			if(_toSpawnp1 == null && _toSpawnp2 == null){
				_respawn = false;
			}
		}
	}
}
