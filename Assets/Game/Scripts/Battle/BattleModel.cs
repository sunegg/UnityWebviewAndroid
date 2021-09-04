using System.Collections.Generic;

public class BattleModel
{
    public int Round=1;

    public List<TeammateModel> Teammates=new List<TeammateModel>();

    public PlayerData Player;

    public List<TeammateModel> Enemies =new List<TeammateModel>();
    
    public string EnemyInfo, PlayerInfo;
    
    public int CurrentTeamDominance,CurrentEnemyDominance;
    
    public int CurrentMaxTeamDominance, CurrentMaxEnemyDominance;
	
    public TeammateModel TeamTotal, EnemyTotal;

    public int Troop;

    public TeammateModel SelectedTroop;
    
    public TeammateModel EnemyTroop;
    
    public int ActualDominance;

    public int SelectedEnemy;
}
