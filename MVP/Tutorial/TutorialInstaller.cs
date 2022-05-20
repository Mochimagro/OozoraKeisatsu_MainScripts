using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class TutorialInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<Tutorial.TutorialModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.TutorialPresenter>().AsCached().NonLazy();
		}
	}
}