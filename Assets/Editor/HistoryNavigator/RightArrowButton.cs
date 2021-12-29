using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;

namespace HistoryNavigator {

    [EditorToolbarElement(id, typeof(SceneView))]
    public class RightArrowButton : EditorToolbarButton {

        public const string id = "HistoryNavigator/Right";

        public RightArrowButton() {
            text = "Forward";
            icon = Resources.Load<Texture2D>("History/rightArrow");
            clicked += OnClick;
            UpdateNext(null);
            HistoryNavigatorOverlay.rightArrowButton = this;
        }

        void OnClick() {
            HistoryNavigatorOverlay.GoForward();
        }

        public void UpdateNext(Object next) {
            if (next != null) {
                SetEnabled(true);
                tooltip = "Jump to previously selected object (" + next.name + ")";
            } else {
                SetEnabled(false);
                tooltip = "Jump to previously selected object";
            }
        }
    }

}