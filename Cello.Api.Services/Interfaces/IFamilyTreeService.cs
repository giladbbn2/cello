using Cello.FamilyTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cello.Api.Services.Interfaces
{
    public interface IFamilyTreeService
    {
        public bool CreateFamilyTree(int motherId, string motherName, int fatherId, string fatherName);
        public bool HaveKid(int motherId, int fatherId, int kidId, string kidName, Genders kidGender);
        public bool Marry(int id, int otherId, string otherName, Genders otherGender);
        public bool Divorce(int motherId, int fatherId, Custodies custody);
        public string Show();
    }
}
