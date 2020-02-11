using System.Collections.Generic;

namespace Scripts.Models
{
    public class GameProgressModel
    {
        // TODO: チュートリアル以外のModelもできるようにリファクタリングする
        private List<int> _passingPointList = new List<int>();

        public void PassingPoint(int id)
        {
            _passingPointList.Add(id);
            switch (id)
            {
                case 1:
                    // 歩けた
                    
                    break;
                case 2:
                    // ジャンプできた
                    // TODO : 敵をスポーンさせる(spawn point id : 1)
                    break;
                case 3:
                    // 攻撃できた
                    // TODO : 敵をスポーンさせる(spawn point id : 2)
                    break;
                case 4:
                    // 必殺技を使えた
                    break;
                case 5: 
                    // クリアできた
                    // TODO : ゴール演出とタイトル画面に戻る
                    break;
            }
        }
    }
}