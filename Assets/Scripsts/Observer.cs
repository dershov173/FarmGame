using UnityEngine;
using System.Collections;

public abstract class Observer :MonoBehaviour { 
	//Абстрактный класс слушателей 
	public abstract void updateData(View flowerbed, int action);//Вызывается при необходимости произвести действия с грядкой
	//На вход подаются указатель на грядку и необходимое действие

	public abstract void updateFarmerPos();//Вызывается в момент, когда работник дошел до нужной грядки. 
	//Предписывает работнику выполнить необходимые действия 
}

public class GlobalVariables{//класс содержит время обработки грядки и поливания цветов

	public static  float handlingTime = 10.0f;

	public static float growingTime = 60.0f;
}