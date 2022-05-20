using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace OozoraKeisatsu.Game.JoyStickInput
{
	
	public interface IJoyStickInputView
    {
		void Init();
		void StopControll();
		void StartControll();
		IObservable<Vector2> OnJoystick { get; }
		IObservable<Vector2> OnEveryJoystick { get; }

	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class JoyStickInputView : MonoBehaviour ,IJoyStickInputView
	{

		[Inject(Id = "IJoyStickInputComponent")] private Component.IJoyStickInputComponent _joyStickInputComponent = null;

        public IObservable<Vector2> OnJoystick => _joyStickInputComponent.OnJoystick;

        public IObservable<Vector2> OnEveryJoystick => _joyStickInputComponent.OnEveryJoystick;

        public void Init()
		{
			_joyStickInputComponent.Init();

		}
		public void StartControll () => _joyStickInputComponent.StartControll();
		public void StopControll() => _joyStickInputComponent.StopControll();
    }
}