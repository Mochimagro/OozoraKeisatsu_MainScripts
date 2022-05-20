using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using Result;

	public interface IResultPresenter
	{
		void ShowResultText();
	}

	public class ResultPresenter : IResultPresenter
	{
		private IResultView _resultView = null;
		private IResultModel _resultModel = null;
		private IScorePresenter _scorePresenter = null;

		public ResultPresenter(IResultView view,IResultModel model,
			IScorePresenter score) 
		{
			_resultView = view ?? throw new ArgumentNullException(nameof(view));
			_resultModel = model ?? throw new ArgumentNullException(nameof(model));
			_scorePresenter = score ?? throw new ArgumentNullException(nameof(score));

			_resultView.Init();
			Bind();
		}

        public void ShowResultText()
        {
			_resultView.TextReset();
			_resultView.ShowCatchCount(_scorePresenter.CatchCount);
			_resultView.ShowScore(_scorePresenter.Score);

        }

        private void Bind () 
		{
			_resultView.OnTweetButton.Subscribe(_ =>
			{
				_resultModel.OpenTweetPage(_scorePresenter.Score,_scorePresenter.CatchCount);
			});

		}
	}
}