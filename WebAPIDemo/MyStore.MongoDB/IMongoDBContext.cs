using MongoDB.Driver;

namespace MyStore.MongoDB
{
    public interface IMongoDBContext
    {
        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        IMongoCollection<ProductModel> Products { get; }
    }
}