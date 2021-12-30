using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;

using System.Collections.Generic;
using JetBrains.Annotations;

namespace HistoryNavigator
{

    [Overlay(editorWindowType: typeof(SceneView), displayName: "History Navigator")]
    public sealed class HistoryNavigatorOverlay : ToolbarOverlay
    {
        [PublicAPI]
        public static readonly List<Object> history = new();
        private static int currentSelectionIndex = -1;
        public static LeftArrowButton leftArrowButton;
        public static RightArrowButton rightArrowButton;

        private HistoryNavigatorOverlay() : base(LeftArrowButton.id, RightArrowButton.id)
        {
            Selection.selectionChanged += UpdateSelection;
            AddCurrentSelection();
        }

        ~HistoryNavigatorOverlay()
        {
            Selection.selectionChanged -= UpdateSelection;
        }

        private void UpdateSelection()
        {
            AddCurrentSelection();
            UpdateButtonState();
        }

        private static void AddCurrentSelection()
        {
            Object current = Selection.activeObject;
            if (current == null) return;
            if (currentSelectionIndex >= 0 && currentSelectionIndex < history.Count && history[currentSelectionIndex] == current) return;
            
            currentSelectionIndex++;
            int pruneCount = history.Count - currentSelectionIndex;
            if (pruneCount > 0)
            {
                history.RemoveRange(index: currentSelectionIndex, count: pruneCount);
            }
            history.Add(item: current);
        }

        public static void GoBack()
        {
            Object previous = GetPreviousSelection(position: out currentSelectionIndex);
            Selection.activeObject = previous;
        }

        public static void GoForward()
        {
            Object next = GetNextSelection(position: out currentSelectionIndex);
            Selection.activeObject = next;
        }

        private static void UpdateButtonState()
        {
            if (leftArrowButton != null)
            {
                Object previous = GetPreviousSelection(position: out _);
                leftArrowButton.UpdatePrevious(previous: previous);
            }
            if (rightArrowButton != null)
            {
                Object next = GetNextSelection(position: out _);
                rightArrowButton.UpdateNext(next: next);
            }
        }

        private static Object GetPreviousSelection(out int position)
        {
            for (position = currentSelectionIndex - 1; position >= 0; position--)
            {
                if (history[position] != null) return history[position];
            }
            return null;
        }

        private static Object GetNextSelection(out int position)
        {
            for (position = currentSelectionIndex + 1; position < history.Count; position++)
            {
                if (history[position] != null) return history[position];
            }
            return null;
        }
    }

}

