using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace HistoryNavigator
{

    [Overlay(typeof(SceneView), "History Navigator")]
    public class HistoryNavigatorOverlay : ToolbarOverlay
    {

        public static List<Object> history = new List<Object>();
        static int currentSelectionIndex = -1;
        public static LeftArrowButton leftArrowButton;
        public static RightArrowButton rightArrowButton;

        HistoryNavigatorOverlay() : base(LeftArrowButton.id, RightArrowButton.id)
        {
            Selection.selectionChanged += UpdateSelection;
            AddCurrentSelection();
        }

        ~HistoryNavigatorOverlay()
        {
            Selection.selectionChanged -= UpdateSelection;
        }

        void UpdateSelection()
        {
            AddCurrentSelection();
            UpdateButtonState();
        }

        void AddCurrentSelection()
        {
            Object current = Selection.activeObject;
            if (current == null) return;
            if (currentSelectionIndex >= 0 && currentSelectionIndex < history.Count && history[currentSelectionIndex] == current) return;
            currentSelectionIndex++;
            int pruneCount = history.Count - currentSelectionIndex;
            if (pruneCount > 0)
            {
                history.RemoveRange(currentSelectionIndex, pruneCount);
            }
            history.Add(current);
        }

        public static void GoBack()
        {
            Object previous = GetPreviousSelection(out currentSelectionIndex);
            Selection.activeObject = previous;
        }

        public static void GoForward()
        {
            Object next = GetNextSelection(out currentSelectionIndex);
            Selection.activeObject = next;
        }

        static void UpdateButtonState()
        {
            if (leftArrowButton != null)
            {
                Object previous = GetPreviousSelection(out _);
                leftArrowButton.UpdatePrevious(previous);
            }
            if (rightArrowButton != null)
            {
                Object next = GetNextSelection(out _);
                rightArrowButton.UpdateNext(next);
            }
        }

        static Object GetPreviousSelection(out int position)
        {
            for (position = currentSelectionIndex - 1; position >= 0; position--)
            {
                if (history[position] != null) return history[position];
            }
            return null;
        }

        static Object GetNextSelection(out int position)
        {
            for (position = currentSelectionIndex + 1; position < history.Count; position++)
            {
                if (history[position] != null) return history[position];
            }
            return null;
        }
    }

}

