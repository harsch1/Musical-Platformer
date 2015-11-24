using UnityEngine;
using System.Collections;

public class Player2Movement : MonoBehaviour {
	private int _direction;
	public int _playerState;
	private Rigidbody _body;
	private Vector3 _spawnPoint;
	public CharacterRespawn respawner;
	// Use this for initialization
	void Start () {
		_direction = 0;
		_playerState = 4;
		_body = gameObject.GetComponent<Rigidbody>();
		_spawnPoint = gameObject.transform.position;
	}
	public void Spawn() {
		gameObject.transform.position = _spawnPoint;
		_body.velocity = Vector3.zero;
		_playerState = 4;
		OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 20f);
		_direction = 0;
	}
	void Update(){
		if(Input.GetAxis("Horizontal")>0.5f){
			_direction = 1;
		}
		else if(Input.GetAxis("Horizontal")<-0.5f){
			_direction = -1;
		}
		else {
			_direction = 0;
		}
		if(_playerState == 0 && Input.GetButtonDown("Fire1")){
			_playerState = 1;
			_body.AddForce(new Vector3(0f,472f,0f));
			OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 21f);
		}
		if(_playerState == 0 && Input.GetButtonDown("Fire2")){
			_playerState = 2;
			_body.AddForce(new Vector3(0f,570f,0f));
			OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 22f);
		}
		if(_playerState == 0 && Input.GetButtonDown("Fire3")){
			_playerState = 3;
			_body.AddForce(new Vector3(0f,650f,0f));
			OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 23f);
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		_body.velocity = new Vector3(_direction * 5f, _body.velocity.y, 0f);
		if(_playerState == 0 && (_body.velocity.y < -0.05f || _body.velocity.y > 0.05f)){
			_playerState = 4;
			OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 24f);
		}
		if(_playerState == 4 && (_body.velocity.y > -0.05f && _body.velocity.y < 0.05f)){
			_playerState = 0;
			OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 20f);
		}
		
	}
	void OnCollisionEnter(Collision collision) {
		if(_playerState!= 0 && collision.gameObject.tag.Contains("Floor")){
			foreach (ContactPoint contact in collision.contacts) {
				if(contact.normal.y == 1.0f){
					_playerState = 0;
					OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 20f);
					break;
				}
			}
		}
		if(collision.gameObject.tag.Contains("Spike")){
			OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 20f);
			OSCHandler.Instance.SendMessageToClient("OSCHost", "float", 25f);
			respawner.Respawn(gameObject, Time.time+1f);
			gameObject.SetActive(false);
		}
	}
}
