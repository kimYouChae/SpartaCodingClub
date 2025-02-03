using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal interface IScene
    {
        // 씬에 들어갔을 때 
        public void SceneEntry();

        // 씬에서 메인으로 동작할 로직
        public void SceneMainFlow();

        // 씬 끝날 때
        public void SceneExit();

    }
}
