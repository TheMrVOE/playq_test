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

        private const int HORIZONTAL_SPACING = 10;

        private const int MIN_SCROLL_VALUE = 3;

        private EnhancedContainer _container;

        private Vector2 _scrollPosition;
        private Color _backGreenColor = new Color(186 / 255f, 217 / 255f, 190 / 255f);
        private Color _backRedColor = new Color(222 / 255f, 189 / 255f, 191 / 255f);
        private GUIStyle _containerItemStyle;
        private GUIStyle _createNewButtonStyle;

        private bool _isInitialized;
        private bool _isRepaintInvoked;

        private int ItemSize => HORIZONTAL_SPACING * 2 + CONTAINER_ITEM_HEIGHT;

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
            containerItemStyle.margin = new RectOffset(5, 5, HORIZONTAL_SPACING, HORIZONTAL_SPACING);
            containerItemStyle.fixedHeight = CONTAINER_ITEM_HEIGHT;
            containerItemStyle.fontStyle = FontStyle.Bold;

            _containerItemStyle = containerItemStyle;

            var createNewButtonStyle = new GUIStyle(GUI.skin.button);
            createNewButtonStyle.fixedHeight = CREATE_NEW_BUTTON_HEIGHT;

            _createNewButtonStyle = createNewButtonStyle;
        }

        private void OnGUI()
        {
            if (_isRepaintInvoked)
            {
                _isRepaintInvoked = false;
                return;
            }

            if (!_isInitialized)
                Initialize();

            DrawContainerContent();
            DrawCreateNewButton();
        }
        
        private void DrawContainerContent()
        {
            MoveCursorOnScrollIfNeed();

            var itemsToRender = Screen.height / ItemSize;
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            for (int i = 0; i < itemsToRender; i++)
            {
                var value = _container.Value;
                var prevColor = GUI.backgroundColor;
                GUI.backgroundColor = value ? _backGreenColor : _backRedColor;

                _container.MoveForward();
                GUILayout.Button(value.ToString(), _containerItemStyle);
                GUI.backgroundColor = prevColor;
            }

            for (int i = 0; i < itemsToRender; i++)
                _container.MoveBackward();

            GUILayout.EndScrollView();
        }

        private void MoveCursorOnScrollIfNeed()
        {
            var scrollStep = GetScrollStep();
            if (scrollStep > 0)
            {
                _container.MoveForward();
                RepaintInternal();
            }
            else if (scrollStep < 0)
            {
                _container.MoveBackward();
                RepaintInternal();
            }
        }

        private void RepaintInternal()
        {
            _isRepaintInvoked = true;
            Repaint();
        }

        private int GetScrollStep()
        {
            if (Event.current.delta == Vector2.zero)
                return 0;

            var scrollStep = Event.current.delta.y / MIN_SCROLL_VALUE;
            return Mathf.RoundToInt(scrollStep);
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