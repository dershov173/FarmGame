using UnityEngine;
using System.Collections;
//Представление нашей грядки на сцене

public class View : MonoBehaviour {
	private float handlingTime = GlobalVariables.handlingTime;
	public float Alpha;
	public Icon icon;

	public Model model;//Ссылка на модель, хранящую данные о грядке
	private CanvasRenderer rend;//отрисовщик
	public Plant plant;//растение, которое будет появляться на грядке

	private bool isHandling = false;//Правда при условии, что работник в скором времени обрабтает эту грядку

	private ArrayList obs = new ArrayList();//массив слушателей

	public void addObserver(Observer observer) 
	{ 
		obs.Add(observer); 
	}

	public void removeObserver(Observer observer) 
	{ 
		obs.Remove(observer); 
	}

	public void changeCollider(){//функция динамически задает размеры и положение полигона, опряделеющего нажатие мыши

		Vector2[] pts = GetComponent<PolygonCollider2D> ().points;
		pts [0].x = 0;
		pts [0].y = GetComponent<RectTransform> ().rect.height / 2;

		pts [1].x = GetComponent<RectTransform> ().rect.width / 2;
		pts [1].y = 0;

		pts [2].x = 0;
		pts [2].y = -GetComponent<RectTransform> ().rect.height / 2;

		pts [3].x = -GetComponent<RectTransform> ().rect.width / 2;
		pts [3].y = 0;

		GetComponent<PolygonCollider2D> ().SetPath (0, pts);
	}

	void Start () {
		model = new Model(transform.position,-1,Color.white);
		rend = GetComponent<CanvasRenderer> ();
		Alpha=0.3f;
	}

	public void toHandle()//обработать грядку 
	{ 
		isHandling = true; 
		StartCoroutine(respawnWaitForHandling(handlingTime)); 
	}

	public void toGrow()//посадить растение 
	{ 
		plant.isGrowing = true; 
		plant.gameObject.SetActive(true); 
		StartCoroutine(respawnWaitForGrowing(plant.getGrowingTime())); 
		model.setState(1); 
	} 

	void Update () {

		changeCollider ();

	}

	void notify(int action)//оповестить слушателей о том, что необходимо выполнить действие action 
	{ 
		for(int i = 0; i < obs.Count; i++) 
		{ 
			Observer o = (Observer)obs[i]; 
			o.updateData(this, action); 
		} 
	}

	void OnMouseOver(){

		model.setColor (Color.grey);
	}

	void OnMouseExit() {
		model.setColor (Color.white);
	}

	void OnMouseDown(){
		if (plant.isGrowing == true || isHandling == true) {
			return;//если грядка уже ожидает работника, то ничего с ней не делаем
		}
		else{//иначе, взависимости от состояния, прописываем директивы работникку
			if (model.getState() == -1) 
			{ 
				notify(0); 
				isHandling = true;
				icon.gameObject.SetActive (true);

			}
			else if (model.getState() == 0) 
			{
				notify(1);
				plant.isGrowing = true;
				icon.gameObject.SetActive (true);
			} 
			else if (model.getState() == 1) 
			{
				plant.gameObject.SetActive (false);
				model.setState (-1);
				Alpha = 0.3f;
			}
		}
	}
	//Енумераторы опряделяют время обработки и поливания грядки
	IEnumerator respawnWaitForHandling(float waitTime){
		yield return new WaitForSeconds (waitTime); 
		model.setState(0); 
		isHandling = false;
	}

	IEnumerator respawnWaitForGrowing(float waitTime) 
	{ 
		yield return new WaitForSeconds(waitTime); 
		model.setState(1); 
		plant.isGrowing = false; 
	} 

	void OnGUI()
	{
		rend.SetColor (model.getColor ());
		rend.SetAlpha (Alpha);
	}
}