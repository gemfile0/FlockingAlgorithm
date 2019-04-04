using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Flock), true)]
public class FlockGizmo : Editor 
{
	#region Variable
	[SerializeField] private bool turnOn = true;

	private Flock flock;
	private Color color;
	private GUIStyle whiteTextStyle;
	#endregion

	#region Unity Method
	void OnEnable()
	{
		flock = (Flock)target;
		color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), .2f);
		// Tools.hidden = true;
		whiteTextStyle = EditorStyles.TextStyle(Color.white);
	}

	void OnDisable()
	{
		// Tools.hidden = false;
	}

	void OnSceneGUI()
	{
		Handles.BeginGUI();
		if (GUILayout.Button("Hide", GUILayout.Width(100)))
		{
			turnOn = !turnOn;
		}
		Handles.EndGUI();

		if (turnOn)
		{
			DrawBoundingArea();
			// DrawSpawningArea();
			// DrawGoalPosition();
			DrawRecognitionDistance();
		}
	}
	#endregion

	#region Custom Method
	void DrawRecognitionDistance()
	{
		foreach (FlockAgent agent in flock.Agents.Values) 
		{
			Handles.color = agent.NeighborCount > 0 ? new Color(255f, 255f, 0, .2f) : new Color(255f, 255f, 255f, .1f);
			Handles.SphereHandleCap(
				agent.GetInstanceID(), 
				agent.transform.position, 
				Quaternion.identity, 
				flock.NeighborRadius, 
				EventType.Repaint
			);

			// Draw a vector of fish
			Handles.color = Color.white;
			Quaternion rotation = agent.transform.rotation;
			Handles.ArrowHandleCap(2, agent.transform.position, rotation, agent.transform.forward.magnitude * 3f, EventType.Repaint);
			Handles.color = Color.red;
			Handles.DrawLine(agent.transform.position, 
							 agent.transform.position + agent.transform.up * 3f);
		}
	}

	// void DrawGoalPosition()
	// {
	// 	Handles.color = new Color(255f, 0f, 0f, .5f);
	// 	if (fishSpawner.GoalPosition.value != Vector3.zero) {
	// 		Handles.SphereHandleCap(1, fishSpawner.GoalPosition.value, Quaternion.identity, 4f, EventType.Repaint);

	// 	} else {
	// 		for (int i = 0; i < fishSpawner.GoalPositions.Count; i++) {
	// 			Vector3 goalPosition = fishSpawner.GoalPositions[i].value;
	// 			float timeCreated = fishSpawner.GoalPositions[i].timeCreated;
				
	// 			Handles.SphereHandleCap(1, goalPosition, Quaternion.identity, 4f, EventType.Repaint);

	// 			foreach (AbstractFishMover fishMover in fishSpawner.AllFishMovers) {
	// 				if (fishMover.Index != i) { continue; }

	// 				Handles.Label(
	// 					fishMover.transform.position, 
	// 					string.Format("{0}s, {1}m", (int)(Time.time - timeCreated), (int)Vector3.Distance(goalPosition, fishMover.transform.position)),
	// 					whiteTextStyle
	// 				);
	// 			}
	// 		}
	// 	}
	// }

	void DrawBoundingArea()
	{
		Handles.color = Color.white;
		Handles.DrawDottedLine(
			flock.transform.position, 
			flock.transform.position + Vector3.up * flock.StayinRadius, 
			5f
		);
		Handles.DrawDottedLine(
			flock.transform.position, 
			flock.transform.position + Vector3.right * flock.StayinRadius,
			5f
		);
		Handles.DrawDottedLine(
			flock.transform.position, 
			flock.transform.position + Vector3.forward * flock.StayinRadius, 
			5f
		);

		Handles.color = color;
		Handles.SphereHandleCap(
			0, 
			flock.transform.position, 
			Quaternion.identity, 
			flock.StayinRadius * 2, 
			EventType.Repaint
		);
	}

	// void DrawSpawningArea()
	// {
	// 	float StartingOffset = fishSpawner.StartingOffset;
	// 	foreach(RangeValue startingAngleRange in fishSpawner.StartingAngleRanges) {
	// 		var radius = fishSpawner.FishFlockingModel.Boundingsize + StartingOffset;
	// 		var angleVector = startingAngleRange.min.ToHorizontalVector();

	// 		Handles.color = Color.green;
	// 		Handles.DrawWireArc(
	// 			fishSpawner.transform.position,
	// 			Vector3.down,
	// 			angleVector,
	// 			(startingAngleRange.max - startingAngleRange.min),
	// 			radius
	// 		);
	// 	}
	// }
	#endregion
}