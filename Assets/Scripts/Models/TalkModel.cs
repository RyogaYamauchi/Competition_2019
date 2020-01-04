using UnityEngine;

namespace Scripts.Models
{
    public readonly struct TalkModel
    {
        public int SpriteId { get; }
        public string MainText { get; }
        public string CharacterName { get; }

        public TalkModel(int spriteId, string characterName, string mainText)
        {
            SpriteId = spriteId;
            CharacterName = characterName;
            MainText = mainText;
        }
    }
}