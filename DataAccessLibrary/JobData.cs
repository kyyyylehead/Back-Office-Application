using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class JobData : IJobData
    {
        private readonly ISQLDataAccess _db;
        public JobData(ISQLDataAccess db)
        {
            _db = db;
        }

        public Task<List<JobModel>> GetJobs()
        {
            string sql = "select * from dbo.Jobs";

            return _db.LoadData<JobModel, dynamic>(sql, new { });
        }

        public Task<List<JobModel>> GetJob(int superiorOrderNumber)
        {
            string sql = "select * from dbo.Jobs where SuperiorOrderWorkNumber=" + superiorOrderNumber + ";";

            return _db.LoadData<JobModel, dynamic>(sql, new { });
        }

        public Task InsertJob(JobModel job)
        {
            string sql = @"insert into dbo.Jobs (Date, Name, SuperiorWorkOrderNumber, CustomerOrderNumber)
                           values (@Date, @Name, @SuperiorWorkOrderNumber, @CustomerOrderNumber)";

            return _db.SaveData(sql, job);
        }
    }
}
