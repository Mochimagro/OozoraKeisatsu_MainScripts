using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using GameFlow;

	public interface IGameFlowPresenter
	{
		IObservable<Unit> OnGameStart { get; }
		IObservable<Unit> OnGameFinish { get; }

	}

	public class GameFlowPresenter : IGameFlowPresenter
	{
		private IGameFlowView _gameFlowView = null;
		private IGameFlowModel _gameFlowModel = null;
		private IReadyCountDownPresenter _readyCountDownPresenter = null;
		private IMainGameTimePresenter _mainGameTimePresenter = null;
		private IMainPlayerCharacterPresenter _mainPlayerCharacterPresenter = null;
		private IJoyStickInputPresenter _joyStickInputPresenter = null;
		private ITargetCharactersCreaterPresenter _targetCharactersCreaterPresenter = null;
		private IScorePresenter _scorePresenter = null;
		private IResultPresenter _resultPresenter = null;
		private ISoundPresenter _soundPresenter = null;
		private ITutorialPresenter _tutorialPresenter = null;

		public GameFlowPresenter(IGameFlowView view,IGameFlowModel model,
			IMainGameTimePresenter mainGameTime,
			IReadyCountDownPresenter readyCountDown,
			IMainPlayerCharacterPresenter mainPlayer,
			IJoyStickInputPresenter joyStickInput,
			ITargetCharactersCreaterPresenter targetCharactersCreater,
			IScorePresenter score,
			IResultPresenter result,
			ISoundPresenter sound,
			ITutorialPresenter tutorial) 
		{
			_gameFlowView = view ?? throw new ArgumentNullException(nameof(view));
			_gameFlowModel = model ?? throw new ArgumentNullException(nameof(model));
			
			_mainGameTimePresenter = mainGameTime ?? throw new ArgumentNullException(nameof(mainGameTime));
			_readyCountDownPresenter = readyCountDown ?? throw new ArgumentNullException(nameof(readyCountDown));
			_mainPlayerCharacterPresenter = mainPlayer ?? throw new ArgumentNullException(nameof(mainPlayer));
			_joyStickInputPresenter= joyStickInput ?? throw new ArgumentNullException(nameof(joyStickInput));
			_targetCharactersCreaterPresenter= targetCharactersCreater ?? throw new ArgumentNullException(nameof(targetCharactersCreater));	
			_scorePresenter = score ?? throw new ArgumentNullException(nameof(score));
			_resultPresenter = result ?? throw new ArgumentNullException(nameof(result));
			_soundPresenter	= sound ?? throw new ArgumentNullException(nameof(sound));
			_tutorialPresenter = tutorial ?? throw new ArgumentNullException(nameof(tutorial));

			_gameFlowView.Init();
			Bind();
		}

        public IObservable<Unit> OnGameStart => _gameFlowModel.OnGameStart;

        public IObservable<Unit> OnGameFinish => _gameFlowModel.OnGameFinish;

        private void Bind () 
		{
			_gameFlowView.OnTitle.Subscribe(_ =>
			{
				_soundPresenter.SetDefault();
				_soundPresenter.PlaySound("Title");
			});

			_gameFlowView.OnTutorial.Subscribe(_ =>
			{
				_tutorialPresenter.Reset();
			});

			_gameFlowView.OnReadyView.Subscribe(_ =>
			{
				_soundPresenter.StopBGM();
				_gameFlowModel.GameReady();
				_mainPlayerCharacterPresenter.Ready();
				_readyCountDownPresenter.StartCountDown();
				_targetCharactersCreaterPresenter.ReadyCreate(3);
			});

			_readyCountDownPresenter.OnCompleteCountDown.Subscribe(_ =>
			{
				_gameFlowView.SendGameEvent("ToMainView");
			});


			_gameFlowView.OnMainView.Subscribe(_ =>
			{
				_soundPresenter.PlaySound("MainGame");
				_gameFlowModel.GameStart();
				_mainGameTimePresenter.StartTimer();
				_joyStickInputPresenter.StartControll();
				_scorePresenter.ResetScore();
				_scorePresenter.ResetComboCount();
				_scorePresenter.ResetCatchCount();
			});

			_mainGameTimePresenter.OnTimeOver.Subscribe(_ =>
			{
				_soundPresenter.StopBGM();
				_soundPresenter.PlaySound("Finish");
				_gameFlowView.SendGameEvent("ToFinish");
				_joyStickInputPresenter.StopControll();
				_targetCharactersCreaterPresenter.AllEscape();
			});

			_gameFlowView.OnFinishView.Subscribe(_ =>
			{
				_gameFlowModel.GameFinish();
			});

			_gameFlowView.OnResultView.Subscribe(_ =>
			{
				// _soundPresenter.PlaySound("Result");
				_resultPresenter.ShowResultText();
			});

		}
	}
}