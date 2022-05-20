using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Arbor.BehaviourTree;
using DG.Tweening;

namespace OozoraKeisatsu.Game.Component
{
	public interface ITargetCharacterComponent
    {
		void Init();
		void Catched();
		void EntryField();
		void RemoveField();
		void EscapeField();
		IObservable<ITargetCharacterComponent> OnCatched { get; }
		IObservable<ITargetCharacterComponent> OnEndAnimation { get; }
		IObservable<ITargetCharacterComponent> OnEscape { get; }
		string CharacterName { get; }
		Sprite CharacterIcon { get; }
		Vector3 Position { get; }
    }


	public class TargetCharacterComponent : MonoBehaviour ,ITargetCharacterComponent
	{
		[SerializeField]private Transform _characterTransform = null;

		[SerializeField] private Animator _characterAnimator = null;

        public Sprite CharacterIcon => _characterIcon;
		[SerializeField] private Sprite _characterIcon = null;

		public string CharacterName => _characterName;
		[SerializeField] private string _characterName = default;

		[SerializeField] private Collider _catchedTriggerArea = null;

        public bool ObjectActive { set => this.gameObject.SetActive(value); }

		[SerializeField] BehaviourTree _behaviourTree = null;

		public IObservable<ITargetCharacterComponent> OnCatched => _onCatched;
        private Subject<ITargetCharacterComponent> _onCatched = new Subject<ITargetCharacterComponent>();

		public IObservable<ITargetCharacterComponent> OnEscape =>_onEscaped;

        public IObservable<ITargetCharacterComponent> OnEndAnimation => _onEndAnimation;

        public Vector3 Position => _characterTransform.position;

        private Subject<ITargetCharacterComponent> _onEndAnimation = new Subject<ITargetCharacterComponent>();

        private Subject<ITargetCharacterComponent> _onEscaped = new Subject<ITargetCharacterComponent>();

        public void Catched()
        {
			_catchedTriggerArea.enabled = false;

			_characterAnimator.SetTrigger("Death");

			_characterTransform.DOScale(0, 0.5f).SetEase(Ease.Linear).SetDelay(0.5f).OnComplete(()=>_onEndAnimation.OnNext(this));

			_onCatched.OnNext(this);
        }

        public void Init () 
		{
			_catchedTriggerArea.enabled = false;

		}

		public void EntryField()
        {
			this.transform.position = new Vector3(UnityEngine.Random.Range(-10f, 10f), 0, UnityEngine.Random.Range(-10, 10));
			_characterTransform.localPosition = Vector3.up * 30;

			_characterTransform.DOScale(1, 0);

			_characterTransform.DOLocalMoveY(0, 0.75f)
				.SetEase(Ease.Linear)
				.OnStart(
				() =>
			{
				ObjectActive = true;
				_characterAnimator.SetTrigger("Jumping");
			}).OnComplete(() =>
			{
				_behaviourTree.Play();
				_catchedTriggerArea.enabled = true;
				_characterAnimator.SetTrigger("Granding");
			});
        }
		
		public void RemoveField()
        {
			ObjectActive = false;
			_behaviourTree.Stop();
        }

        public void EscapeField()
        {
			_characterTransform.DOLocalMoveY(30, 0.85f)
				.SetEase(Ease.Linear)
				.OnStart(() =>
				{
					_characterAnimator.SetTrigger("ToJump");
				})
				.OnComplete(() =>
				{
					_onEscaped.OnNext(this);
				});
        }
    }

}