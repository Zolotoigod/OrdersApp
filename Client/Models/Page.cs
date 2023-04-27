namespace OrdersApp.Client.Models
{
    public class Page
    {
        public Page(int number, bool isCurrent)
        {
            Number = number;
            IsCurrent = isCurrent;
        }

        public int Number { get; init; }

        public bool IsCurrent { get; set; }
    }
}
