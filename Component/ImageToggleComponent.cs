using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace OozoraKeisatsu.Game.Component
{
	public interface IImageToggleComponent
    {
		void Init(bool defaultValue);
		IObservable<bool> OnSwitch { get; }
    }


	public class ImageToggleComponent : MonoBehaviour ,IImageToggleComponent
	{

        public IObservable<bool> OnSwitch => _onSwitch;
		private Subject<bool> _onSwitch = new Subject<bool>();
		private bool _value = true;

		[SerializeField] private Image _markImage = null;
		[SerializeField] private Button _button = null;
		[SerializeField] private Sprite _onSprite = null;
		[SerializeField] private Sprite _offSprite = null;


        public void Init(bool defaultValue)
		{
			_value = defaultValue;

			_markImage.sprite = _value ? _onSprite : _offSprite;

			OnSwitch.Subscribe(value =>
			{
				_markImage.sprite = value ? _onSprite : _offSprite;
			});

			_button.OnClickAsObservable().Subscribe(_ =>
			{
				_value = !_value;
				_onSwitch.OnNext(_value);
			});

		}
		
	}
}