using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class NewBehaviourScript : MonoBehaviour {
	string lastMessage;
	public GameObject[] sphere;
	public GameObject player1, player2;
	public float foo;
	// Use this for initialization
	void Start () {
		OSCHandler.Instance.Init();
		lastMessage = "";
		Physics.IgnoreLayerCollision(8,9);
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.R)) Application.LoadLevel(0);
		OSCHandler.Instance.UpdateLogs();
		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
		servers = OSCHandler.Instance.Servers;
		ServerLog s = new ServerLog();
		if(servers.ContainsKey("HarryPC")){
			s = servers["HarryPC"];
			if(s.log.Count>0){
				if(lastMessage != s.log[s.log.Count-1]){
					lastMessage = s.log[s.log.Count-1];
					foreach(GameObject g in (GameObject.FindGameObjectsWithTag("Flashing Floor"))){
						g.GetComponent<Renderer>().enabled = !g.GetComponent<Renderer>().enabled;
						g.GetComponent<Collider>().enabled = !g.GetComponent<Collider>().enabled;
					}
					foreach(GameObject g in (GameObject.FindGameObjectsWithTag("Flashing Spike"))){
						g.GetComponent<Renderer>().enabled = !g.GetComponent<Renderer>().enabled;
						g.GetComponent<Collider>().enabled = !g.GetComponent<Collider>().enabled;
					}
				}
			}
		}
		List<float> list = new List<float>();
		//list.Add(player1.transform.position.x/22f);
		//list.Add(player2.transform.position.x/22f);
		list.Add(player1.GetComponent<Rigidbody>().velocity.x/foo);
		list.Add(player2.GetComponent<Rigidbody>().velocity.x/foo);
		OSCHandler.Instance.SendMessageToClient("OSCPan", "float-list", list);
	}
}
