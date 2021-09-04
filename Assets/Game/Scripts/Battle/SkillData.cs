using System;

[Serializable]
    public struct SkillData {
        public int HealthAffect;
        public int EnergyCost;
        public int DamageAffect;
        public Buffs BuffAffect;

    }

    public enum SkillType { Idle,Attack,Defend,Potion,Hinder,Crazy,Alert,Fierce}
