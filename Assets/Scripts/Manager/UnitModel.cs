using UnityEngine;

namespace MVC
{
    public class UnitModel
    {
        public readonly int MaxHealth;
        public int Health { get; private set; }
        public bool IsAlive => Health > 0;


        public UnitModel(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void Heal(int amount)
        {
            Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        }

        public void Injure(int amount)
        {
            Health = Mathf.Clamp(Health - amount, 0, MaxHealth);
        }
    }
}
