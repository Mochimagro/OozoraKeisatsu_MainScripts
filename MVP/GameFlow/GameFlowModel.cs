using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.GameFlow
{
	public interface IGameFlowModel
    {
        void GameReady();
		void GameStart();
		void GameFinish();
        IObservable<Unit> OnGameReady { get; }
		IObservable<Unit> OnGameStart { get; }
		IObservable<Unit> OnGameFinish { get; }
    }


	public class GameFlowModel : IGameFlowModel
	{
		public GameFlowModel()
		{
			

		}

        public IObservable<Unit> OnGameStart => _onGameStart;
        private Subject<Unit> _onGameStart = new Subject<Unit>();

        public IObservable<Unit> OnGameFinish => _onGameFinish;

        public IObservable<Unit> OnGameReady => _onGameReady;
        private Subject<Unit> _onGameReady = new Subject<Unit> ();

        private Subject<Unit> _onGameFinish = new Subject<Unit>();

        public void GameReady()
        {
            _onGameReady.OnNext(Unit.Default);
        }

        public void GameFinish()
        {
            _onGameFinish.OnNext(Unit.Default);
        }

        public void GameStart()
        {
            _onGameStart.OnNext(Unit.Default);
        }
    }
}