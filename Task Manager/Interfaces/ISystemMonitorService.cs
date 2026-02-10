namespace Task_Manager.Interfaces
{
    public interface ISystemMonitorService
    {
        double GetUsage();
        string Name { get; set; }
        Dictionary<string, string> Info { get;}
    }
}
