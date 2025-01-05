using Microsoft.Data.SqlClient;
using Dapper;
  
namespace Task1Wiener
{
    public class PartnerRepository
    {
        private readonly string _connectionString;

     
        public PartnerRepository()
        {
            _connectionString = "Data Source=KARLO; Initial Catalog=master; Integrated Security=SSPI; Trust Certificate=True;";  
        }

        public async Task<IEnumerable<Partner>> GetAllPartnersAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var result1 = await connection.QueryAsync<Partner>("SELECT * FROM dbo.Partners ORDER BY CreatedAtUtc DESC");
            return result1.ToList();
        }

        public async Task<Partner?> GetPartnerByIdAsync(int partnerId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var result2 = await connection.QuerySingleOrDefaultAsync<Partner>("SELECT * FROM dbo.Partners WHERE PartnerId = @PartnerId", new { PartnerId = partnerId });
            return result2;
        }

        public async Task<int> AddPartnerAsync(Partner partner)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = @"INSERT INTO dbo.[Partners] (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreatedByUser, IsForeign, ExternalCode, Gender)
                        VALUES (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId, @CreatedAtUtc, @CreatedByUser, @IsForeign, @ExternalCode, @Gender);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            var result3 = await connection.ExecuteScalarAsync<int>(sql, partner);
            return result3;
        }

        public async Task<IEnumerable<Policy>> GetPoliciesByPartnerIdAsync(int partnerId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var result4 = await connection.QueryAsync<Policy>("SELECT * FROM dbo.Policies WHERE PartnerId = @PartnerId", new { PartnerId = partnerId });
            return result4.ToList();
        }

        public async Task<int> AddPolicyAsync(Policy policy)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = @"INSERT INTO dbo.Policies (PartnerId, PolicyNumber, PolicyAmount)
                        VALUES (@PartnerId, @PolicyNumber, @PolicyAmount);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            var result5 = await connection.ExecuteScalarAsync<int>(sql, policy);
            return result5; 
        }
    }
}

