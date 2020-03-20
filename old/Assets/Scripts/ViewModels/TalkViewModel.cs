using Framework;
using Scripts.Models;

namespace ViewModels
{
    public class TalkViewModel : IViewModel
    {
        public int CharacterID { get; }
        public string MainText { get; }
        public string CharacterName { get; }

        public TalkViewModel(int characterId, string characterName, string mainText)
        {
            CharacterID = characterId;
            CharacterName = characterName;
            MainText = mainText;
        }

        public TalkViewModel(TalkModel talkModel)
        {
            CharacterID = talkModel.SpriteId;
            MainText = talkModel.MainText;
            CharacterName = talkModel.CharacterName;
        }
    }
}
