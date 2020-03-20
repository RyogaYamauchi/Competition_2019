using Models;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public static void プレイヤーはHpが０になると死ぬ()
        {
            var model = new PlayerModel(1, 1);
            model.SubHp(10);
            Assert.AreEqual(true, model.IsDead);
        }

        [Test]
        public static void プレイヤーのHpは０より小さくならない()
        {
            var model = new PlayerModel(1, 1);
            model.SubHp(10);
            Debug.Log(model.Hp.Value);
            Assert.IsTrue(0 == model.Hp.Value);
            model = new PlayerModel(-10, 1);
            Assert.IsTrue(0 == model.Hp.Value);
        }
    }
}