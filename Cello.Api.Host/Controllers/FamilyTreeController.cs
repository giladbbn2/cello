using Cello.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cello.Api.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Cello.Api.Host.Controllers
{
    [ApiController]
    public class FamilyTreeController : MainControllerBase
    {
        private readonly IFamilyTreeService _familyTreeService;

        public FamilyTreeController(IFamilyTreeService familyTreeService) : base()
        {
            _familyTreeService = familyTreeService;
        }

        [Route("api/family_tree/create_family_tree")]
        [HttpGet]
        public async Task CreateFamilyTreeAsync([Required] int motherId, [Required] string motherName, [Required] int fatherId, [Required] string fatherName)
        {
            var result = _familyTreeService.CreateFamilyTree(motherId, motherName, fatherId, fatherName);

            await SetSuccessResponseJSONAsync(result);
        }

        [Route("api/family_tree/marry")]
        [HttpGet]
        public async Task Marry([Required] int id, [Required] int otherId, [Required] string otherName, [Required] bool isMale)
        {
            var result = _familyTreeService.Marry(id, otherId, otherName, isMale ? FamilyTree.Genders.Male : FamilyTree.Genders.Female);

            await SetSuccessResponseJSONAsync(result);
        }

        [Route("api/family_tree/have_kids")]
        [HttpGet]
        public async Task HaveKid([Required] int motherId, [Required] int fatherId, [Required] int kidId, [Required] string kidName, [Required] bool isKidMale)
        {
            var result = _familyTreeService.HaveKid(motherId, fatherId, kidId, kidName, isKidMale ? FamilyTree.Genders.Male : FamilyTree.Genders.Female);

            await SetSuccessResponseJSONAsync(result);
        }

        [Route("api/family_tree/divorce")]
        [HttpGet]
        public async Task Divorce([Required] int motherId, [Required] int fatherId, [Required] bool isCustodyWithMother)
        {
            var result = _familyTreeService.Divorce(motherId, fatherId, isCustodyWithMother ? FamilyTree.Custodies.WithMother : FamilyTree.Custodies.WithFather);

            await SetSuccessResponseJSONAsync(result);
        }

        [Route("api/family_tree/show")]
        [HttpGet]
        public async Task Show()
        {
            await SetSuccessResponseJSONAsync(_familyTreeService.Show());
        }
    }
}
