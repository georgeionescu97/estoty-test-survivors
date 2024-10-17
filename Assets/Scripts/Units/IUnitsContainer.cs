using System;
using System.Collections.Generic;
using Units;

public interface IUnitsContainer
{
    void RegisterUnit(Unit unit);
    void RemoveUnit(Unit unit);
    IEnumerable<Unit> GetUnits<T>() where T : Unit;
    IEnumerable<Unit> GetUnits(Type type);
}