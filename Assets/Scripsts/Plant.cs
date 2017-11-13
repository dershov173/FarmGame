using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Plant : MonoBehaviour {
	private CanvasRenderer rend;
	public bool isGrowing;//Правда, если на грядке начинает расти цветок
	private float growingTime = GlobalVariables.growingTime;//время цветения

	public float getGrowingTime(){
		return growingTime;
	}

	void Start () {//поначалу цветок невидим
		gameObject.SetActive(false);
	}
		
	void OnGUI(){

		Image im;
		im = GetComponent<Image>();
		im.sprite = GetComponent<SpriteRenderer> ().sprite;
	}
}