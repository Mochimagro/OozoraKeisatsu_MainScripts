using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class JoyStickInputInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<JoyStickInput.JoyStickInputModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.JoyStickInputPresenter>().AsCached().NonLazy();
		}
	}
}