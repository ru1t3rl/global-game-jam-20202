using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FMODUnity;

namespace Ru1t3rl.Audio
{
    [CustomEditor(typeof(AudioSource))]
    [CanEditMultipleObjects]
    public class AudioSourceEditor : Editor
    {
        StudioEventEmitterEditor.ParameterValueView parameterValueView;
        SerializedObject eventEmitterSerialized;

        public void OnEnable()
        {
            eventEmitterSerialized = new SerializedObject((target as AudioSource).eventEmitter);
            parameterValueView = new StudioEventEmitterEditor.ParameterValueView(eventEmitterSerialized);
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            var emitterRef = serializedObject.FindProperty("emitterRef");
            var triggerEvent = serializedObject.FindProperty("triggerEvent");
            var stopEvent = serializedObject.FindProperty("stopEvent");
            var playEvent = serializedObject.FindProperty("playEvent");
            var eventRef = serializedObject.FindProperty("eventRef");
            var paramRefs = serializedObject.FindProperty("paramRefs");
            var eventPath = eventRef.FindPropertyRelative("Path");

            var fadeout = eventEmitterSerialized.FindProperty("AllowFadeout");
            var once = eventEmitterSerialized.FindProperty("TriggerOnce");
            var preload = eventEmitterSerialized.FindProperty("Preload");

            EditorGUILayout.PropertyField(emitterRef, new GUIContent("Emitter Ref"));
            EditorGUILayout.PropertyField(triggerEvent, new GUIContent("Trigger Event"));

            EditorGUILayout.PropertyField(playEvent, new GUIContent("Play Event"));
            EditorGUILayout.PropertyField(stopEvent, new GUIContent("Stop Event"));
            EditorGUILayout.PropertyField(eventRef, new GUIContent("Event"));

            EditorEventRef editorEvent = EventManager.EventFromPath(eventPath.stringValue);
            parameterValueView.OnGUI(editorEvent, !eventRef.hasMultipleDifferentValues);

            fadeout.isExpanded = EditorGUILayout.Foldout(fadeout.isExpanded, "Advanced Controls");
            if (fadeout.isExpanded)
            {
                EditorGUILayout.PropertyField(preload, new GUIContent("Preload Sample Data"));
                EditorGUILayout.PropertyField(fadeout, new GUIContent("Allow Fadeout When Stopping"));
                EditorGUILayout.PropertyField(once, new GUIContent("Trigger Once"));
            }

            serializedObject.ApplyModifiedProperties();
            eventEmitterSerialized.ApplyModifiedProperties();
        }
    }
}