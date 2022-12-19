namespace Common.Stats
{
    public interface IStatsUI
    {
        void ReduceStat(float amount);
        void RecoverStat();
        void UpdateStatBar();
    }
}