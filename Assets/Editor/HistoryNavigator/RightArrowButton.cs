using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;

namespace HistoryNavigator 
{
    [EditorToolbarElement(id, targetWindows: typeof(SceneView))]
    public class RightArrowButton : EditorToolbarButton 
    {
        public const string id = "HistoryNavigator/Right";

        public RightArrowButton() 
        {
            //text = "Forward";
            icon = Resources.Load<Texture2D>(path: "History/rightArrow");
            clicked += OnClick;
            UpdateNext(null);
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