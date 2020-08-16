using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	[InitializeOnLoad]
	internal static class UILayerVisibleSwitcherOnSceneView
	{
		private const string BUTTON_TEXT = " UI ";

		private static readonly Rect       m_buttonPosition = new Rect( 2, 2, 50, 20 );
		private static readonly int        m_uiLayerMask    = LayerMask.GetMask( "UI" );
		private static readonly GUIContent m_visibleContent = EditorGUIUtility.IconContent( "scenevis_visible_hover" );
		private static readonly GUIContent m_hiddenContent  = EditorGUIUtility.IconContent( "scenevis_hidden_hover" );

		static UILayerVisibleSwitcherOnSceneView()
		{
			m_visibleContent.text = BUTTON_TEXT;
			m_hiddenContent.text  = BUTTON_TEXT;

			SceneView.duringSceneGui += OnGUI;
		}

		private static void OnGUI( SceneView sceneView )
		{
			Handles.BeginGUI();

			var isVisible = ( Tools.visibleLayers & m_uiLayerMask ) != 0;
			var content   = isVisible ? m_visibleContent : m_hiddenContent;

			if ( GUI.Button( m_buttonPosition, content ) )
			{
				Tools.visibleLayers ^= m_uiLayerMask;
				SceneView.RepaintAll();
			}

			Handles.EndGUI();
		}
	}
}