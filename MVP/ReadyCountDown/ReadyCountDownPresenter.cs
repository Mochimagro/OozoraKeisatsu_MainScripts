using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using ReadyCountDown;

	public interface IReadyCountDownPresenter
	{
		void StartCountDown();
		IObservable<Unit> OnCompleteCountDown { get; }
	}

	public class ReadyCountDownPresenter : IReadyCountDownPresenter
	{
		private IReadyCountDownView _readyCountDownView = null;
		private IReadyCountDownModel _readyCountDownModel = null;
		private ISoundPresenter _soundPresenter = null;

		public ReadyCountDownPresenter(
			IReadyCountDownView view,IReadyCountDownModel model,
			ISoundPresenter sound) 
		{
			_readyCountDownView = view ?? throw new ArgumentNullException(nameof(view));
			_readyCountDownModel = model ?? throw new ArgumentNullException(nameof(model));

			_soundPresenter = sound ?? throw new ArgumentNullException(nameof(sound));

			_readyCountDownView.Init();
			Bind();
		}

        public IObservable<Unit> OnCompleteCountDown => _readyCountDownView.OnCompleteCountDown;

        public void StartCountDown() => _readyCountDownView.StartCountDown();

        private void Bind () 
		{
			_readyCountDownView.OnCountDown.Subscribe(_ =>
			{
				_soundPresenter.PlaySound("CountDown");
			});

		}
	}
}