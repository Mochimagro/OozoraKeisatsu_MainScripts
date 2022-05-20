using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Tutorial
{
	public interface ITutorialModel
    {
		void ChangePage(int value);
		void Reset();
		IObservable<bool> OnMinPage { get; }
		IObservable <bool> OnMaxPage { get; }

		IReadOnlyList<Sprite> Sprites { get; }
		IObservable<Sprite> OnSprite { get; }

    }


	public class TutorialModel : ITutorialModel
	{
        public IReadOnlyList<Sprite> Sprites => _sprites;

        public IObservable<Sprite> OnSprite => _onSprites;

        public IObservable<bool> OnMinPage => _onMinPage;
		private Subject<bool> _onMinPage = new Subject<bool>();
        public IObservable<bool> OnMaxPage => _onMaxPage;
		private Subject<bool> _onMaxPage = new Subject<bool>();

        private Subject<Sprite> _onSprites = new Subject<Sprite>();

        private List<Sprite> _sprites = new List<Sprite>();

		private int _page = 0;

		public TutorialModel(Data.ITutorialSpritesData data)
		{
			foreach(var sprite in data.Sprites)
            {
				_sprites.Add(sprite);
            }
		}

        public void ChangePage(int value)
        {
			_page += value;

			_onSprites.OnNext(_sprites[_page]);
			_onMinPage.OnNext(_page != 0);
			_onMaxPage.OnNext(_page != _sprites.Count - 1);

		}

		public void Reset()
        {
			_page = 0;
			ChangePage(0);
        }

    }
}