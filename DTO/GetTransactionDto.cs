namespace TrackWallet.DTO
{
    public class GetTransactionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public catagorydto Category { get; set; }
        public string Type { get; set; }
        public string? Url { get; set; } = string.Empty;
    }
}
