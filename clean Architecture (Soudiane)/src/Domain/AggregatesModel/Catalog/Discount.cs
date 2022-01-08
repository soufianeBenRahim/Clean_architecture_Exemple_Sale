using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog
{
    public class Discount
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Guid Referance { get; set; }
        public string Bar_Code { get; set; }
        public DescountValueType DescountValueType { get; set; }
        public double ValueDescount { get; set; }
        public double QteMin { get; set; }
        public double MaxQteSale { get; set; }
        public double CumulQteSaled { get; set; }
        public DateTime DateBeginDescount { get; set; }
        public DateTime DateEndDescount { get; set; }
        public bool IsActiveInDate(DateTime dateReferance,double QteSaled)
        {
            if ((DateBeginDescount != null || DateBeginDescount <= dateReferance)
                && (DateEndDescount != null || DateEndDescount >= dateReferance))
            {
                return true;
            }
            if ( QteSaled >= QteMin)
            {
                return true;
            }
            if ( CumulQteSaled >= MaxQteSale)
            {
                return true;
            }
            return false;
        }
    }
}
