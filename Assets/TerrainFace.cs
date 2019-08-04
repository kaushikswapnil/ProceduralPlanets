using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh _Mesh;
    int _Resolution;
    Vector3 _Sight;
    Vector3 _Horizon;
    Vector3 _Banking;

    PlanetShapeGenerator _ShapeGenerator;

    public TerrainFace(PlanetShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
    	_ShapeGenerator = shapeGenerator;

    	this._Mesh = mesh;
    	this._Resolution = resolution;

    	_Sight = new Vector3(localUp.y, localUp.z, localUp.x);
    	_Horizon = Vector3.Cross(localUp, _Sight);
    	_Banking = localUp;
    }

    public void ConstructMesh()
    {
    	Vector3[] vertices = new Vector3[this._Resolution*this._Resolution];
    	int[] triangleIndices = new int[(this._Resolution - 1)* (this._Resolution - 1) * 6];

    	int triIndex = 0;
    	//int verticeIndex = 0;

    	int maxXYIterValue = this._Resolution - 1;

    	for (int iterX = 0; iterX < (this._Resolution); ++iterX)
    	{
    		for (int iterY = 0; iterY < (this._Resolution); ++iterY)
    		{
    			Vector2 uv = (new Vector2(iterX, iterY))/maxXYIterValue;
    			Vector3 pOnUnitCube = _Banking + ((uv.x - 0.5f) * 2 * _Sight) + ((uv.y - 0.5f) * 2 * _Horizon);
    			Vector3 pOnUnitSphere = pOnUnitCube.normalized;

    			int verticeIndex = iterX + (iterY * _Resolution);
    			vertices[verticeIndex] = _ShapeGenerator.CalculatePointOnPlanetSurface(pOnUnitSphere);

    			if (iterX != maxXYIterValue && iterY != maxXYIterValue)
    			{
    				triangleIndices[triIndex] = verticeIndex;
    				triangleIndices[triIndex + 1] = verticeIndex + _Resolution + 1;
    				triangleIndices[triIndex + 2] = verticeIndex + _Resolution;

    				triangleIndices[triIndex + 3] = verticeIndex;
    				triangleIndices[triIndex + 4] = verticeIndex + 1;
    				triangleIndices[triIndex + 5] = verticeIndex + _Resolution + 1;

    				triIndex += 6;
    			}

    			//++verticeIndex;
    		}
    	}

    	_Mesh.Clear();
    	_Mesh.vertices = vertices;
    	_Mesh.triangles = triangleIndices;
    	_Mesh.RecalculateNormals();
    }
}

