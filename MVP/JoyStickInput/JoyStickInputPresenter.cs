using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using JoyStickInput;

	public interface IJoyStickInputPresenter
	{
		IObservable<Vector2> OnInputStick { get; }
		IObservable<Vector2> OnEveryJoyStickInput { get; }
		void StartControll();
		void StopControll();
	}

	public class JoyStickInputPresenter : IJoyStickInputPresenter
	{
		private IJoyStickInputView _joyStickInputView = null;
		private IJoyStickInputModel _joyStickInputModel = null;

		public JoyStickInputPresenter(IJoyStickInputView view,IJoyStickInputModel model) 
		{
			_joyStickInputView = view ?? throw new ArgumentNullException(nameof(view));
			_joyStickInputModel = model ?? throw new ArgumentNullException(nameof(model));
			
			_joyStickInputView.Init();
			Bind();
		}

        public IObservable<Vector2> OnInputStick => _joyStickInputView.OnJoystick;

        public IObservable<Vector2> OnEveryJoyStickInput => _joyStickInputView.OnEveryJoystick;

		public void StartControll() => _joyStickInputView.StartControll();
        public void StopControll() => _joyStickInputView.StopControll();

        private void Bind () 
		{
			

		}
	}
}