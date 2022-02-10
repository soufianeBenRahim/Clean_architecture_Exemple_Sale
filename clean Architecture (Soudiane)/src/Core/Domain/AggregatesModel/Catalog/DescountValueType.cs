namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog
{
    using Clean_Architecture_Soufiane.Domain.SeedWork;
    using Clean_Architecture_Soufiane.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DescountValueType
        : Enumeration
    {

        public static DescountValueType Value = new DescountValueType(1, nameof(Value).ToLowerInvariant());
        public static DescountValueType Percentage = new DescountValueType(2, nameof(Percentage).ToLowerInvariant());


        public DescountValueType(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<DescountValueType> List() =>
            new[] { Value, Percentage };

        public static DescountValueType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ValidationDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static DescountValueType From(int id)
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

