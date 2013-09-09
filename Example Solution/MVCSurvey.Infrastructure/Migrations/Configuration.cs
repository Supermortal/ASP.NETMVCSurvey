using System.Data.Entity.Migrations;
using MVCSurvey.Infrastructure.Models;

namespace MVCSurvey.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFContext>
    {

        private EFContext db = new EFContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EFContext context)
        {
            
        }

    }
}
