using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;

namespace HistoryNavigator 
{
    [EditorToolbarElement(id, targetWindows: typeof(SceneView))]
    public class LeftArrowButton : EditorToolbarButton 
    {
        public const string id = "HistoryNavigator/Left";

        public LeftArrowButton() 
        {
            //text = "Back";
            icon = Resources.Load<Texture2D>(path: "History/leftArrow");
            clicked += OnClick;
            UpdatePrevious(previous: null);
            
            HistoryNavigatorOverlay.leftArrowButton = this;
        }

        private static void OnClick() 
        {
            HistoryNavigatorOverlay.GoBack();
        }

        public void UpdatePrevious(Object previous) 
        {
            if (previous != null) 
            {
                SetEnabled(true);
                tooltip = $"Jump to previously selected object ({previous.name})";
            }
            else 
            {
                SetEnabled(false);
                tooltip = "Jump to previously selected object";
            }
        }
    }

}