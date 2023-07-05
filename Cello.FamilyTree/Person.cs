namespace Cello.FamilyTree
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genders Gender { get; set; }

        public Person Spouse { get; set; }
        public Person FirstChild { get; set; }
        public Person YoungerSibling { get; set; }
        public Person Mother { get; set; }
        public Person Father { get; set; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Person(int id, string name, Genders gender)
        {
            Id = id;
            Name = name;
            Gender = gender;
        }
    }
}