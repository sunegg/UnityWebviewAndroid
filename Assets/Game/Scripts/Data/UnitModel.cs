[System.Serializable]
public class UnitModel:BaseModel {
    public int Id; 
    public string RankString;

    public RankType Rank;
    
    public string Name;

    public int AbilityId;
    public int AbilityTypeId;

    public RankType AbilityRank;

    public StatusType StatusType;

    public int Siege;

}
