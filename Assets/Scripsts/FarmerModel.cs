using UnityEngine; 
using System.Collections; 

public class FarmerModel { 
	//Класс хранит данные о работнике
	public float speed = 2f;//скорость передвижения работника по сцене
	public ArrayList actions; //коллекция необходимых действий 
	private Vector2 position; //позиция работника
	public Vector2 getPosition() 
	{ 
		return position; 
	} 

	public void setPosition(Vector2 pos) 
	{ 
		position = pos; 
	} 

	private int direction; //направление движения
	public int getDirection() 
	{ 
		return direction; 
	} 

	public void setDirection(int dir) 
	{ 
		direction = dir; 
	} 

	private int state; //состояние
	public int getState() 
	{ 
		return state; 
	} 

	public void setState(int st) 
	{ 
		state = st; 
	} 

	public FarmerModel() 
	{ 
		state = 0; 
		direction = -1; 
		position = new Vector2(0, 0); 
		actions = new ArrayList(); 
	} 
} 

public class FarmerAction 
{ 
	//Данный класс определяет директиву работнику: грядку, необходимую для 
	//обработки и действие, которое нужно произвести над этой грядкой
	public int action; 
	public View Flowerbed; 

	public FarmerAction(int act, View fPos) 
	{ 
		action = act; 
		Flowerbed = fPos; 
	} 
}