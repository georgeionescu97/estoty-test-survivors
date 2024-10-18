using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Units
{
    public class UnitsContainer : IUnitsContainer
    {
        private readonly List<Unit> _units = new();

        public void RegisterUnit(Unit unit)
        {
            if (_units.Contains(unit))
            {
                Debug.LogError($"Unit already registered: {unit.GetInstanceID()}");
                return;
            }

            _units.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            if (!_units.Contains(unit))
            {
                Debug.LogError($"Unit is not registered. InstanceID: {unit.GetInstanceID()}. Name: {unit.name}");
                return;
            }

            _units.Remove(unit);  
        }

        public void RemoveAllUnits()
        {
            _units.Clear();
        }

        public void RemoveUnits<T>() where T : Unit
        {
            _units.RemoveAll(u => u is T);
        }

        public int GetUnitCount<T>() where T : Unit
        {
            return GetUnits<T>().Count();
        }

        public IEnumerable<Unit> GetUnits<T>() where T : Unit
        {
            return GetUnits(typeof(T));
        }

        public IEnumerable<Unit> GetUnits(Type type)
        {
            return _units.Where(u => u != null &&
                                     (u.GetType() == type || u.GetType().IsSubclassOf(type)))
                          .ToList();
        }
    }
}