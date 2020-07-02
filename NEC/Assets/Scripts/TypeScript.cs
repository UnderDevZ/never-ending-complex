using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// attach to UI Text component (with the full text already there)
public class TypeScript : MonoBehaviour 
{
Text txt;
public string vText;
void Awake () 
	{
txt = GetComponent<Text> ();

//txt.text = "This is a very good cool";
// TODO: add optional delay when to start

	}
public IEnumerator PlayText()
	{
foreach (char c in vText) 
		{
txt.text += c;
yield return new WaitForSeconds (0.125f);
		}
		yield return new WaitForSeconds(3f);
		txt.text = "";
		yield break;
	}
}
