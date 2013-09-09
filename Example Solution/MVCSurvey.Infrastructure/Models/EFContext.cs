using System.Data.Entity;

namespace MVCSurvey.Infrastructure.Models
{
    public class EFContext : DbContext
    {

        public EFContext()
            : base("name=EFContext")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Survey.SurveyModel> SurveyModels { get; set; }
        public DbSet<Survey.SurveyInstance> SurveyInstances { get; set; }
        public DbSet<Survey.SurveyKeyValue> SurveyKeyValues { get; set; }
        public DbSet<Survey.SurveyParameter> SurveyParameters { get; set; }
        public DbSet<Survey.SurveyParameterMember> SurveyParameterMembers { get; set; }

    } 

}