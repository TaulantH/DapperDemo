using Dapper;
using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DapperDemo.Repository
{
	public class CompanyRepository : ICompanyRepository
	{
		private IDbConnection db;
		public CompanyRepository(IConfiguration configuration)
		{
			this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection")); 
		}
		public Company Add(Company company)
		{
			var sql = "INSERT INTO Companies (Name, Address,City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);" + "SELECT CAST(SCOPE_IDENTITY() AS int);";
			var id = db.Query<int>(sql,company).Single();
			company.CompanyId = id;
			return company;
		}

		public Company Find(int id)
		{
			var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";  // Correct parameter name
			return db.Query<Company>(sql, new { CompanyId = id }).Single();  // Corrected parameter name
		}


		public List<Company> GetAll()
		{
			var sql = "SELECT * FROM Companies";
			return db.Query<Company>(sql).ToList();
		}

		public void Remove(int id)
		{
			var sql = "DELETE FROM Companies WHERE CompanyId = @Id";
			db.Execute(sql, new {id});
		}

		public Company Update(Company company)
		{
			var sql = "UPDATE Companies SET Name = @Name, Address = @Address, City = @City, " +
				"State = @State, PostalCode = @PostalCode WHERE CompanyId = @CompanyId";
			db.Execute(sql, company);
			return company;
		}
	}
}
