using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using Tutorial;

	public interface ITutorialPresenter
	{
		void Reset();
	}

	public class TutorialPresenter : ITutorialPresenter
	{
		private ITutorialView _tutorialView = null;
		private ITutorialModel _tutorialModel = null;

		public TutorialPresenter(ITutorialView view,ITutorialModel model) 
		{
			_tutorialView = view ?? throw new ArgumentNullException(nameof(view));
			_tutorialModel = model ?? throw new ArgumentNullException(nameof(model));

			_tutorialView.Init();
			Bind();

		}

        public void Reset() => _tutorialModel.Reset();

		private void Bind () 
		{
			_tutorialModel.OnMinPage.Subscribe(value =>
			{
				_tutorialView.IsPrevButton = value;
			});

			_tutorialModel.OnMaxPage.Subscribe(value =>
			{
				_tutorialView.IsNextButton = value;
			});

			_tutorialModel.OnSprite.Subscribe(value =>
			{
				_tutorialView.ImageSprite = value;
			});

			_tutorialView.OnPrevButton.Subscribe(_ =>
			{
				_tutorialModel.ChangePage(-1);
			});

			_tutorialView.OnNextButton.Subscribe(_ =>
			{
				_tutorialModel.ChangePage(+1);
			});

		}
	}
}