using Collections;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows
{
    public class ScrollableContainerWindow : EditorWindow
    {
        public const string WINDOW_NAME = "Scrollable container";

        private const int CONTAINER_ITEM_HEIGHT = 80;
        private const int CREATE_NEW_BUTTON_HEIGHT = 40;
        private const int CONTAINER_PRICISION = 20;

        private EnhancedContainer _container;
        
        private Vector2 _scrollPosition;
        private Color _backGreenColor = new Color(186 / 255f, 217 / 255f, 190 / 255f);
        private Color _backRedColor = new Color(222 / 255f, 189 / 255f, 191 / 255f);
        private GUIStyle _containerItemStyle;
        private GUIStyle _createNewButtonStyle;
        
        private bool _isInitialized;
        
        private void Awake()
        {
            CreateContainer();
        }

        private void Initialize()
        {
            InitStyles();
        }

        private void InitStyles()
        {
            var containerItemStyle = new GUIStyle(GUI.skin.button);
            containerItemStyle.margin = new RectOffset(5,5,10,10);
            containerItemStyle.fixedHeight = CONTAINER_ITEM_HEIGHT;
            containerItemStyle.fontStyle = FontStyle.Bold;
            
            _containerItemStyle = containerItemStyle;
            
            var createNewButtonStyle = new GUIStyle(GUI.skin.button);
            createNewButtonStyle.fixedHeight = CREATE_NEW_BUTTON_HEIGHT;
            
            _createNewButtonStyle = createNewButtonStyle;
        }

        private void OnGUI()
        {
            if(!_isInitialized)
                Initialize();
            
            DrawContainerContent();
            DrawCreateNewButton();  
        }

        private void DrawContainerContent()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            for (int i = 0; i < _container.Count; i++)
            {
                var value = _container.Value;
                var prevColor = GUI.backgroundColor;
                GUI.backgroundColor = value ? _backGreenColor : _backRedColor;
                
                _container.MoveForward();
                GUILayout.Button(value.ToString(), _containerItemStyle);
                GUI.backgroundColor = prevColor;
            }

            GUILayout.EndScrollView();
        }

        private void DrawCreateNewButton()
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create new", _createNewButtonStyle))
                CreateContainer();
        }

        private void CreateContainer()
        {
            _container = new EnhancedContainer(CONTAINER_PRICISION);
        }
    }
}