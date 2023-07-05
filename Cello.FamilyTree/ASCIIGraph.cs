using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cello.FamilyTree
{
    public static class ASCIIGraph
    {
        public static string Show(FamilyTree familyTree)
        {
            if (familyTree == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            sb = ShowInternal(familyTree.GetRootMother(), 0, sb, false);

            sb.Append("\n\n (* kids from a former marriage)");

            return sb.ToString();
        }

        private static StringBuilder ShowInternal(Person p, int padSize, StringBuilder sb, bool isKidFromFormerMarriage)
        {
            if (p == null)
            {
                return null;
            }

            var pad = new String('\t', padSize);

            sb.Append("\n");

            if (isKidFromFormerMarriage)
            {
                pad += "* ";
            }

            if (p.Spouse == null)
            {
                sb.Append(pad + p.Name);
            }
            else
            {
                sb.Append(pad + p.Name + " married to " + p.Spouse.Name);
            }

            if (p.FirstChild == null)
            {
                return sb;
            }

            bool _isKidFromFormerMarriage;

            var child = p.FirstChild;

            while (child != null)
            {
                if (
                    (p.Gender == Genders.Male && child.Mother != p.Spouse) ||
                    (p.Gender == Genders.Female && child.Father != p.Spouse)
                )
                {
                    _isKidFromFormerMarriage = true;
                }
                else
                {
                    _isKidFromFormerMarriage = false;
                }

                sb = ShowInternal(child, padSize + 1, sb, _isKidFromFormerMarriage);

                child = child.YoungerSibling;
            }

            return sb;
        }
    }
}
