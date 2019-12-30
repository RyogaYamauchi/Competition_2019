using System.Collections.Generic;
using Scripts.Models;
using UnityEngine;

namespace Repository
{
    public static class AnimationRepository
    {
        private static readonly Dictionary<AnimationEnum, Sprite[]> _spriteDictionary =
            new Dictionary<AnimationEnum, Sprite[]>();

        static AnimationRepository()
        {
            var attackWeaponSprites1 = CreateSprites("Sprites/Player/Attack1/Weapon");
            var attackWeaponSprites2 = CreateSprites("Sprites/Player/Attack2/Weapon");
            var attackWeaponSprites3 = CreateSprites("Sprites/Player/Attack3/Weapon");
            

            var attackPlayerSprites1 = CreateSprites("Sprites/Player/Attack1/Player");
            var attackPlayerSprites2 = CreateSprites("Sprites/Player/Attack2/Player");
            var attackPlayerSprites3 = CreateSprites("Sprites/Player/Attack3/Player");

            var standingSprites = CreateSprites("Sprites/Player/Standing");

            _spriteDictionary.Add(AnimationEnum.WeaponAttack1,attackWeaponSprites1);
            _spriteDictionary.Add(AnimationEnum.WeaponAttack2, attackWeaponSprites2);
            _spriteDictionary.Add(AnimationEnum.WeaponAttack3, attackWeaponSprites3);


            _spriteDictionary.Add(AnimationEnum.PlayerAttack1, attackPlayerSprites1);
            _spriteDictionary.Add(AnimationEnum.PlayerAttack2, attackPlayerSprites2);
            _spriteDictionary.Add(AnimationEnum.PlayerAttack3, attackPlayerSprites3);
            
            
            _spriteDictionary.Add(AnimationEnum.PlayerIdling, standingSprites);
        }

        private static Sprite[] CreateSprites(string path)
        {
            var cnt = Resources.LoadAll<Sprite>(path).Length;

            List<Sprite> sprites = new List<Sprite>();
            for (int i = 1; i < cnt+1; i++)
            {
                sprites.Add(Resources.Load<Sprite>($"{path}/{i}"));
            }

            return sprites.ToArray();
        }

        public static Sprite[] GetSprites(AnimationEnum animationEnum)
        {
            return _spriteDictionary[animationEnum];
        }
    }
}