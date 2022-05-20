using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace OozoraKeisatsu.Game.Result
{
	public interface IResultView
    {
		void Init();
		void TextReset();
		void ShowScore(int score);
		void ShowCatchCount(int count);
		int ScoreText { set; }
		string RankText { set; }
		IObservable<Unit> OnTweetButton { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class ResultView : MonoBehaviour ,IResultView
	{
		[SerializeField] private TextMeshProUGUI _resultScoreText = null;
		[SerializeField] private TextMeshProUGUI _resultCatchCountText = null;
		[SerializeField] private TextMeshProUGUI _rankText = null;

		[SerializeField] private Button _tweetButton = null;

		private Sequence _resultShowSeq = null;

        public int ScoreText { set => _resultScoreText.text = $"{value}"; }
        public string RankText { set => _rankText.text = $"{value}"; }

        public IObservable<Unit> OnTweetButton => _tweetButton.OnClickAsObservable();

        public void Init()
		{
			_resultScoreText.text = $"0";

		}

        public void TextReset()
        {
			_resultShowSeq?.Complete();
			_resultShowSeq = DOTween.Sequence();
        }

        public void ShowCatchCount(int count)
        {
			_resultCatchCountText.text = $"0";

			_resultShowSeq.Append(_resultCatchCountText.DOCounter(0, count, 0.55f).SetDelay(0.5f).SetEase(Ease.Linear));

			
        }

        public void ShowScore(int score)
		{
			_resultScoreText.text = $"0";

			_resultShowSeq.Append(_resultScoreText.DOCounter(0, score, 0.55f).SetDelay(0.5f).SetEase(Ease.Linear));

        }


	}
}