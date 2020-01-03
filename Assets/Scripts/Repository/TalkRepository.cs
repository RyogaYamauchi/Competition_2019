using System.Collections.Generic;
using System.IO;
using Scripts.Models;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Assertions;


namespace Repository
{
    public static class TalkRepository
    {
        public static async UniTask<TalkModel[]> LoadTalk(string path)
        {
            List<TalkModel> talk = new List<TalkModel>();
            int cnt = 0;
            var csvFile = await Resources.LoadAsync("Talk/" + path) as TextAsset;
            Assert.IsNotNull(csvFile);
            StringReader reader = new StringReader(csvFile.text);
            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                var a = line.Split(',');
                //TODO : CharacterReposotoryから持ってくる
                var characterName = "棒人間さん";
                var model = new TalkModel(int.Parse(a[0]), characterName, a[1]);
                talk.Add(model);
                cnt++;
            }

            return talk.ToArray();
        }
    }
}