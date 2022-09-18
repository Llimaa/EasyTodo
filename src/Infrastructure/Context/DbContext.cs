using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public class DbContext : IDbContext
{
    private IConfiguration configuration { get; }

    public DbContext(IConfiguration configuration) => this.configuration = configuration;
    public SqlConnection GetCon() => new SqlConnection(configuration.GetConnectionString("Connection"));
}