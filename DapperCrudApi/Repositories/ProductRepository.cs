using Dapper;
using DapperCrudApi.Data;
using DapperCrudApi.Models;
using DapperCrudApi.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DapperCrudApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }


        #region Context Based Repository Methods
        /*
        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.Include(p => p.ProductDetails).ToListAsync();
        */
        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.Include(p => p.ProductDetails)
                                   .FirstOrDefaultAsync(p => p.Id == id);
        
        public async Task<Product> AddAsync(Product product)
        {
            var data = await _context.Products.AddAsync(product);
            var changes = await _context.SaveChangesAsync();
            return product;
            //return data.State == EntityState.Added && changes > 0;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var data = _context.Products.Update(product);
            var changes = await _context.SaveChangesAsync();
            //return data.State == EntityState.Modified && changes > 0;
            return changes > 0;
        }
        /*
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
                _context.Products.Remove(product);
        }
        */
        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
        #endregion

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Product";
            return await connection.QueryAsync<Product>(sql);
        }

        /*
        public async Task<Product?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Product WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });

        //With Details
        using var connection = new SqlConnection(_connectionString);

    var sql = @"
        SELECT p.*, pd.*
        FROM Product p
        LEFT JOIN ProductDetails pd ON p.Id = pd.ProductId
        WHERE p.Id = @Id;
    ";

    var productDict = new Dictionary<int, Product>();

    var result = await connection.QueryAsync<Product, ProductDetail, Product>(
        sql,
        (product, detail) =>
        {
            if (!productDict.TryGetValue(product.Id, out var currentProduct))
            {
                currentProduct = product;
                currentProduct.Details = new List<ProductDetail>();
                productDict.Add(product.Id, currentProduct);
            }

            if (detail != null)
                currentProduct.Details.Add(detail);

            return currentProduct;
        },
        new { Id = id },
        splitOn: "Id" // Make sure this matches the first column of ProductDetail
    );

    return result.FirstOrDefault();
        }
        public async Task<int> AddAsync(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Product (Name, Price) VALUES (@Name, @Price); SELECT SCOPE_IDENTITY();";
            var id = await connection.ExecuteScalarAsync<int>(sql, product);
            return id;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Product SET Name = @Name, Price = @Price WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, product);
            return rowsAffected > 0;
        }
        */
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Product WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
