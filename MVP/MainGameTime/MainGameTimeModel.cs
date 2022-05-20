using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.MainGameTime
{
	public interface IMainGameTimeModel
    {
		IObservable<int> OnTimer { get; }
		IObservable<Unit> OnTimeOver { get; }
		int Duration { get; }
		void InvokeCountDown();
    }


	public class MainGameTimeModel : IMainGameTimeModel
	{
        public int Duration => _duration;
		private int _duration = 30;

        public IObservable<int> OnTimer => _timer;
        private IntReactiveProperty _timer = new IntReactiveProperty(-1);
        public IObservable<Unit> OnTimeOver => _onTimeOver;
		private Subject<Unit> _onTimeOver = new Subject<Unit>();

		public MainGameTimeModel()
		{
			

		}

        public void InvokeCountDown()
        {
			_timer.Value = _duration + 1;

			var interval = Observable
				.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
				.TakeWhile(x => _timer.Value > 0);

			interval.Subscribe(x =>
			{
				_timer.Value--;
			},
			() =>
			{
				_timer.Value = 0;
				_onTimeOver.OnNext(Unit.Default);
			});


		}
    }
}