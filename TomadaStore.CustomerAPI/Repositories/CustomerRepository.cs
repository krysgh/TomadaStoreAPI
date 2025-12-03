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

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var insertSql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber)
                           VALUES (@FirstName, @LastName, @Email, @PhoneNumber);";

                await _connection.ExecuteAsync(insertSql, new { customer.FirstName, 
                                                                customer.LastName, 
                                                                customer.Email, 
                                                                customer.PhoneNumber });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
