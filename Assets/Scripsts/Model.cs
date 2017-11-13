using UnityEngine;
using System.Collections;
//Класс, отвечающий за данные грядки: позицию, состояние и цвет отрисовки
public class Model {

	private Vector2 position;//позиция грядки
	public Vector2 getPosition(){
		return position;
	}
	public void setPosition(Vector2 position1){
		position = position1;
	}

	private int state;//состояние грядки: -1 - не выкопана, 0- обработана, 1-на ней растет цветок, 2- растение выросло
	public int getState(){
		return state;
	}
	public void setState(int state1){
		state = state1;
	}

	private Color color;//цвет отображения грядки
	public Color getColor(){
		return color;
	}
	public void setColor(Color color1){
		color = color1;
	}

	public Model(Vector2 position1, int state1, Color color1){
		state = state1;
		position = position1;
		color = color1;

	}
}