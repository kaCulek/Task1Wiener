namespace Task1Wiener
{
    public class Partner
    {
        public int PartnerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public long PartnerNumber { get; set; }
        public required string CroatianPIN { get; set; }
        public int PartnerTypeId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public required string CreatedByUser { get; set; }
        public bool IsForeign { get; set; }
        public required string ExternalCode { get; set; }
        public char Gender { get; set; }
    }

    public class Policy
    {
        public int PolicyId { get; set; }
        public int PartnerId { get; set; }
        public required string PolicyNumber { get; set; }
        public decimal PolicyAmount { get; set; }
        public DateTime CreatedAtUtc { get; internal set; }
    }
}
