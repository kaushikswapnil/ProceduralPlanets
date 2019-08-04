using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShapeGenerator 
{
    PlanetShapeSettings _ShapeSettings;

    public PlanetShapeGenerator(PlanetShapeSettings shapeSettings)
    {
    	_ShapeSettings = shapeSettings;
    }

    public Vector3 CalculatePointOnPlanetSurface(Vector3 pOnUnitSphere)
    {
    	return pOnUnitSphere * _ShapeSettings._PlanetRadius;
    }

    public float GetPlanetRadius()
    {
    	return _ShapeSettings._PlanetRadius;
    }
}
