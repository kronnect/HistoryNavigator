using UnityEditor;
using UnityEditor.ShortcutManagement;
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

        //Unfortunately we can't bind it to Mouse4 at this moment.
        //And as far as I'm aware we can't make the context the Hierarchy Window, otherwise I'd have the Scene and Hierarchy as contexts.
        private const KeyCode _SHORTCUT = KeyCode.Equals; //KeyCode.Mouse4;
        [Shortcut(id: "History Navigator/Next", defaultKeyCode: _SHORTCUT)]
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