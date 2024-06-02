using System;
using Dapper;
using System.Data;
using MySqlConnector;
using mgrNET.Store;
using mgrNET.Domain;

namespace mgrNET.Store.MySql;

public class MySqlSpendingStore : ISpendingStore
{
    private readonly string connectionString;
    private readonly SqlHelper<MySqlSpendingStore> sqlHelper;

    public MySqlSpendingStore(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SpendingDb");
        if (connectionString == null)
        {
            throw new InvalidOperationException("Missing [SpendingDb] connection string.");
        }

        this.connectionString = connectionString;
        sqlHelper = new SqlHelper<MySqlSpendingStore>();
    }
    public async Task Create(CreateSpendingParams createSpendingParams)
    {
        await using var connection = new MySqlConnection(connectionString);
        {
            var parameters = new
            {
                createSpendingParams.id,
                createSpendingParams.name,
                createSpendingParams.value,
                createSpendingParams.category,
                createSpendingParams.date
            };

            try
            {
                await connection.ExecuteAsync(
                    sqlHelper.GetSqlFromEmbeddedResource("Create"),
                    parameters,
                    commandType: CommandType.Text);
            }
            catch (MySqlException ex)
            {
                if (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    throw new DuplicateKeyException();
                }

                throw;
            }
        }
    }

    public async Task Delete(Guid id)
    {
        await using var connection = new MySqlConnection(connectionString);
        await connection.ExecuteAsync(
            sqlHelper.GetSqlFromEmbeddedResource("Delete"),
            new { id },
            commandType: CommandType.Text
            );
    }



    public async Task<IEnumerable<Spending>> GetAll()
    {
        await using var connection = new MySqlConnection(connectionString);
        var sql = sqlHelper.GetSqlFromEmbeddedResource("GetAll");
        return await connection.QueryAsync<Spending>(sql, commandType: CommandType.Text);
    }

    public async Task<Spending?> GetById(int id)
    {
        await using var connection = new MySqlConnection(connectionString);
        return await connection.QueryFirstOrDefaultAsync<Spending?>(
            sqlHelper.GetSqlFromEmbeddedResource("GetById"),
            new { id },
            commandType: CommandType.Text
            );
    }
        
    async Task<Domain.Spending?> ISpendingStore.GetById(int id)
    {
        await using var connection = new MySqlConnection(connectionString);
        return await connection.QueryFirstOrDefaultAsync<Spending?>(
            sqlHelper.GetSqlFromEmbeddedResource("GetById"),
            new { id },
            commandType: CommandType.Text
            );
    }

    public async Task Update(Guid id, UpdateSpendingParams updateSpendingParams)
    {
        await using var connection = new MySqlConnection(connectionString);
        {
            var parameters = new
            {
                Id = id,
                updateSpendingParams.name,
                updateSpendingParams.value,
                updateSpendingParams.category,
                updateSpendingParams.date,
            };

            await connection.ExecuteAsync(
                sqlHelper.GetSqlFromEmbeddedResource("Update"),
                parameters,
                commandType: CommandType.Text);
        }
    }
}