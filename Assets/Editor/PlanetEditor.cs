using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet _Planet;

    Editor _ShapeEditor;
    Editor _ColorEditor;

    public override void OnInspectorGUI()
    {
    	using (var check = new EditorGUI.ChangeCheckScope())
    	{
    		base.OnInspectorGUI();

    		if (check.changed)
    		{
    			_Planet.GeneratePlanet();
    		}
    	}
    	
    	if (GUILayout.Button("Generate Planet"))
    	{
    		_Planet.GeneratePlanet();
    	}

    	DrawSettingsEditor(_Planet._ShapeSettings, _Planet.OnShapeSettingsUpdate, ref _Planet._ShapeSettingsEditorFoldout, ref _ShapeEditor);
    	DrawSettingsEditor(_Planet._ColorSettings, _Planet.OnColorSettingsUpdate, ref _Planet._ColorSettingsEditorFoldout, ref _ColorEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
    	if (settings == null)
    	{
    		return;
    	}

    	foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
    	using (var check = new EditorGUI.ChangeCheckScope())
    	{
    		if (foldout)
    		{
    			CreateCachedEditor(settings, null, ref editor);
	    		editor.OnInspectorGUI();

	    		if (check.changed)
	    		{
	    			if (onSettingsUpdated != null)
	    			{
	    				onSettingsUpdated();
	    			}
	    		}
    		}    		
    	}
    }

    private void OnEnable()
    {
    	_Planet = (Planet)target;
    }
}
