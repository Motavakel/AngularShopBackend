namespace Application.Wrappers;

public class PaginationResponse<T> where T : class
{
    public PaginationResponse(int pageIndex, int pageSize, int count, IEnumerable<T> result, decimal minPrice, decimal maxPrice)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Result = result;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IEnumerable<T> Result { get; set; }
    public decimal MinPrice { get; set; }  
    public decimal MaxPrice { get; set; }
}
