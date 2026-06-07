namespace GCashAPI.Models
{
    public class PayBillRequest
    {
        public string Biller { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
}