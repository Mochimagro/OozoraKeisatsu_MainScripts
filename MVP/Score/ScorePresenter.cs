using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using Score;

	public interface IScorePresenter
	{
		void ResetScore();
		void AddScore(int value);
		void AddWantedScore();
		void ResetComboCount();
		void AddCatchCount();
		void ResetCatchCount();
		int Score { get; }
		int CatchCount { get; }
	}

	public class ScorePresenter : IScorePresenter
	{
		private IScoreView _scoreView = null;
		private IScoreModel _scoreModel = null;


        public ScorePresenter(IScoreView view,IScoreModel model) 
		{
			_scoreView = view ?? throw new ArgumentNullException(nameof(view));
			_scoreModel = model ?? throw new ArgumentNullException(nameof(model));
			

			Bind();
		}
		
		private void Bind () 
		{
			_scoreModel.OnScore.Subscribe(value =>
			{
				_scoreView.ScoreText = value;
			});

			_scoreModel.OnComboCount.Subscribe(value =>
			{
				_scoreView.ComboText = value;
			});

		}
		public int Score => _scoreModel.Score;

        public int CatchCount => _scoreModel.CatchCount;

        public void AddScore(int value) => _scoreModel.AddScore(value);
		public void ResetScore() => _scoreModel.ResetScore();

        public void AddWantedScore() => _scoreModel.AddWantedScore();

		public void ResetComboCount() => _scoreModel.ResetWantedCombo();

		public void AddCatchCount() => _scoreModel.AddCatchCount();

		public void ResetCatchCount() => _scoreModel.ResetCatchCount();
        
    }
}