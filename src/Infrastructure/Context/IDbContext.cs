using System.Data.SqlClient;

namespace Infrastructure.Context;

public interface IDbContext
{
    SqlConnection GetCon();
}
