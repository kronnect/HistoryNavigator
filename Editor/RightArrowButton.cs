using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Kronnect.Tools.HistoryNavigator 
{
    [EditorToolbarElement(id, targetWindows: typeof(SceneView))]
    public class RightArrowButton : EditorToolbarButton 
    {
        public const string id = "HistoryNavigator/Right";

        public RightArrowButton()
        {
            icon = (Texture2D) EditorGUIUtility.IconContent(name: "ArrowNavigationRight").image;
            clicked += OnClick;
            UpdateNext(next: null);
            
            HistoryNavigatorOverlay.rightArrowButton = this;
        }

        private static void OnClick() 
        {
            HistoryNavigatorOverlay.GoForward();
        }

        public void UpdateNext(Object next) 
        {
            if (next != null) 
            {
                SetEnabled(true);
                tooltip = $"Jump to next selected object ({next.name})";
            }
            else 
            {
                SetEnabled(false);
                tooltip = "Jump to next selected object";
            }
        }
    }

}