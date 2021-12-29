using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;

namespace HistoryNavigator {

    [EditorToolbarElement(id, typeof(SceneView))]
    public class LeftArrowButton : EditorToolbarButton {

        public const string id = "HistoryNavigator/Left";

        public LeftArrowButton() {
            text = "Back";
            icon = Resources.Load<Texture2D>("History/leftArrow");
            clicked += OnClick;
            UpdatePrevious(null);
            HistoryNavigatorOverlay.leftArrowButton = this;
        }

        void OnClick() {
            HistoryNavigatorOverlay.GoBack();
        }

        public void UpdatePrevious(Object previous) {
            if (previous != null) {
                SetEnabled(true);
                tooltip = "Jump to previously selected object (" + previous.name + ")";
            } else {
                SetEnabled(false);
                tooltip = "Jump to previously selected object";
            }
        }
    }

}