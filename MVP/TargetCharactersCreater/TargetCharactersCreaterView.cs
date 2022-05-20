using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace OozoraKeisatsu.Game.TargetCharactersCreater
{
	public interface ITargetCharactersCreaterView
	{
		void Init();
		IReadOnlyList<Component.ITargetCharacterComponent> Characters {get;}
		Sprite WantedImageSprite { set; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class TargetCharactersCreaterView : MonoBehaviour, ITargetCharactersCreaterView
	{
		public IReadOnlyList<Component.ITargetCharacterComponent> Characters => _targetCharacterComponents;
		[Inject(Id = "ITargetCharacterComponents")] private List<Component.ITargetCharacterComponent> _targetCharacterComponents = null;

		[SerializeField] private Image _wantedCharacterImage = null;
		public Sprite WantedImageSprite { set => _wantedCharacterImage.sprite = value; }

        public void Init()
		{
			

		}
		
	}
}