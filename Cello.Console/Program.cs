// See https://aka.ms/new-console-template for more information
using Cello.FamilyTree;

var family = FamilyTree.CreateFamilyTree(0, "rootMother", 1, "rootFather");

family.HaveKid(0, 1, 2, "female1", Genders.Female);

family.Marry(2, 3, "otherMale1", Genders.Male);

family.HaveKid(2, 3, 4, "male2", Genders.Male);

family.HaveKid(2, 3, 5, "female2", Genders.Female);

//family.Divorce(2, 3, Custodies.WithFather);

family.Divorce(2, 3, Custodies.WithMother);

family.Marry(2, 6, "otherMale2", Genders.Male);

family.HaveKid(2, 6, 7, "male3", Genders.Male);

//family.Divorce(2, 6, Custodies.WithMother);

family.HaveKid(0, 1, 8, "male4", Genders.Male);

//family.HaveKid(2, 3, 9, "female3", Genders.Female);

family.Marry(8, 10, "otherFemale1", Genders.Female);

family.Marry(5, 11, "otherMale3", Genders.Male);

family.HaveKid(5, 11, 12, "male5", Genders.Male);

Console.WriteLine(ASCIIGraph.Show(family));

Console.ReadLine();
