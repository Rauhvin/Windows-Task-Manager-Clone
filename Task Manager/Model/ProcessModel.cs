using System.Diagnostics;

namespace Task_Manager.Model
{
    public class ProcessModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long WorkingSet { get; set; }

        public double MemoryUsage => Math.Round(WorkingSet / 1024.0 / 1024.0, 2);
    }
}
