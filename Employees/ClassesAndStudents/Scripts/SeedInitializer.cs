using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Employees.Models;

namespace Employees.Scripts
{
    public class SeedInitializer : DropCreateDatabaseAlways<EFModel1>
    {
        protected override void Seed(EFModel1 context)
        {
            IList<JobTitleEntity> defaultStandards = new List<JobTitleEntity>();

            defaultStandards.Add(new JobTitleEntity() { ID = 1, JobTitle = "CEO" });
            defaultStandards.Add(new JobTitleEntity() { ID = 2, JobTitle = "Business Analyst" });
            defaultStandards.Add(new JobTitleEntity() { ID = 3, JobTitle = "Dev" });
            defaultStandards.Add(new JobTitleEntity() { ID = 4, JobTitle = "QA" });

            foreach (JobTitleEntity std in defaultStandards)
                context.JobTitleEntities.Add(std);

            base.Seed(context);
        }
    }

}