﻿using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;
using Chisel;
using Chisel.Core;

namespace Chisel.Editors
{
    [CustomPropertyDrawer(typeof(SurfaceDescription))]
    public sealed class SurfaceDescriptionPropertyDrawer : PropertyDrawer
    {
        const float kSpacing = 2;
        static readonly GUIContent	kUV0Contents            = new GUIContent("UV");
        static readonly GUIContent	kSurfaceFlagsContents   = new GUIContent("Surface Flags");
        static readonly GUIContent	kSmoothingGroupContents = new GUIContent("Smoothing Groups");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty smoothingGroupProp   = property.FindPropertyRelative(nameof(SurfaceDescription.smoothingGroup));
            return  UVMatrixPropertyDrawer.DefaultHeight +
                    kSpacing + 
                    SurfaceFlagsPropertyDrawer.DefaultHeight +
                    kSpacing + 
                    SmoothingGroupPropertyDrawer.GetDefaultHeight(smoothingGroupProp.propertyPath, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty uv0Prop              = property.FindPropertyRelative(nameof(SurfaceDescription.UV0));
            SerializedProperty surfaceFlagsProp     = property.FindPropertyRelative(nameof(SurfaceDescription.surfaceFlags));
            SerializedProperty smoothingGroupProp   = property.FindPropertyRelative(nameof(SurfaceDescription.smoothingGroup));

            EditorGUI.BeginProperty(position, label, property);
            bool prevShowMixedValue			= EditorGUI.showMixedValue;
            try
            { 
                position.height = SurfaceFlagsPropertyDrawer.DefaultHeight; 
                EditorGUI.PropertyField(position, surfaceFlagsProp, kSurfaceFlagsContents, false);
                position.y += position.height + kSpacing;

                position.height = UVMatrixPropertyDrawer.DefaultHeight; 
                EditorGUI.PropertyField(position, uv0Prop, kUV0Contents, false);
                position.y += position.height + kSpacing;

                position.height = SmoothingGroupPropertyDrawer.GetDefaultHeight(smoothingGroupProp.propertyPath, true);
                EditorGUI.PropertyField(position, smoothingGroupProp, kSmoothingGroupContents, false);
            }
            catch (ExitGUIException) { }
            catch (Exception ex) { Debug.LogException(ex); }
            
            EditorGUI.showMixedValue = prevShowMixedValue;
            EditorGUI.EndProperty();
        }
    }
}
