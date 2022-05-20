using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace OozoraKeisatsu.Game.Score
{
	public interface IScoreView
    {
		void Init();
		int ScoreText { set; }
		int ComboText { set; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class ScoreView : MonoBehaviour ,IScoreView
	{

		[SerializeField] private TextMeshProUGUI _scoreText = null;
		[SerializeField] private TextMeshProUGUI _comboText = null;

		int _beforeScore = 0;

		Sequence _scoreSeq = null;

		Sequence _comboSeq = null;

		public void Init()
		{
		}

		public int ScoreText { 
			set
			{
				if(value == 0)
                {
					_scoreText.text = $"{0}";
					return;
                }

				_scoreSeq?.Complete();

				_scoreSeq = DOTween.Sequence();

				_scoreSeq.Append(
					_scoreText.DOCounter(_beforeScore,value,0.35f).SetEase(Ease.Linear));

				_beforeScore = value;
			}
		}
        public int ComboText { set
			{
                if (value > 1)
                {
					_comboSeq?.Complete();

					_comboSeq = DOTween.Sequence();
					
					_comboText.text = $"{value}Combo";

					_comboSeq.Append(_comboText.transform.DOScale(1.75f, 0));
					_comboSeq.Append(
						_comboText.DOScale(1.0f,0.35f).SetEase(Ease.OutExpo));

                }
                else
                {
					_comboText.text = string.Empty;
                }
			}
		}
    }
}