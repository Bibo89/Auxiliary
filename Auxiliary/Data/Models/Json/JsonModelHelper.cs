namespace Auxiliary
{
    internal static class JsonModelHelper<T>
        where T : JsonModel, new()
    {
        public static readonly JsonCollection<T> Collection = new(typeof(T).Name + "s");

        public static async Task<bool> SaveAsync(T model, CancellationToken cancellationToken = default)
            => await Collection.InsertOrUpdateDocumentAsync(model, cancellationToken);

        public static async Task<bool> DeleteAsync(T model, CancellationToken cancellationToken = default)
            => await Collection.DeleteDocumentAsync(model, cancellationToken);

        public static async Task<T?> GetAsync(Func<T, bool> func, Action<T>? creationAction = null, CancellationToken cancellationToken = default)
        {
            var document = Collection.FindDocument(func);

            if (document is null && creationAction is not null)
                document = await CreateAsync(creationAction, cancellationToken);

            return document;
        }

        public static async Task<T> CreateAsync(Action<T> action, CancellationToken cancellationToken = default)
        {
            var document = new T();

            action(document);

            await Collection.InsertOrUpdateDocumentAsync(document, cancellationToken);

            return document;
        }
    }
}
