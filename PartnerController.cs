using Microsoft.AspNetCore.Mvc;

namespace Task1Wiener
{
    public class PartnerController : Controller
    {
        private readonly PartnerRepository _repository;

        public PartnerController(PartnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var partners = await _repository.GetAllPartnersAsync();
            return View(partners);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Partner partner)
        {
            if (ModelState.IsValid)
            {
                partner.CreatedAtUtc = DateTime.UtcNow;
                await _repository.AddPartnerAsync(partner);
                return RedirectToAction(nameof(Index));
            }
            return View(partner);
        }

        public async Task<IActionResult> Details(int id)
        {
            var partner = await _repository.GetPartnerByIdAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            var policies = await _repository.GetPoliciesByPartnerIdAsync(id);
            var model = new PartnerDetailsViewModel
            {
                Partner = partner,
                Policies = policies
            };
            return View(model);
        }

        public IActionResult AddPolicy(int partnerId)
        {
            var model = new AddPolicyViewModel
            {
                PartnerId = partnerId,
                PolicyNumber = string.Empty // Initialize required member
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPolicy(AddPolicyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var policy = new Policy
                {
                    PartnerId = model.PartnerId,
                    PolicyNumber = model.PolicyNumber,
                    PolicyAmount = model.PolicyAmount,
                    CreatedAtUtc = DateTime.UtcNow
                };
                await _repository.AddPolicyAsync(policy);
                return RedirectToAction(nameof(Details), new { id = model.PartnerId });
            }
            return View(model);
        }
    }

    public class AddPolicyViewModel
    {
        public int PartnerId { get; set; }
        public required string PolicyNumber { get; set; }
        public decimal PolicyAmount { get; set; }
    }

    public class PartnerDetailsViewModel
    {
        public required Partner Partner { get; set; }
        public required IEnumerable<Policy> Policies { get; set; }
    }
}
