using System.ComponentModel.DataAnnotations;

namespace TurboForms.ViewModels
{
    public class CreateOrder
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TurboForms.Resources.DataAnnotation))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TurboForms.Resources.DataAnnotation))]
        public string Phone { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TurboForms.Resources.DataAnnotation))]
        public string Adress { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TurboForms.Resources.DataAnnotation))]
        public DateTime PickupDate { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TurboForms.Resources.DataAnnotation))]
        public string CustomerName { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TurboForms.Resources.DataAnnotation))]
        public string CustomerPhone { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TurboForms.Resources.DataAnnotation))]
        public string CustomerAdress { get; set; }
        public string CustomerAdressDetails { get; set; }
        public double OrderPrice { get; set; }
        public double DeliveryFee { get; set; }
        public double Total { get; set; }
        public bool OrderStatus { get; set; } //breakable or not
        public string OrderDetails { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } //Cash/wallet/instapay
    }
}
