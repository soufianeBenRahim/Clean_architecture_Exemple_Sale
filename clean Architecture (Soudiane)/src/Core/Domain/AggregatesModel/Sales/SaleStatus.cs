using Clean_Architecture_Soufiane.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using Clean_Architecture_Soufiane.Domain.Exceptions;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales
{

    public class SaleStatus
        : Enumeration
    {

        public static SaleStatus AwaitingValidation = new SaleStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static SaleStatus Paid = new SaleStatus(4, nameof(Paid).ToLowerInvariant());
        public static SaleStatus Cancelled = new SaleStatus(6, nameof(Cancelled).ToLowerInvariant());

        public SaleStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<SaleStatus> List() =>
            new[] {  AwaitingValidation, Paid,  Cancelled };

        public static SaleStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ValidationDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static SaleStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ValidationDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
