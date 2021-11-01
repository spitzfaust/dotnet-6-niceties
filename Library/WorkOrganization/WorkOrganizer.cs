// Look at me - no braces
namespace WorkOrganization;

public class WorkOrganizer
{
    private readonly object backlogLock = new();
    public enum WorkCategory
    {
        None,
        Implementation,
        Testing,
        Documentation,
        Research
    }

    public record WorkItem(int Id, WorkCategory Category);

    // Enabling implicit usings in the csproj is the same as adding the following to each file
    // using System;
    // using System.IO;
    // using System.Collections.Generic;
    // using System.Linq;
    // using System.Net.Http;
    // using System.Threading;
    // using System.Threading.Tasks;
    // This is why for example System.Collections.Generic is not imported in this file
    private readonly List<WorkItem> backlog = new List<WorkItem>() {
        new(1, WorkCategory.Implementation),
        new(2, WorkCategory.Testing),
        new(3, WorkCategory.Implementation),
        new(4, WorkCategory.Implementation),
        new(5, WorkCategory.Documentation),
        new(6, WorkCategory.Documentation),
        new(7, WorkCategory.Research),
        new(8, WorkCategory.Testing),
        new(9, WorkCategory.Research),
        new(10, WorkCategory.Documentation),
    };

    // You can now use the Chunk method to get chunks of a sequence
    public IEnumerable<WorkItem[]> GetWorkForSprints() => backlog.Chunk(3);

    // You do not need the GroupBy(x => x.Prop).Select(g => g.First()) anymore but instead use DistinctBy
    public IEnumerable<WorkItem> GetDistinctWorkByCategory() => backlog.DistinctBy(w => w.Category);

    // The FirstOrDefault, SingleOrDefault, and LastOrDefault methods now take a default value that you can define
    public WorkItem GetWorkWithNoneCategory() =>
        backlog.FirstOrDefault(w => w.Category is WorkCategory.None,
            new WorkItem(0, WorkCategory.None));

    public WorkItem AddNew(WorkCategory workCategory)
    {
        lock (backlogLock)
        {
            var workItem = new WorkItem(backlog.Count + 1, workCategory);
            backlog.Add(workItem);
            return workItem;
        }
    }

    public IEnumerable<WorkItem> Backlog => backlog.AsReadOnly();

    public WorkItem? GetById(int id) => backlog.FirstOrDefault(item => item.Id == id);
}
