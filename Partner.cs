namespace PolApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq; 

    public partial class Partner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partner()
        {
            this.Partner_products = new HashSet<Partner_products>();
        }

        public int idPartner { get; set; }
        public Nullable<int> typePartner { get; set; }
        public string namePartner { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string adress { get; set; }
        public Nullable<double> inn { get; set; }
        public Nullable<int> rate { get; set; }
        public string surnameDirector { get; set; }
        public string nameDirector { get; set; }
        public string patronymicDirector { get; set; }

       

        public float TotalProductsSold
        {
            get
            {
                var total = MasterPolEntities1.GetContext()
                    .Partner_products
                    .Where(p => p.idPartner == this.idPartner)
                    .Sum(p => (int?)p.countProduct) ?? 0;

                return (float)total;
            }
        }

        public string GetDiscount
        {
            get
            {
                float total = TotalProductsSold;

                if (total < 10000) return "0%";
                else if (total < 50000) return "5%";
                else if (total < 300000) return "10%";
                else return "15%";
            }
        }

        // =================================

        public virtual ICollection<Partner_products> Partner_products { get; set; }
        public virtual Partners_type Partners_type { get; set; }
    }
}