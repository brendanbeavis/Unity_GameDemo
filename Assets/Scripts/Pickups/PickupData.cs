using Assets.Scripts.Dudes;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Pickups
{
    public enum PickupType
    {
        HP_BUFF,
        HP,
        XP_BUFF,
        XP,
        CHEST,
        ARMOUR,
        SHIELD,
        SPEED_BUFF
    }

    public enum MoveType
    {
        NONE,
        MAGNET,
        MOVE
    }

    public class PickupData : MonoBehaviour
    {
        ///<summary>
        ///The 'value' is a multiplier for buffs i.e 1.05 to give a 5% buff, or a raw add/subtract value for most other picks i.e 10 for an additional 10 HP, and a 'bonus size/level' for chests i.e 2 for a level 2 chest.
        ///</summary>
        [SerializeField] private string pickupName = "";
        [TextArea][SerializeField] private string description = "";
        [SerializeField] private float pickupValue = 0;
        [SerializeField] private bool moveToDude = false;
        [SerializeField] private float moveSpeed = 0f;
        [SerializeField] private bool usable = false;
        [SerializeField] private bool expired = false;
        [SerializeField] private int minDudeLevel = 1;
        [SerializeField] private Sprite icon;
        [SerializeField] private PickupType pickupType = PickupType.XP;
        [SerializeField] private MoveType moveType = MoveType.NONE;

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        public bool MoveToDude
        {
            get { return moveToDude; }
            set { moveToDude = value; }
        }
        public Sprite Icon
        {
            get { return icon; }
        }

        public MoveType MoveType
        {
            get { return moveType; }
        }
        public PickupType PickupType
        {
            get { return pickupType; }
        }
        public float PickupValue
        {
            get { return pickupValue; }
            set { pickupValue = value; }
        }
        public bool Expired
        {
            get { return expired; }
            set { expired = value; }
        }
        public bool Usable
        {
            get { return usable; }
            set { usable = value; }
        }
        public int MinDudeLevel
        {
            get { return minDudeLevel; }
            set { minDudeLevel = value; }
        }
        
    }

}