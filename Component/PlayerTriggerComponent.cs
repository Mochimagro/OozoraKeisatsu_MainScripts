using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace OozoraKeisatsu.Game.Component
{
	public interface IPlayerTriggerComponent
    {
		void Init();
		IObservable <ITargetCharacterComponent> OnCatchTarget { get; }
    }


	public class PlayerTriggerComponent : MonoBehaviour ,IPlayerTriggerComponent
	{

		public IObservable<ITargetCharacterComponent> OnCatchTarget => _onCatchTarget;
		private Subject<ITargetCharacterComponent> _onCatchTarget = new Subject<ITargetCharacterComponent>();

		public void Init () 
		{

			this.OnTriggerEnterAsObservable().Where(col => col.CompareTag("Target")).Subscribe(target =>
			{
				var chara = target.GetComponent<ITargetCharacterComponent>();
				_onCatchTarget.OnNext(chara);

			});
		}
		
	}
}