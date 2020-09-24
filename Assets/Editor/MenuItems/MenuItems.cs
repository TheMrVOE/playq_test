using Editor.Windows;
using UnityEditor;

namespace Editor.MenuItems
{
    public static class MenuItems
    {
        private const string SCROLLABLE_CONTAINER_PATH = "Window/Test/ScrollableContainer"; 
        
        [MenuItem(SCROLLABLE_CONTAINER_PATH)]
        public static void ShowWindow()
        {
            var windowType = typeof(ScrollableContainerWindow);
            EditorWindow.GetWindow(windowType, false, ScrollableContainerWindow.WINDOW_NAME);
        }
    }
}