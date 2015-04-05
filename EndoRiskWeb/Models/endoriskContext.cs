using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EndoRiskWeb.Models.Mapping;

namespace EndoRiskWeb.Models
{
    public partial class endoriskContext : DbContext
    {
        static endoriskContext()
        {
            Database.SetInitializer<endoriskContext>(null);
        }

        public endoriskContext()
            : base("Name=endoriskContext")
        {
        }

        public IDbSet<administrator> administrators { get; set; }
        public IDbSet<comment> comments { get; set; }
        public IDbSet<disease> diseases { get; set; }
        public IDbSet<endoanswer> endoanswers { get; set; }
        public IDbSet<endochoice> endochoices { get; set; }
        public IDbSet<endoquestion> endoquestions { get; set; }
        public IDbSet<patient> patients { get; set; }
        public IDbSet<patientsymptom> patientsymptoms { get; set; }
        public IDbSet<romeanswer> romeanswers { get; set; }
        public IDbSet<romechoice> romechoices { get; set; }
        public IDbSet<romedependency> romedependencies { get; set; }
        public IDbSet<romediagnosi> romediagnosis { get; set; }
        public IDbSet<romequestion> romequestions { get; set; }
        public IDbSet<romequestionnaire> romequestionnaires { get; set; }
        public IDbSet<severity> severities { get; set; }
        public IDbSet<symptom> symptoms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new administratorMap());
            modelBuilder.Configurations.Add(new commentMap());
            modelBuilder.Configurations.Add(new diseaseMap());
            modelBuilder.Configurations.Add(new endoanswerMap());
            modelBuilder.Configurations.Add(new endochoiceMap());
            modelBuilder.Configurations.Add(new endoquestionMap());
            modelBuilder.Configurations.Add(new patientMap());
            modelBuilder.Configurations.Add(new patientsymptomMap());
            modelBuilder.Configurations.Add(new romeanswerMap());
            modelBuilder.Configurations.Add(new romechoiceMap());
            modelBuilder.Configurations.Add(new romedependencyMap());
            modelBuilder.Configurations.Add(new romediagnosiMap());
            modelBuilder.Configurations.Add(new romequestionMap());
            modelBuilder.Configurations.Add(new romequestionnaireMap());
            modelBuilder.Configurations.Add(new severityMap());
            modelBuilder.Configurations.Add(new symptomMap());
        }
    }
}
