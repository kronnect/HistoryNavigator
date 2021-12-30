using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Kronnect.Tools.HistoryNavigator 
{
    [EditorToolbarElement(id, targetWindows: typeof(SceneView))]
    public class LeftArrowButton : EditorToolbarButton 
    {
        public const string id = "HistoryNavigator/Left";

        public LeftArrowButton()
        {
            icon = (Texture2D) EditorGUIUtility.IconContent(name: "ArrowNavigationLeft").image;
            clicked += OnClick;
            UpdatePrevious(previous: null);
            
            HistoryNavigatorOverlay.leftArrowButton = this;
        }

        //Unfortunately we can't bind it to Mouse3 at this moment.
        //And as far as I'm aware we can't make the context the Hierarchy Window, otherwise I'd have the Scene and Hierarchy as contexts.
        private const KeyCode _SHORTCUT = KeyCode.Minus; //KeyCode.Mouse3;
        [Shortcut(id: "History Navigator/Prev", defaultKeyCode: _SHORTCUT)]
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