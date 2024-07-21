using System.Collections.Generic;

public class PlayerStats
{
    private List<PlayerStat> _stats;
    public List<PlayerStat> Stats 
    {
        get { return _stats; }
        set { _stats = value; }
    }

    public PlayerStats(StartStatsSO startStats)
    {
        _stats = new List<PlayerStat>();

        foreach(StartStat stat in startStats.Stats)
        {
            _stats.Add(new PlayerStat(stat));
        }
    }
}
