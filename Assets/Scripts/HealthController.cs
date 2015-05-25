using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	
	public float scale = 0f;

	public RectTransform rectTransform;

	void Update() {
		rectTransform.localPosition = new Vector3 ((1 - scale) * (-94f), rectTransform.localPosition.y, rectTransform.localPosition.z);

	}

	public void getHit(string enemyTag){
		print (enemyTag);
		if (scale > 0) {
			switch (enemyTag) {

			case "square":
				scale -= 0.1f;
				break;
			case "circle":
				scale -= 0.2f;
				break;

			}
		}

	}

}

