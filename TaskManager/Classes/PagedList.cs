namespace TaskManager.Classes;

public class PagedList<T>
{
  private PagedList(List<T> items, int page, int pageSize, int totalCount)
  {
    Items = items;
    Page = page;
    PageSize = pageSize;
    TotalCount = totalCount;
  }

  public List<T> Items { get; }

  public int Page { get; }

  public int PageSize { get; }

  public int TotalCount { get; }

  public bool HasNextPage => Page * PageSize < TotalCount;

  public bool HasPreviousPage => Page > 1;

  public static PagedList<T> Create(List<T> tasks, int page, int pageSize, int totalCount)
  {
    return new(tasks, page, pageSize, totalCount);
  }
}
