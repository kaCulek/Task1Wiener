using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Task1Wiener.Pages
{
    public class IndexModel(PartnerRepository partnerRepository) : PageModel
    {
        private readonly PartnerRepository _partnerRepository = partnerRepository ?? throw new ArgumentNullException(nameof(partnerRepository));

        public IEnumerable<Partner> Partners { get; private set; } = Enumerable.Empty<Partner>();

        public async Task OnGetAsync()
        {
            Partners = await _partnerRepository.GetAllPartnersAsync();
        }
    }
}
