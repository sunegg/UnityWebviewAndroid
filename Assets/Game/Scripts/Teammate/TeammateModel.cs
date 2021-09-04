using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class TeammateModel : UnitModel,ICloneable {
    public int Min;
    public int Max;
    public string AbilityTypeIdRange;
    public string AbilityRankRange;
    public int RoleID;
    public int Level;
    public static TeammateModel operator +(TeammateModel a, TeammateModel b) {
        var ab = new TeammateModel {
            Attack    = a.Attack + b.Attack,
            Defence   = a.Defence + b.Defence,
            Dominance = a.Dominance + b.Dominance,
            Speed     = a.Speed + b.Speed
        };
        return ab;
    }

    public object Clone() {
        using(Stream objectStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, this);
            objectStream.Seek(0, SeekOrigin.Begin);
            return (TeammateModel) formatter.Deserialize(objectStream);
        }
    }
}
