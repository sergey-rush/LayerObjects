namespace LOB.Core
{
    public class OrderReport
    {
        public Shop Shop { get; set; }
        
        public Customer Customer { get; set; }

        private decimal _deliveryCost = 200;
        public decimal DeliveryCost
        {
            get { return _deliveryCost; }
            set { _deliveryCost = value; }
        }
    }
}