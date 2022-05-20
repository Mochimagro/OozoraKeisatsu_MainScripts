using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class ScoreInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<Score.ScoreModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.ScorePresenter>().AsCached().NonLazy();
		}
	}
}