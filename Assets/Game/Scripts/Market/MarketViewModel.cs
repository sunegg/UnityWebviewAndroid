using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class MarketViewModel:ICloneable
{
	public int MoneyToFoodA { get; set; }
	public int MoneyToFoodB { get; set; }
	public int MoneyToPopulationA { get; set; }
	public int MoneyToPopulationB { get; set; }
	public int FoodToMoneyA { get; set; }
	public int FoodToMoneyB { get; set; }
	public int PopulationToMoneyA { get; set; }
	public int PopulationToMoneyB { get; set; }
	
	public object Clone() {
		using(Stream objectStream = new MemoryStream())
		{
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(objectStream, this);
			objectStream.Seek(0, SeekOrigin.Begin);
			return (MarketViewModel) formatter.Deserialize(objectStream);
		}
	}
}
