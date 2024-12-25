namespace Task1Wiener
{
    public class Partner
    {
        public int PartnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long PartnerNumber { get; set; }
        public string CroatianPIN { get; set; }
        public int PartnerTypeId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedByUser { get; set; }
        public bool IsForeign { get; set; }
        public string ExternalCode { get; set; }
        public char Gender { get; set; }
    }

    public class Policy
    {
        public int PolicyId { get; set; }
        public int PartnerId { get; set; }
        public string PolicyNumber { get; set; }
        public decimal PolicyAmount { get; set; }
    }
}
