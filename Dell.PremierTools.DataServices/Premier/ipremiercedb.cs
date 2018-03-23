using Dell.PremierTools.Entity.Model.viewmodel;

namespace Dell.PremierTools.DataServices.Premier
{
   public interface  Ipremiercepage
    {
        landingpageviewmodel getpremierlandingpagedetails(string emailaddress, string customer_set_id);
    }
}
