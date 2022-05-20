using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;
using Doozy.Engine;

namespace OozoraKeisatsu.Game.GameFlow
{
	public interface IGameFlowView
    {
		void Init();
        void SendGameEvent(string eventName);
        IObservable<Unit> OnTitle { get; }
        IObservable<Unit> OnTutorial { get; }
        IObservable<Unit> OnReadyView { get; }
        IObservable<Unit> OnMainView { get; }
        IObservable<Unit> OnFinishView { get; }
        IObservable<Unit> OnResultView { get; }

    }

    [RequireComponent(typeof(ZenjectBinding))]
	public class GameFlowView : MonoBehaviour ,IGameFlowView
	{
        public IObservable<Unit> OnTitle => _onTitle;
        private Subject<Unit> _onTitle = new Subject<Unit>();

        public IObservable<Unit> OnReadyView => _onReadyView;
        private Subject<Unit> _onReadyView = new Subject<Unit>();

        public IObservable<Unit> OnMainView => _onMainView;
        private Subject<Unit> _onMainView = new Subject<Unit>();

        public IObservable<Unit> OnFinishView => _onFinishView;
        private Subject<Unit> _onFinishView = new Subject<Unit>();

        public IObservable<Unit> OnResultView => _onResultView;

        public IObservable<Unit> OnTutorial => _onTutorial;
        private Subject<Unit> _onTutorial = new Subject<Unit>();

        private Subject<Unit> _onResultView = new Subject<Unit>();

        public void Init()
		{
			

		}

        public void OnShowTitleView()
        {
            _onTitle.OnNext(Unit.Default);
        }

        public void OnShowTutorialView()
        {
            _onTutorial.OnNext(Unit.Default);
        }

        public void OnShowReadyView()
        {
            _onReadyView.OnNext(Unit.Default);
        }

        public void OnShowFinishView()
        {
            _onFinishView.OnNext(Unit.Default);
        }

        public void OnShowMainView()
        {
            _onMainView.OnNext(Unit.Default);
        }
        public void OnShowResultView()
        {
            _onResultView.OnNext(Unit.Default);
        }

        public void SendGameEvent(string eventName)
        {
            GameEventMessage.SendEvent(eventName);
        }
    }
}