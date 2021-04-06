using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IJobData
    {
        Task<List<JobModel>> GetJobs();
        Task<List<JobModel>> GetJob(int superiorOrderNumber);
        Task InsertJob(JobModel job);
    }
}