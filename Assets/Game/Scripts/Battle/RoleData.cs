using System;

[Serializable]
    public class RoleData
    {

        public int Damage;
        public int Health;
        public int MaxHealth;

        public SkillData AttackData;

        public Buffs Buffs;

    }

    [Flags][Serializable]
    public enum Buffs { None,Fierce}

    public enum RoleType {Player,Enemy }
