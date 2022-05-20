using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;
using Doozy.Engine;

namespace OozoraKeisatsu.Game.MainGameTime
{
	public interface IMainGameTimeView
    {
		void Init(int startValue);
		int TimerTextCount { set; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class MainGameTimeView : MonoBehaviour ,IMainGameTimeView
	{
		[SerializeField] private TextMeshProUGUI _timerText = null;


        public int TimerTextCount { set => _timerText.text = $"{value}"; }

        public void Init(int startValue)
		{
			TimerTextCount = startValue;

		}
	}
}