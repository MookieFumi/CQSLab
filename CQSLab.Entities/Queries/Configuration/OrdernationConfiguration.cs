using System;
using CQSLab.CrossCutting.Enums;

namespace CQSLab.Entities.Queries.Configuration
{
    public class OrdernationConfiguration
    {
        public OrdernationConfiguration()
        {
            SortExpression = String.Empty;
        }

        public SortDirection SortDirection { get; set; }
        public string SortExpression { get; set; }
    }
}