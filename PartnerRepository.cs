using Microsoft.Data.SqlClient;
using Dapper;

namespace Task1Wiener
{
    public class PartnerRepository
    {
        private readonly string _connectionString;

        public PartnerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PartnerRepository()
        {
            _connectionString = "Server=.;Database=Task1Wiener;Trusted_Connection=True;";   
        }

        public async Task<IEnumerable<Partner>> GetAllPartnersAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Partner>("SELECT * FROM Partners ORDER BY CreatedAtUtc DESC");
        }

        public async Task<Partner?> GetPartnerByIdAsync(int partnerId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Partner>("SELECT * FROM Partners WHERE PartnerId = @PartnerId", new { PartnerId = partnerId });
        }

        public async Task<int> AddPartnerAsync(Partner partner)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Partners (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreatedByUser, IsForeign, ExternalCode, Gender)
                        VALUES (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId, @CreatedAtUtc, @CreatedByUser, @IsForeign, @ExternalCode, @Gender);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.ExecuteScalarAsync<int>(sql, partner);
        }

        public async Task<IEnumerable<Policy>> GetPoliciesByPartnerIdAsync(int partnerId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Policy>("SELECT * FROM Policies WHERE PartnerId = @PartnerId", new { PartnerId = partnerId });
        }

        public async Task<int> AddPolicyAsync(Policy policy)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Policies (PartnerId, PolicyNumber, PolicyAmount)
                        VALUES (@PartnerId, @PolicyNumber, @PolicyAmount);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.ExecuteScalarAsync<int>(sql, policy);
        }
    }
}

