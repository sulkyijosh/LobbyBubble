/*namespace Facebook.Unity.Login
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	internal class ConsoleBaseScript : MonoBehaviour
	{
		protected static int ButtonHeight = Constants.IsMobile ? 60 : 24;
		protected static int MainWindowWidth = Constants.IsMobile ? Screen.width - 30 : 700;
		protected static int MainWindowFullWidth = Constants.IsMobile ? Screen.width : 760;
		protected static int MarginFix = Constants.IsMobile ? 0 : 48;
		private const int DpiScalingFactor = 160;
		private static Stack<string> menuStack = new Stack<string>();
		private string status = "Ready";
		private string lastResponse = string.Empty;
		private Vector2 scrollPosition = Vector2.zero;

		// DPI scaling
		private float? scaleFactor;
		private GUIStyle textStyle;
		private GUIStyle buttonStyle;
		private GUIStyle textInputStyle;
		private GUIStyle labelStyle;

		protected static Stack<string> MenuStack
		{
			get
			{
				return ConsoleBaseScript.menuStack;
			}

			set
			{
				ConsoleBaseScript.menuStack = value;
			}
		}

		protected string Status
		{
			get
			{
				return this.status;
			}

			set
			{
				this.status = value;
			}
		}

		protected Texture2D LastResponseTexture { get; set; }

		protected string LastResponse
		{
			get
			{
				return this.lastResponse;
			}

			set
			{
				this.lastResponse = value;
			}
		}

		protected Vector2 ScrollPosition
		{
			get
			{
				return this.scrollPosition;
			}

			set
			{
				this.scrollPosition = value;
			}
		}

		// Note we assume that these styles will be accessed from OnGUI otherwise the
		// unity APIs will fail.
		protected float ScaleFactor
		{
			get
			{
				if (!this.scaleFactor.HasValue)
				{
					this.scaleFactor = Screen.dpi / ConsoleBaseScript.DpiScalingFactor;
				}

				return this.scaleFactor.Value;
			}
		}

		protected int FontSize
		{
			get
			{
				return (int)Math.Round(this.ScaleFactor * 16);
			}
		}

		protected GUIStyle TextStyle
		{
			get
			{
				if (this.textStyle == null)
				{
					this.textStyle = new GUIStyle(GUI.skin.textArea);
					this.textStyle.alignment = TextAnchor.UpperLeft;
					this.textStyle.wordWrap = true;
					this.textStyle.padding = new RectOffset(10, 10, 10, 10);
					this.textStyle.stretchHeight = true;
					this.textStyle.stretchWidth = false;
					this.textStyle.fontSize = this.FontSize;
				}

				return this.textStyle;
			}
		}

		protected GUIStyle ButtonStyle
		{
			get
			{
				if (this.buttonStyle == null)
				{
					this.buttonStyle = new GUIStyle(GUI.skin.button);
					this.buttonStyle.fontSize = this.FontSize;
				}

				return this.buttonStyle;
			}
		}

		protected GUIStyle TextInputStyle
		{
			get
			{
				if (this.textInputStyle == null)
				{
					this.textInputStyle = new GUIStyle(GUI.skin.textField);
					this.textInputStyle.fontSize = this.FontSize;
				}

				return this.textInputStyle;
			}
		}

		protected GUIStyle LabelStyle
		{
			get
			{
				if (this.labelStyle == null)
				{
					this.labelStyle = new GUIStyle(GUI.skin.label);
					this.labelStyle.fontSize = this.FontSize;
				}

				return this.labelStyle;
			}
		}

		protected virtual void Awake()
		{
			// Limit the framerate to 60 to keep device from burning through cpu
			Application.targetFrameRate = 60;
		}

		protected bool Button(string label)
		{
			return GUILayout.Button(
				label,
				this.ButtonStyle,
				GUILayout.MinHeight(ConsoleBaseScript.ButtonHeight * this.ScaleFactor),
				GUILayout.MaxWidth(ConsoleBaseScript.MainWindowWidth));
		}

		protected void LabelAndTextField(string label, ref string text)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(label, this.LabelStyle, GUILayout.MaxWidth(200 * this.ScaleFactor));
			text = GUILayout.TextField(
				text,
				this.TextInputStyle,
				GUILayout.MaxWidth(ConsoleBaseScript.MainWindowWidth - 150));
			GUILayout.EndHorizontal();
		}

		protected bool IsHorizontalLayout()
		{
			#if UNITY_IOS || UNITY_ANDROID
			return Screen.orientation == ScreenOrientation.Landscape;
			#else
			return true;
			#endif
		}

		protected void SwitchMenu(Type menuClass)
		{
			ConsoleBaseScript.menuStack.Push(this.GetType().Name);
			Application.LoadLevel(menuClass.Name);
		}

		protected void GoBack()
		{
			if (ConsoleBaseScript.menuStack.Any())
			{
				Application.LoadLevel(ConsoleBaseScript.menuStack.Pop());
			}
		}
	}
}
*/