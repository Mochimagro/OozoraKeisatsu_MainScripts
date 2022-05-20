using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using MainPlayerCharacter;

	public interface IMainPlayerCharacterPresenter
	{
		void Ready();
	}

	public class MainPlayerCharacterPresenter : IMainPlayerCharacterPresenter
	{
		private IMainPlayerCharacterView _mainPlayerCharacterView = null;
		private IMainPlayerCharacterModel _mainPlayerCharacterModel = null;
		private IJoyStickInputPresenter _joyStickInputPresenter = null;

		public MainPlayerCharacterPresenter(IMainPlayerCharacterView view,IMainPlayerCharacterModel model,
			IJoyStickInputPresenter joyStickInput) 
		{
			_mainPlayerCharacterView = view ?? throw new ArgumentNullException(nameof(view));
			_mainPlayerCharacterModel = model ?? throw new ArgumentNullException(nameof(model));

			_joyStickInputPresenter = joyStickInput ?? throw new ArgumentNullException(nameof(joyStickInput));


			_mainPlayerCharacterView.Init();
			Bind();
		}

        public void Ready()
        {
			_mainPlayerCharacterView.SetStart();
        }

        private void Bind () 
		{

			_mainPlayerCharacterView.OnCatchTarget.Subscribe(target =>
			{
				// _scorePresenter.AddScore(target.Score);
				// target.ObjectActive = false;
				target.Catched();
			});

			_joyStickInputPresenter.OnEveryJoyStickInput.Subscribe(value =>
			{
				_mainPlayerCharacterView.Move(value);
			});

		}
	}
}