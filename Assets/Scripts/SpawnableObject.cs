using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpawnableObject : MonoBehaviour
{
	public int ObjectID
	{
		get
		{
			return objectID;
		}
		set
		{
			if (objectID == int.MinValue && value != int.MinValue)
			{
				objectID = value;
			}
			else
			{
				Debug.LogError("Not allowed to change ObjectID.");
			}
		}
	}
	int objectID = int.MinValue;
}
