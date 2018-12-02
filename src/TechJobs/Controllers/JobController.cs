using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            SearchJobsViewModel searchJobsViewModel = new SearchJobsViewModel();
            searchJobsViewModel.Jobs = new List<Job> { jobData.Find(id) };
            return View(searchJobsViewModel);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            if(newJobViewModel.Name != null)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.SkillID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID),
                };
                jobData.Jobs.Add(newJob);
                return Redirect("/Job?id=" + newJob.ID);
            }
            else
            {
                return View(newJobViewModel);
            }
        }
    }
}
