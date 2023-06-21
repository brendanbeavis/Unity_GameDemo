using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;


namespace Assets.Scripts.Pickups
{
    internal class Token
    {
        public int xpValue = 1;
        public bool alive = true;
        public bool moveToDude = false;
        public eTokenType tokenType = eTokenType.xp;
        public Token() { }
        public Token(int _xpValue, eTokenType _tokenType)
        {
            xpValue = _xpValue;
            tokenType = _tokenType;
        }
    }

    enum eTokenType { xp, health, mana, pickup }
}
