using UnityEngine; 
using UnityEngine.UI; 
using System.Collections; 

public class FarmerView : MonoBehaviour { 

	public float xx;

	public FarmerModel model; 
	private Animator anim; 
	public Vector3 delta;
	public bool hasArrived;
	public bool isBusy = false;

	private View currentFlowerbed;//обрабатываемая грядка
	private FarmerAction currentAction;//выполняемое в данный момент действие

	public View getCurrentFlowerbed() 
	{ 
		return currentFlowerbed; 
	}

	public void setCurrentFlowerbed(View destFlowerbed) 
	{ 
		currentFlowerbed = destFlowerbed; 
	} 
	// Use this for initialization 

	private ArrayList obs = new ArrayList();

	public void addObserver(Observer observer) //добавить слушателя
	{ 
		obs.Add(observer); 
	}

	public void removeObserver(Observer observer) //удалить слушателя
	{ 
		obs.Remove(observer); 
	} 


	void Start () { 
		model = new FarmerModel(); 
		anim = GetComponent<Animator>(); 
		hasArrived = false; 
	} 

	// Update is called once per frame 
	void Update () { 
		if (isBusy == true) 
		{ 
			return; 
		} 

		if (model.actions.Count!=0) 
		{ 
			currentAction = (FarmerAction)model.actions[0]; 
			currentFlowerbed = currentAction.Flowerbed; 
			float dt = MoveTo(new Vector2(currentAction.Flowerbed.GetComponent<RectTransform>().position.x, 
				currentAction.Flowerbed.GetComponent<RectTransform>().position.y));  
		} 
	} 

	public void makeAction() //выполнить необходимое действие
	{ 
		isBusy = true; 
		anim.SetInteger ("action", currentAction.action);
		if (currentAction.action == 0) 
		{
			StartCoroutine(respawnWait(GlobalVariables.handlingTime)); 
		} else if (currentAction.action == 1) 
		{ 
			StartCoroutine(respawnWait(GlobalVariables.growingTime)); 
		} 
		model.actions.RemoveAt(0); 
	}
	IEnumerator respawnWait(float waitTime) 
	{ 
		yield return new WaitForSeconds(waitTime); 
		isBusy = false; 
		anim.SetInteger ("action", -1);
		currentFlowerbed.Alpha = 1;
	} 

	private void notify() //оповестить слушателей
	{ 
		for(int i = 0; i < obs.Count; i++) 
		{ 
			Observer o = (Observer) obs[i]; 
			o.updateFarmerPos(); 
		} 
	} 

	public float MoveTo(Vector3 fPos){

		float dx = fPos.x - GetComponent<RectTransform> ().position.x;
		float dy = fPos.y - GetComponent<RectTransform> ().position.y+1f;

		if (Mathf.Abs (dx * dx + dy * dy) <0.001) {//если дошли до необходимой грядки
			anim.SetInteger ("action", -1);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			//передвигаем позицию якорей соответственно с изменением позиции игрока
			GetComponent<RectTransform> ().anchorMin= new Vector2( GetComponent<RectTransform> ().anchorMin.x + dx / delta.x,
				GetComponent<RectTransform> ().anchorMin.y + dy / delta.y);
			GetComponent<RectTransform> ().anchorMax= new Vector2( GetComponent<RectTransform> ().anchorMax.x + dx / delta.x,
				GetComponent<RectTransform> ().anchorMax.y + dy / delta.y);

			hasArrived = true;
			notify();

			return 0;
		} else {//иначе двигаемся дальше
			//вычисляем направление движения
			int direction = 0;

			if (dx >= 0 && dy > 0)
				direction = 1;
			
			if (dx < 0 && dy >= 0)
				direction = 2;
			
			if (dx <= 0 && dy < 0)
				direction = 3;
			
			if (dx > 0 && dy <= 0)
				direction = 4;
				
			anim.SetInteger ("action", 2);
			anim.SetInteger ("direction", direction);



			float S = Mathf.Sqrt (dx * dx + dy * dy);
			float T = S / model.speed;

			float dt = Time.deltaTime/T;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (dt*dx / T, dt*dy / T);

			GetComponent<RectTransform> ().anchorMin= new Vector2( GetComponent<RectTransform> ().anchorMin.x + dt*dx / delta.x,
				GetComponent<RectTransform> ().anchorMin.y + dt*dy / delta.y);
			GetComponent<RectTransform> ().anchorMax= new Vector2( GetComponent<RectTransform> ().anchorMax.x + dt*dx / delta.x,
				GetComponent<RectTransform> ().anchorMax.y + dt*dy / delta.y);
			return T;
		}

	}

	void OnGUI() //перерисовываем спрайт игрока при движении. Ассоциируем его с конкретной картинкой(image), чтоб можно было стабилизировать
	{ 
		Image im; 
		im = GetComponent<Image>(); 
		im.sprite = GetComponent<SpriteRenderer>().sprite; 

	} 
}