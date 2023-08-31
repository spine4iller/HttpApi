namespace Api.Infra.Interfaces;

public interface ISearchService
{
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}

public class SearchRequest
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public string Origin { get; set; }
    
    // Mandatory
    // End point of route, e.g. Sochi
    public string Destination { get; set; }
    
    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; set; }
    
    // Optional
    public SearchFilters? Filters { get; set; }
}

public class SearchFilters
{
    // Optional
    // End date of route
    public DateTime? DestinationDateTime { get; set; }
    
    // Optional
    // Maximum price of route
    public decimal? MaxPrice { get; set; }
    
    // Optional
    // Minimum value of timelimit for route
    public DateTime? MinTimeLimit { get; set; }
    
    // Optional
    // Forcibly search in cached data
    public bool? OnlyCached { get; set; }
}

public class SearchResponse
{
    // Mandatory
    // Array of routes
    public Route[] Routes { get; set; }
    
    // Mandatory
    // The cheapest route
    public decimal MinPrice { get; set; }
    
    // Mandatory
    // Most expensive route
    public decimal MaxPrice { get; set; }
    
    // Mandatory
    // The fastest route
    public int MinMinutesRoute { get; set; }
    
    // Mandatory
    // The longest route
    public int MaxMinutesRoute { get; set; }
}

public class Route : IEquatable<Route>
{
    // Mandatory
    // Identifier of the whole route
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // Mandatory
    // Start point of route
    public string Origin { get; set; }
    
    // Mandatory
    // End point of route
    public string Destination { get; set; }
    
    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; set; }
    
    // Mandatory
    // End date of route
    public DateTime DestinationDateTime { get; set; }
    
    // Mandatory
    // Price of route
    public decimal Price { get; set; }
    
    // Mandatory
    // Timelimit. After it expires, route became not actual
    public DateTime TimeLimit { get; set; }

    public bool Equals(Route? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Origin == other.Origin && Destination == other.Destination && 
               OriginDateTime.Equals(other.OriginDateTime) && 
               DestinationDateTime.Equals(other.DestinationDateTime) && 
               Price == other.Price && TimeLimit.Equals(other.TimeLimit);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Route)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Origin, Destination, OriginDateTime, DestinationDateTime, Price, TimeLimit);
    }
}