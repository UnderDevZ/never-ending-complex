using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityTriggerScript : MonoBehaviour
{


	public string mode; //The mode in which the player will get their sanity depleted.

	//mode = "onetime" if you want the sanity to deplete smoothly once every time the trigger is called.

	//mode = "continuous" if you want the sanity to deplete continuously as long as the player stays in the trigger.

	public float sanityAdjustment; //The amount of sanity that you want the player to lose or gain.

	//Use positive numbers to make sanity increase.

	//Use negative numbers to make sanity decrease.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
