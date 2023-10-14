namespace WebApi.Helpers;

public class FilterRoot
{
    public int first { get; set; }
    public int rows { get; set; }
    public int sortOrder { get; set; }
    public Filters filters { get; set; }
    public object globalFilter { get; set; }
}
public class Filters
{
}
