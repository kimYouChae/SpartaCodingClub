using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    abstract class Singleton<T>
        where T : class , new() // new 제약조건이 있어야 new T() 할 수 있음 
    {
        private static T instance;

        public static T Instance
        {
            get 
            {
                // null일때만 새로 생성
                if (instance == null)
                    instance = new T();

                return instance;
            }
        }   
    }
}
