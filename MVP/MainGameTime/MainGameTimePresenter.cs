using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using MainGameTime;

	public interface IMainGameTimePresenter
	{
		void StartTimer();


		IObservable <Unit> OnTimeOver { get; }
	}

	public class MainGameTimePresenter : IMainGameTimePresenter
	{
		private IMainGameTimeView _mainGameTimeView = null;
		private IMainGameTimeModel _mainGameTimeModel = null;

		public MainGameTimePresenter(IMainGameTimeView view,IMainGameTimeModel model) 
		{
			_mainGameTimeView = view ?? throw new ArgumentNullException(nameof(view));
			_mainGameTimeModel = model ?? throw new ArgumentNullException(nameof(model));

			_mainGameTimeView.Init(_mainGameTimeModel.Duration);
			Bind();
		}

		public IObservable<Unit> OnTimeOver => _mainGameTimeModel.OnTimeOver;

        private void Bind () 
		{

			_mainGameTimeModel.OnTimer.Subscribe(value =>
			{
				_mainGameTimeView.TimerTextCount = value;
			});

		}

		public void StartTimer() => _mainGameTimeModel.InvokeCountDown();

    }
}