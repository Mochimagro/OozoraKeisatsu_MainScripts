using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class ResultInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<Result.ResultModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.ResultPresenter>().AsCached().NonLazy();
		}
	}
}