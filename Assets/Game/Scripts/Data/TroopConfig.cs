using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class TroopConfig : UnitModel,ICloneable {
  public float FoodCost;

  public object Clone() {
    using(Stream objectStream = new MemoryStream())
    {
      IFormatter formatter = new BinaryFormatter();
      formatter.Serialize(objectStream, this);
      objectStream.Seek(0, SeekOrigin.Begin);
      return (TroopConfig) formatter.Deserialize(objectStream);
    }
  }
}
