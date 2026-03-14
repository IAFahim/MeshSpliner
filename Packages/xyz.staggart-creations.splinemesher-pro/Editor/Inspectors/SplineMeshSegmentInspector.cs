using System.Reflection;
using sc.splinemesher.pro.runtime;
using UnityEditor;
using UnityEngine;

namespace sc.splinemesher.pro.editor
{
    [CustomEditor(typeof(SplineMeshSegment))]
    public class SplineMeshSegmentInspector : Editor
    {
        private SplineMeshSegment component;

        private float memorySize;
        private int vertexCount, triangleCount;
        private bool readable;
        
        private MeshPreview sourceMeshPreview;
        private PreviewRenderUtility meshPreviewUtility;
        private float cameraDistance = 1f;
        private FieldInfo settingsField;
        private object settings;
        private FieldInfo orthoPositionField;
        private FieldInfo zoomFactor;
        
        void OnEnable()
        {
            component = (SplineMeshSegment)target;

            if (component.mesh)
            {
                memorySize = Utilities.GetMemorySize(component.mesh);
                readable = component.mesh.isReadable;
            }

            (vertexCount, triangleCount) = component.GetMeshStats();
            
            sourceMeshPreview = new MeshPreview(new Mesh());
            
            //Override zoom level
            meshPreviewUtility = (PreviewRenderUtility)typeof(MeshPreview).GetField("m_PreviewUtility", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sourceMeshPreview);
            meshPreviewUtility.camera.fieldOfView = 25;
            meshPreviewUtility.camera.backgroundColor = UnityEngine.Color.white * 0.09f;
            
            //Use reflection to access the private m_Settings field
            settingsField = typeof(MeshPreview).GetField("m_Settings", BindingFlags.Instance | BindingFlags.NonPublic);
            settings = settingsField.GetValue(sourceMeshPreview);
            orthoPositionField = settings.GetType().GetField("m_PivotPositionOffset", BindingFlags.Instance | BindingFlags.NonPublic);
            orthoPositionField.SetValue(settings, Vector3.forward * 20f);
            
            FieldInfo m_PreviewDir = settings.GetType().GetField("m_PreviewDir", BindingFlags.Instance | BindingFlags.NonPublic);
            m_PreviewDir.SetValue(settings, new Vector2(0, -33));
            
            zoomFactor = settings.GetType().GetField("m_ZoomFactor", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public override void OnInspectorGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                UI.DrawHeader();
            }

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.ObjectField("Container", component.Container, typeof(GameObject), true);
            }
            
            EditorGUILayout.Space();

            if (component.mesh)
            {
                Mesh inputMesh = component.mesh;

                if (sourceMeshPreview.mesh != inputMesh) sourceMeshPreview.mesh = inputMesh;
                Rect previewRect = EditorGUILayout.GetControlRect(false, 150f);

                var previewMouseOver = previewRect.Contains(Event.current.mousePosition);
                var meshPreviewFocus = previewMouseOver && (Event.current.type == EventType.MouseDown ||
                                                            Event.current.type == EventType.MouseDrag);
                
                //Handle scroll wheel separately - only needs mouse over, not dragging
                if (previewMouseOver && Event.current.type == EventType.ScrollWheel)
                {
                    float scrollDelta = Event.current.delta.y;
                    cameraDistance += scrollDelta * 0.025f;
                    
                    zoomFactor.SetValue(settings, cameraDistance);
                    
                    Event.current.Use();
                    Repaint();
                }

                if (meshPreviewFocus)
                {
                    sourceMeshPreview.OnPreviewGUI(previewRect, GUIStyle.none);
                }
                else
                {
                    if (Event.current.type == EventType.Repaint)
                    {
                        GUI.DrawTexture(previewRect,
                            sourceMeshPreview.RenderStaticPreview((int)previewRect.width, (int)previewRect.height));
                    }
                }
                
                using (new EditorGUILayout.HorizontalScope())
                {
                    sourceMeshPreview.OnPreviewSettings();
                }

                EditorGUILayout.Space();
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField(
                    new GUIContent($"  Vertices: {vertexCount:N0}",
                        EditorGUIUtility.IconContent("d_EditCollider").image), EditorStyles.miniLabel);
                EditorGUILayout.LabelField(
                    new GUIContent($" Triangles: {triangleCount:N0}",
                        EditorGUIUtility.IconContent("d_ProfilerColumn.WarningCount").image),
                    EditorStyles.miniLabel);
                EditorGUILayout.LabelField(
                    new GUIContent($" Size: {Utilities.FormatMemorySize(memorySize)}",
                        EditorGUIUtility.IconContent("Profiler.Memory").image), EditorStyles.miniLabel);
                EditorGUILayout.LabelField(
                    new GUIContent($" Readable: {readable}", EditorGUIUtility.IconContent("d_SaveAs").image),
                    EditorStyles.miniLabel);
            }

            UI.DrawFooter();
        }
        
        public void OnDisable()
        {
            if (sourceMeshPreview != null)
            {
                sourceMeshPreview.Dispose();
                sourceMeshPreview = null;
            }
        }
    }
}