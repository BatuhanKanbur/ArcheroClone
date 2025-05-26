using Gamecore.MobManager.Interface;
using UnityEngine;

namespace Gamecore.MobManager.Behaviours
{
    public class SpawnStrategy : ISpawnPositionStrategy
    {
        public Vector3 GetSpawnPosition(Vector2 horizontalLimits, Vector2 verticalLimits)
        {
            var horizontalPosition = Random.Range(horizontalLimits.x, horizontalLimits.y);
            var verticalPosition = Random.Range(verticalLimits.x, verticalLimits.y);
            return new Vector3(horizontalPosition, 0, verticalPosition);
        }
    }
}
