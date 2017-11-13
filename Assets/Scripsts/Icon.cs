using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour {
	//класс определяет картинку, которая показывает состояние грядки, находящейся в ожидании работника

	private CanvasRenderer rend;
		

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}

}
