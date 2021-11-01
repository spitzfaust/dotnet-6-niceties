var workOrganizer = new WorkOrganizer();

var workWithNoneCategory = workOrganizer.GetWorkWithNoneCategory();
Console.WriteLine("Work with none category:");
Console.WriteLine(workWithNoneCategory);

var distinctWorkByCategory = workOrganizer.GetDistinctWorkByCategory();
Console.WriteLine("Distinct work by category:");
foreach (var work in distinctWorkByCategory)
{
    Console.WriteLine(work);
}

var workForSprints = workOrganizer.GetWorkForSprints().ToList();
Console.WriteLine("Work for sprints:");
for (int sprint = 0; sprint < workForSprints.Count; sprint++)
{
    Console.WriteLine($"Sprint {sprint}: {workForSprints[sprint].Length} items");
}