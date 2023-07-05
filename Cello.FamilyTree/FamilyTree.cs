using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cello.FamilyTree
{
    public class FamilyTree
    {
        private Person rootFather;
        private Person rootMother;

        private FamilyTree(int motherId, string motherName, int fatherId, string fatherName)
        {
            rootMother = new Person(motherId, motherName, Genders.Female);      // assumed female

            rootFather = new Person(fatherId, fatherName, Genders.Male);        // assumed male

            rootMother.Spouse = rootFather;

            rootFather.Spouse = rootMother;
        }

        public static FamilyTree CreateFamilyTree(int motherId, string motherName, int fatherId, string fatherName)
        {
            // validate input

            return new FamilyTree(motherId, motherName, fatherId, fatherName);
        }

        public Person GetRootFather()
        {
            return rootFather;
        }

        public Person GetRootMother()
        {
            return rootMother;
        }

        private Person findPerson(int id)
        {
            if (rootFather.Id == id)
            {
                return rootFather;
            }

            if (rootMother.Id == id)
            {
                return rootMother;
            }

            var child = rootFather.FirstChild;

            if (child == null)
            {
                return null;
            }

            return findPersonRecursive(child, id);
        }

        private Person findPersonRecursive(Person p, int id)
        {
            if (p == null)
            {
                return null;
            }

            if (p.Id == id)
            {
                return p;
            }

            Person result = null;

            if (p.Spouse != null && p.Spouse.Id == id)
            {
                return p.Spouse;
            }

            if (p.YoungerSibling != null)
            {
                result = findPersonRecursive(p.YoungerSibling, id);

                if (result != null)
                {
                    return result;
                }
            }

            if (p.FirstChild != null)
            {
                result = findPersonRecursive(p.FirstChild, id);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public bool HaveKid(int motherId, int fatherId, int kidId, string kidName, Genders kidGender)
        {
            // assuming one parent is male and the other is female

            if (motherId == fatherId)
            {
                // mother and father are same person

                return false;
            }

            var mother = findPerson(motherId);

            if (mother == null)
            {
                // parent wasn't found

                return false;
            }

            var father = findPerson(fatherId);

            if (father == null)
            {
                // parent wasn't found

                return false;

            }

            if (mother.Spouse != father)
            {
                // parents are not married

                return false;
            }

            var kid = new Person(kidId, kidName, kidGender);

            kid.Mother = mother;

            kid.Father = father;

            Person child = null;

            if (mother.FirstChild == null)
            {
                mother.FirstChild = kid;

                if (father.FirstChild == null)
                {
                    father.FirstChild = kid;

                    return true;
                }

                child = father.FirstChild;

                while (child.YoungerSibling != null)
                {
                    child = child.YoungerSibling;
                }

                child.YoungerSibling = kid;

                return true;
            }
            else if (father.FirstChild == null)
            {
                father.FirstChild = kid;

                if (mother.FirstChild == null)
                {
                    mother.FirstChild = kid;

                    return true;
                }

                child = mother.FirstChild;

                while (child.YoungerSibling != null)
                {
                    child = child.YoungerSibling;
                }

                child.YoungerSibling = kid;

                return true;
            }

            // both mother and father have kids

            child = mother.FirstChild;

            while (child.YoungerSibling != null)
            {
                child = child.YoungerSibling;
            }

            child.YoungerSibling = kid;

            if (child.Father == father)
            {
                // parents had their last child together

                return true;
            } 
            else
            {
                // parent didn't have their last child together

                child = father.FirstChild;

                while (child.YoungerSibling != null)
                {
                    child = child.YoungerSibling;
                }

                child.YoungerSibling = kid;

                return true;
            }
        }

        public bool Marry(int id, int otherId, string otherName, Genders otherGender)
        {
            // not checking that one person is male and the other is female

            if (id == otherId)
            {
                // a person cannot marry themselves

                return false;
            }

            var person = findPerson(id);

            if (person == null)
            {
                // person was not found

                return false;
            }

            if (person.Spouse != null)
            {
                // person is already married

                return false;
            }

            if (findPerson(otherId) != null)
            {
                // other person was found
                // other person's ancestor must not be the root

                return false;
            }

            var other = new Person(otherId, otherName, otherGender);

            person.Spouse = other;

            other.Spouse = person;

            return true;
        }

        public bool Divorce(int motherId, int fatherId, Custodies custody)
        {
            if (motherId == fatherId)
            {
                // a person cannot divorce themselves

                return false;
            }

            var mother = findPerson(motherId);

            if (mother == null)
            {
                // mother was not found

                return false;
            }

            var father = findPerson(fatherId);

            if (father == null)
            {
                // father was not found

                return false;
            }

            mother.Spouse = null;
            father.Spouse = null;

            Person child = null;
            Person olderSibling = null;

            if (mother.Mother == null)
            {
                // mother's ancestor is not root

                if (custody == Custodies.WithFather)
                {
                    return true;
                }
                else
                {
                    // custody is with mother

                    child = father.FirstChild;

                    olderSibling = null;

                    while (child != null)
                    {
                        if (child.Mother == mother)
                        {
                            if (olderSibling == null)
                            {
                                father.FirstChild = child.YoungerSibling;
                            }
                            else
                            {
                                olderSibling.YoungerSibling = child.YoungerSibling;
                            }
                        }

                        olderSibling = child;

                        child = child.YoungerSibling;
                    }
                }
            }
            else
            {
                // father's ancestor is not root

                if (custody == Custodies.WithMother)
                {
                    return true;
                }
                else
                {
                    // custody is with father

                    child = mother.FirstChild;

                    olderSibling = null;

                    while (child != null)
                    {
                        if (child.Father == father)
                        {
                            if (olderSibling == null)
                            {
                                mother.FirstChild = child.YoungerSibling;

                                child = mother.FirstChild;
                            }
                            else
                            {
                                olderSibling.YoungerSibling = child.YoungerSibling;

                                child = olderSibling.YoungerSibling;
                            }
                        }
                        else
                        {
                            olderSibling = child;

                            child = child.YoungerSibling;
                        }
                    }
                }
            }

            return true;
        }
    }
}
