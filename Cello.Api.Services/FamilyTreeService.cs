using Cello.Api.Services.Interfaces;
using Cello.FamilyTree;

namespace Cello.Api.Services
{
    public class FamilyTreeService : IFamilyTreeService
    {
        private FamilyTree.FamilyTree familyTree;

        public bool CreateFamilyTree(int motherId, string motherName, int fatherId, string fatherName)
        {
            familyTree = FamilyTree.FamilyTree.CreateFamilyTree(motherId, motherName, fatherId, fatherName);

            return familyTree != null;
        }

        public bool Divorce(int motherId, int fatherId, Custodies custody)
        {
            if (familyTree == null)
            {
                return false;
            }

            return familyTree.Divorce(motherId, fatherId, custody);
        }

        public bool HaveKid(int motherId, int fatherId, int kidId, string kidName, Genders kidGender)
        {
            if (familyTree == null)
            {
                return false;
            }

            return familyTree.HaveKid(motherId, fatherId, kidId, kidName, kidGender);
        }

        public bool Marry(int id, int otherId, string otherName, Genders otherGender)
        {
            if (familyTree == null)
            {
                return false;
            }

            return familyTree.Marry(id, otherId, otherName, otherGender);
        }

        public string Show()
        {
            return ASCIIGraph.Show(familyTree);
        }
    }
}