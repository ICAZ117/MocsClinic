// Copyright 2020, Tiny Angle Labs, All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SpeechBlendEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(SpeechBlend))]
public class BlendShapeList : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpeechBlend SB = (SpeechBlend)target;

        if (SB != null)
        {
            if (SB.faceBlendshapes == null)
            {
                SB.faceBlendshapes = new SpeechUtil.VisemeBlendshapeIndexes(VoiceProfile.TemplateFromType(SB.shapeTemplate));
            }
            if (SB.visemeWeightTuning == null)
            {
                SB.visemeWeightTuning = new SpeechUtil.VisemeWeight(VoiceProfile.TemplateFromType(SB.shapeTemplate));
            }


            if (SB.shapeTemplate != SB.faceBlendshapes.template.templateType)
            {
                int mouthOpenIndex = SB.faceBlendshapes.mouthOpenIndex;
                SB.faceBlendshapes = new SpeechUtil.VisemeBlendshapeIndexes(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                SB.visemeWeightTuning = new SpeechUtil.VisemeWeight(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                SB.faceBlendshapes.mouthOpenIndex = mouthOpenIndex;
            }
            SkinnedMeshRenderer smr = SB.headMesh;
            List<string> bs_names = new List<string>();
            bs_names.Add("none");

            if (smr != null)
            {
                Mesh m = smr.sharedMesh;
                for (int i = 0; i < m.blendShapeCount; i++)
                {
                    string s = m.GetBlendShapeName(i);
                    bs_names.Add(s);
                }
            }

            SB.showBlendShapeMenu = EditorGUILayout.Foldout(SB.showBlendShapeMenu, "Blendshapes");

            if (SB.showBlendShapeMenu)
            {
                if (SB.faceBlendshapes.template == null)
                {
                    SB.faceBlendshapes = new SpeechUtil.VisemeBlendshapeIndexes(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                    SB.visemeWeightTuning = new SpeechUtil.VisemeWeight(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                }
                if (SB.faceBlendshapes.template.visemeNames == null)
                {
                    SB.faceBlendshapes = new SpeechUtil.VisemeBlendshapeIndexes(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                    SB.visemeWeightTuning = new SpeechUtil.VisemeWeight(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                }
                if (SB.faceBlendshapes.template.visemeNames.Length == 0)
                {
                    SB.faceBlendshapes = new SpeechUtil.VisemeBlendshapeIndexes(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                    SB.visemeWeightTuning = new SpeechUtil.VisemeWeight(VoiceProfile.TemplateFromType(SB.shapeTemplate));
                }

                if (GUILayout.Button("Auto-Detect"))
                {
                    int[] bs_inputs_auto = SB.faceBlendshapes.ReturnArray();
                    for (int i = 0; i < SB.faceBlendshapes.template.Nvis; i++)
                    {
                        if (!SB.faceBlendshapes.BlendshapeAssigned(i))
                        {
                            string str = "VSM" + SB.faceBlendshapes.template.visemeNames[i];
                            for (int j = 1; j < (bs_names.Count); j++)
                            {
                                if (bs_names[j].Contains(str))
                                {
                                    bs_inputs_auto[i] = j - 1;
                                    break;
                                }
                            }
                            if (!SB.faceBlendshapes.BlendshapeAssigned(i))
                            {
                                str = SB.faceBlendshapes.template.visemeNames[i];
                                for (int j = 1; j < (bs_names.Count); j++)
                                {
                                    if (bs_names[j].Contains(str))
                                    {
                                        bs_inputs_auto[i] = j - 1;
                                        break; 
                                    }
                                }
                            }
                        }
                    }
                    if (!SB.faceBlendshapes.BlendshapeAssigned("mouthOpen"))
                    {
                        for (int j = 1; j < (bs_names.Count - 1); j++)
                        {
                            if (bs_names[j].Contains("MouthOpen") & !bs_names[j].Contains("Wide"))
                            {
                                SB.faceBlendshapes.mouthOpenIndex = j - 1;
                                break;
                            }
                            if (bs_names[j].Contains("Mouth_Open") & !bs_names[j].Contains("Wide"))
                            {
                                SB.faceBlendshapes.mouthOpenIndex = j - 1;
                                break;
                            }
                        }
                    }
                    SB.faceBlendshapes.LoadFromArray(bs_inputs_auto);
                }
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Mouth Open", GUILayout.Width(80));
                SB.faceBlendshapes.mouthOpenIndex = EditorGUILayout.Popup(SB.faceBlendshapes.mouthOpenIndex + 1, bs_names.ToArray()) - 1;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("\tVisemes");
                int[] bs_inputs = SB.faceBlendshapes.ReturnArray();
                float[] v_weights = SB.visemeWeightTuning.ReturnArray();
                for (int i = 0; i < SB.faceBlendshapes.template.Nvis; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("" + SB.faceBlendshapes.template.visemeNames[i] + "", GUILayout.Width(120));
                    bs_inputs[i] = EditorGUILayout.Popup(bs_inputs[i] + 1, bs_names.ToArray()) - 1;
                    v_weights[i] = (EditorGUILayout.Slider(v_weights[i] / 6f + 2f / 6f, 0f, 1f) - 2f / 6f) * 6f;
                    EditorGUILayout.EndHorizontal();
                }
                SB.faceBlendshapes.LoadFromArray(bs_inputs);
                SB.visemeWeightTuning.LoadFromArray(v_weights);
            }
        }
    }
}
#endif