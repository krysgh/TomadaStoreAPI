using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repositories.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        #region ConnectionLoggerDependencyInjection
        private ILogger<ICustomerRepository> _logger;
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<ICustomerRepository> logger, ConnectionDB connectionDB)
        {
            this._logger = logger;
            this._connection = connectionDB.GetConnection();
        }
        #endregion

        public async Task InsertCustomerAsync(Customer customer)
        {
            try
            {
                var insertSql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber,Status)
                           VALUES (@FirstName, @LastName, @Email, @PhoneNumber,@Status);";

                await _connection.ExecuteAsync(insertSql, new { customer.FirstName, 
                                                                customer.LastName, 
                                                                customer.Email, 
                                                                customer.PhoneNumber,
                                                                customer.Status});
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                var sqlSelect = @"SELECT CustomerId, FirstName,LastName,Email,PhoneNumber,[Status]
                                  FROM Customers";

                return (await _connection.QueryAsync<CustomerResponseDTO>(sqlSelect)).ToList();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<CustomerResponseDTO?> GetCustomerByIdAsync(int id)
        {
            try
            {
                var sqlSelect = @"SELECT CustomerId, FirstName,LastName,Email,PhoneNumber,[Status]
                                  FROM Customers
                                  WHERE CustomerId = @CustomerId";

                return await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sqlSelect, new { CustomerId = id });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task UpdateCustomerStatusByIdAsync(int id)
        {
            try
            {
                var sqlUpdate = @"UPDATE Customers
                                  SET Status = CASE 
                                               WHEN Status = 1 THEN 0 
                                               ELSE 1 
                                               END
                                  WHERE CustomerId = @CustomerId";

                await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sqlUpdate, new { CustomerId = id });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
