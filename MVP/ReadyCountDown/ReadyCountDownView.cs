using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using Doozy.Engine;

namespace OozoraKeisatsu.Game.ReadyCountDown
{
	public interface IReadyCountDownView
    {
		void Init();
		void StartCountDown();
		IObservable<Unit> OnCountDown { get; }
		IObservable<Unit> OnCompleteCountDown { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class ReadyCountDownView : MonoBehaviour ,IReadyCountDownView
	{
		[SerializeField] private TextMeshProUGUI _countDownText = null;

        public IObservable<Unit> OnCompleteCountDown => _onCompleteCountDown;
        private Subject<Unit> _onCompleteCountDown = new Subject<Unit>();

        public IObservable<Unit> OnCountDown => _onCountDown;
		private Subject<Unit> _onCountDown = new Subject<Unit> ();


        public void Init()
		{
			_countDownText.text = $"{3}";

		}

        public void StartCountDown()
		{
			_countDownText.text = $"{3}";

			var interval = Observable
				.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
				.Select(x => (int)(3 - x))
				.TakeWhile(x => x > 0);

			interval.Subscribe(x =>
			{
				_countDownText.text = $"{x}";
				_onCountDown.OnNext(Unit.Default);
				
            }, () =>
			{
				_countDownText.text = "START";
				_onCompleteCountDown.OnNext(Unit.Default);
			});


        }
    }
}