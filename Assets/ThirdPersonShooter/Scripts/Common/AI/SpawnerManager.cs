using System;
using System.Collections.Generic;

using ThirdPersonShooter.Scripts;
using ThirdPersonShooter.Scripts.Entities.Player;

using UnityEngine;

namespace ThirdPersonShooter.AI
{
	public class SpawnerManager : MonoBehaviour
	{
		[Serializable]
		public struct SpawnerGoals
		{
			public Spawner spawner;
			public int pointThreshold;
			public int pointEndThresholds;
			public bool isDefault;

			public void SetSpawnerState(bool _active) => spawner.gameObject.SetActive(_active);
		}
		
		[SerializeField] private List<SpawnerGoals> spawners;

		private PlayerEntity player;
		
		private void Awake()
		{
			player = GameManager.IsValid() ? GameManager.Instance.Player : FindObjectOfType<PlayerEntity>();
			
			foreach(SpawnerGoals spawner in spawners)
				spawner.SetSpawnerState(spawner.isDefault);

			player.onScoreUpdated += OnScoreUpdated;
		}

		private void OnDestroy()
		{
			player.onScoreUpdated -= OnScoreUpdated;
		}

		private void OnScoreUpdated(int _score)
		{
			foreach(SpawnerGoals spawner in spawners)
				spawner.SetSpawnerState(_score >= spawner.pointThreshold);
			
			//foreach(SpawnerGoals spawner in spawners)
			//	spawner.SetSpawnerState(_score >= spawner.pointEndThresholds);
		}
	}
}