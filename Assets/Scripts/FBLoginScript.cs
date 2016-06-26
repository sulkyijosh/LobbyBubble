using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Facebook.Unity;
using UnityEngine.UI;

namespace Facebook.Unity.Example
{
	internal sealed class FBLoginScript : MenuBase
	{
		public Text loginButtonText;
		bool loggedIn;
		bool firstPlay = true;
	
		protected override void Awake () 
		{
			Application.targetFrameRate = 60;

			if(!FB.IsInitialized)
			{
				FB.Init(InitCallBack, OnHideUnity);
			}
			else
			{
				FB.ActivateApp();
			}

			if(firstPlay == true)
			{
				loggedIn = false;
				firstPlay = false;
			}

		}
	
	
		void InitCallBack()
		{
			if(FB.IsInitialized)
			{
				FB.ActivateApp();
			}
			else
			{
				Debug.Log("Failed to Initialize Facebook SDK");
			}
		}
	
		void OnHideUnity(bool isGameShown)
		{
			if(!isGameShown)
			{
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = 1;
			}
		}
	
		void AuthCallBack(ILoginResult result)
		{
			var perms = new List<string>(){"public_profile", "email", "user_friends"};
			FB.LogInWithReadPermissions(perms, AuthCallBack);
	
			if(FB.IsLoggedIn)
			{
				var authToken = AccessToken.CurrentAccessToken;
				Debug.Log(authToken.UserId);
				//foreach(string perms in authToken.Permissions)
				{
					Debug.Log(perms);
				}
			}
			else
			{
				Debug.Log("Cancelled Login");
			}
		}
	
		public void FacebookLoginToggle()
		{
			if(loggedIn == false)
			{
				FB.LogInWithReadPermissions(new List<string>() {"public_profile", "email", "user_friends"}, this.HandleResult);
				loggedIn = true;
				loginButtonText.text = "Logout";
			}
			else if(loggedIn == true)
			{
				FB.LogOut();
				loggedIn = false;
				loginButtonText.text = "Login";
			}
		}

		private void OnInitComplete()
		{
			this.Status = "Success - Check log for details";
			this.LastResponse = "Success Response: OnInitComplete Called\n";
			string logMessage = string.Format(
				"OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'",
				FB.IsLoggedIn,
				FB.IsInitialized);
			LogView.AddLog(logMessage);
		}

		private void CallFBLoginForPublish()
		{
			// It is generally good behavior to split asking for read and publish
			// permissions rather than ask for them all at once.
			//
			// In your own game, consider postponing this call until the moment
			// you actually need it.
			FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, this.HandleResult);
		}

		protected override void GetGui()
		{
			bool enabled = GUI.enabled;
			if (this.Button("FB.Init"))
			{
				FB.Init(this.OnInitComplete, this.OnHideUnity);
				this.Status = "FB.Init() called with " + FB.AppId;
			}

			GUILayout.BeginHorizontal();

			GUI.enabled = enabled && FB.IsInitialized;
			if (this.Button("Login"))
			{
				this.FacebookLoginToggle();
				this.Status = "Login called";
			}

			GUI.enabled = FB.IsLoggedIn;
			if (this.Button("Get publish_actions"))
			{
				this.CallFBLoginForPublish();
				this.Status = "Login (for publish_actions) called";
			}

			#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_EDITOR
			if (this.Button("Logout"))
			{
				FacebookLoginToggle();
				this.Status = "Logout called";
			}
			#endif
			// Fix GUILayout margin issues
			GUILayout.Label(GUIContent.none, GUILayout.MinWidth(ConsoleBase.MarginFix));
			GUILayout.EndHorizontal();

			GUI.enabled = enabled && FB.IsInitialized;
			if (this.Button("Share Dialog"))
			{
				this.SwitchMenu(typeof(DialogShare));
			}

			bool savedEnabled = GUI.enabled;
			GUI.enabled = enabled &&
				AccessToken.CurrentAccessToken != null &&
				AccessToken.CurrentAccessToken.Permissions.Contains("publish_actions");
			if (this.Button("Game Groups"))
			{
				this.SwitchMenu(typeof(GameGroups));
			}

			GUI.enabled = savedEnabled;

			if (this.Button("App Requests"))
			{
				this.SwitchMenu(typeof(AppRequests));
			}

			if (this.Button("Graph Request"))
			{
				this.SwitchMenu(typeof(GraphRequest));
			}

			if (Constants.IsWeb && this.Button("Pay"))
			{
				this.SwitchMenu(typeof(Pay));
			}

			if (this.Button("App Events"))
			{
				this.SwitchMenu(typeof(AppEvents));
			}

			if (this.Button("App Links"))
			{
				this.SwitchMenu(typeof(AppLinks));
			}

			if (Constants.IsMobile && this.Button("App Invites"))
			{
				this.SwitchMenu(typeof(AppInvites));
			}

			if (Constants.IsMobile && this.Button("Access Token"))
			{
				this.SwitchMenu(typeof(AccessTokenMenu));
			}

			GUI.enabled = enabled;
		}
	}
}