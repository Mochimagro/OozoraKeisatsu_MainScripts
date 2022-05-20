using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace OozoraKeisatsu.Game.Tutorial
{
	public interface ITutorialView
    {
		void Init();
		Sprite ImageSprite { set; }
		bool IsPrevButton { set; }
		bool IsNextButton { set; }
		IObservable<Unit> OnPrevButton { get; }
		IObservable<Unit> OnNextButton { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class TutorialView : MonoBehaviour ,ITutorialView
	{
		[SerializeField] private Image _image = null;
		[SerializeField] private Button _nextButton = null;
		[SerializeField] private Button _prevButton = null;

        public Sprite ImageSprite { set => _image.sprite = value; }
        public bool IsPrevButton { set => _prevButton.gameObject.SetActive(value); }
        public bool IsNextButton { set => _nextButton.gameObject.SetActive(value); }

        public IObservable<Unit> OnPrevButton => _prevButton.OnClickAsObservable();

        public IObservable<Unit> OnNextButton => _nextButton.OnClickAsObservable();

        public void Init()
        {

        }
    }
}