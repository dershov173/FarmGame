using UnityEngine;
using System.Collections;
using System; 

public class Garden : Observer {

	int fCount = 25;
	public View [] flowerbeds;
	public FarmerView farmer;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < fCount; i++) 
		{ 
			flowerbeds[i].addObserver(this); 
		}
		farmer.delta = transform.TransformVector(GetComponent<RectTransform>().rect.size); 
		farmer.addObserver(this); 
	}

	public override void updateData(View flowerbed, int action) 
	{ 
		farmer.model.actions.Add(new FarmerAction(action, flowerbed)); 
	}

	public override void updateFarmerPos() 
	{ 
		farmer.makeAction(); 
		farmer.getCurrentFlowerbed ().icon.gameObject.SetActive (false);
		if (farmer.getCurrentFlowerbed().model.getState() == -1) 
		{ 
			farmer.getCurrentFlowerbed().toHandle(); 
		} 
		else if (farmer.getCurrentFlowerbed().model.getState() == 0) 
		{ 
			farmer.getCurrentFlowerbed().toGrow(); 
		}
	} 

	void Update () {
		farmer.delta = transform.TransformVector(GetComponent<RectTransform> ().rect.size);//размер экрана в локальных координатах

	}
}