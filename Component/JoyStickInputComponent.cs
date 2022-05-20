using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Component
{
	public interface IJoyStickInputComponent
    {
		void Init();
		void StopControll();
		void StartControll();
		IObservable<Vector2> OnJoystick { get; }
		IObservable<Vector2> OnEveryJoystick { get; }
    }


	public class JoyStickInputComponent : MonoBehaviour ,IJoyStickInputComponent
	{

		[SerializeField] private Joystick _joystick = null;

        public IObservable<Vector2> OnJoystick => _onEveryJoyStickInputVector2;

		public IObservable<Vector2> OnEveryJoystick => _onEveryJoyStickInputVector2;

        Vector2ReactiveProperty _onJoyStickInputVector2 = new Vector2ReactiveProperty();
		Subject<Vector2> _onEveryJoyStickInputVector2 = new Subject<Vector2>();
		[SerializeField]Vector2 _joystickTemplate;


        public void Init () 
		{
			_joystickTemplate = new Vector2();

		}

        private void FixedUpdate()
        {
			_joystickTemplate.x = _joystick.Horizontal;
			_joystickTemplate.y = _joystick.Vertical;
			_onJoyStickInputVector2.Value = _joystickTemplate;

			_onEveryJoyStickInputVector2.OnNext(_joystickTemplate);
        }

		public void StartControll()
        {
			_joystick.gameObject.SetActive(true);
			_joystick.FinishContoll();
        }

        public void StopControll()
        {
			_joystick.FinishContoll();
			_joystick.gameObject.SetActive(false);
        }
    }
}