using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Models
{
    public class EnemySpawnPointMasterData
    {
        public static EnemySpawnPointMasterData Instance { get; } = new EnemySpawnPointMasterData();
        private Queue<Vector3> _spawnPoints = new Queue<Vector3>();

        public EnemySpawnPointMasterData()
        {
            _spawnPoints.Enqueue(new Vector3(-386,-150,0));
        }

        public Vector3 GetSpawnPoint()
        {
            return _spawnPoints.Dequeue();
        }
    }
}