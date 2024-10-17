using UnityEngine;

namespace Units
{
    public interface IPlayerAttack
    {
        Vector2 MovementDirection();
        Vector2 TargetPosition { get; }
    }
}
