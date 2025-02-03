using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Singleton<T>
        where T : class , new()
    {
        private static T instance;

        public static T Instance
        {
            get 
            {
                if (instance == null)
                    instance = new T();

                return instance;
            }
        }
        
    }
}
