using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using System.Runtime.InteropServices;

namespace OozoraKeisatsu.Game.Result
{
	public interface IResultModel
    {
		void OpenTweetPage(int score, int count);

    }


	public class ResultModel : IResultModel
	{
		[DllImport("__Internal")] static extern bool reload();
		[DllImport("__Internal")] private static extern string TweetFromUnity(string rawMessage);
		[DllImport("__Internal")] private static extern string openURL(string rawURL);

        public ResultModel()
		{

		}
		public void OpenTweetPage(int score,int count)
		{
			var message = $"大空警察業務報告！極悪ホロメン{count}人確保！スコアは{score}%0a" +
				$"ゲームプレイはこちら↓%0a"
				+ "%0a" + "https://mochimagro.github.io/OozoraKeisatsu/"
				+ "%0a%23" + "大空スバル" + "%0a%23" + "大空警察24時";

			TweetFromUnity(message);
		}


	}
}