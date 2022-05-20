using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Score
{
	public interface IScoreModel
    {
		IObservable<int> OnScore { get; }
        IObservable<int> OnComboCount { get; }
		int Score { get; }
        int CatchCount { get; }
		void AddScore(int value);
		void AddWantedScore();
        void ResetWantedCombo();
		void ResetScore();
        void AddCatchCount();
        void ResetCatchCount();
    }


	public class ScoreModel : IScoreModel
	{
        public IObservable<int> OnScore => _score;
        public int Score => _score.Value;
        private IntReactiveProperty _score = new IntReactiveProperty();

        public IObservable<int> OnComboCount => _combo;
        private IntReactiveProperty _combo = new IntReactiveProperty();

        public IObservable<int> CatchedCount => _catchedCount;
        public int CatchCount => _catchedCount.Value;
        private IntReactiveProperty _catchedCount = new IntReactiveProperty();


		public ScoreModel()
		{
			

		}

        public void AddScore(int value)
        {
            _score.Value += value;
        }

        public void ResetScore()
        {
			_score.Value = 0;
		
		}

        public void AddWantedScore()
        {
            _combo.Value++;
            AddScore((int)Mathf.Pow(_combo.Value, 2) * 100);
        }

        public void ResetWantedCombo()
        {
            _combo.Value = 0;
        }

        public void AddCatchCount()
        {
            _catchedCount.Value++;
        }

        public void ResetCatchCount()
        {
            _catchedCount.Value = 0;
        }
    }
}