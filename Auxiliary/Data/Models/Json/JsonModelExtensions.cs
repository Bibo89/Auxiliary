namespace Auxiliary
{
    public static class JsonModelExtensions
    {
        public static async Task<bool> SaveAsync<T>(this T? model, CancellationToken cancellationToken = default)
            where T : JsonModel, new()
            => model is not null && await JsonModelHelper<T>.SaveAsync(model, cancellationToken);

        public static async Task<bool> DeleteAsync<T>(this T? model, CancellationToken cancellationToken = default)
            where T : JsonModel, new()
            => model is not null && await JsonModelHelper<T>.DeleteAsync(model, cancellationToken);
    }
}
