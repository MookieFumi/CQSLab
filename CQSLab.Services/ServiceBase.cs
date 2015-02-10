using System.Configuration;
using CQSLab.Entities;

namespace CQSLab.Services
{
    public abstract class ServiceBase
    {
        public readonly string ConnectionString = "";
        public readonly ModelContext Context;

        protected ServiceBase(ModelContext context)
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["ModelContext"].ConnectionString;
            this.Context = context;
        }
    }
}