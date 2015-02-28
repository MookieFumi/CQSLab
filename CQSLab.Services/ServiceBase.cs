using System.Configuration;
using CQSLab.Entities;

namespace CQSLab.Services
{
    public abstract class ServiceBase
    {
        protected readonly ModelContext Context;

        protected ServiceBase(ModelContext context)
        {
            Context = context;
        }
    }
}