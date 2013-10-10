namespace Exu.RouteService.Infra.Query
{
    public interface IQuery<out TResult>
    {
        TResult Execute();
    }
}