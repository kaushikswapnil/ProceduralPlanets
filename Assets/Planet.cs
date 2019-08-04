using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	[SerializeField, HideInInspector]
    MeshFilter[] _MeshFilters;
    TerrainFace[] _TerrainFaces;

    public int _TerrainFaceResolution = 10;

    public PlanetShapeSettings _ShapeSettings;
    public PlanetColorSettings _ColorSettings;

    [HideInInspector]
    public bool _ShapeSettingsEditorFoldout;
    [HideInInspector]
    public bool _ColorSettingsEditorFoldout;


    PlanetShapeGenerator _ShapeGenerator;

    void Initialize()
    {
    	if (_ShapeGenerator == null)
    	{
    		_ShapeGenerator = new PlanetShapeGenerator(_ShapeSettings);
    	}

    	if (_MeshFilters == null || _MeshFilters.Length == 0)
    	{
    		_MeshFilters = new MeshFilter[6];
    	}    	

    	_TerrainFaces = new TerrainFace[6];

    	Vector3[] terrainFaceDirections = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

    	for (int iter = 0; iter < 6; ++iter)
    	{
    		if (_MeshFilters[iter] == null)
    		{
    			GameObject meshObj = new GameObject("mesh");
	    		meshObj.transform.parent = this.transform;

	    		meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
	    		_MeshFilters[iter] = meshObj.AddComponent<MeshFilter>();
	    		_MeshFilters[iter].sharedMesh = new Mesh();
    		}    		

    		_TerrainFaces[iter] = new TerrainFace(_ShapeGenerator, _MeshFilters[iter].sharedMesh, _TerrainFaceResolution, terrainFaceDirections[iter]);
    	}
    }

    public void GeneratePlanet()
    {
    	Initialize();
    	GenerateMesh();
    	GenerateColors();
    }

    public void OnShapeSettingsUpdate()
    {
    	Initialize();
    	GenerateMesh();
    }

    public void OnColorSettingsUpdate()
    {
    	Initialize();
    	GenerateColors();
    }

    void GenerateMesh()
    {
    	foreach(TerrainFace face in _TerrainFaces)
    	{
    		face.ConstructMesh();
    	}
    }

    void GenerateColors()
    {
    	foreach(MeshFilter meshFilter in _MeshFilters)
    	{
    		meshFilter.GetComponent<MeshRenderer>().sharedMaterial.color = _ColorSettings._PlanetColor;
    	}
    }
}
