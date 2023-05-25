using CoolAppInTheCloud.Data.Models;

namespace CoolAppInTheCloud.Helpers_Extensions
{
    public static class Helpers
    {

        public static List<Person> ReadPeopleFromFile()
        {
            //string fileName = "PeopleList.txt";
            string fileName = "ModifiedPeopleList.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            List<Person> people = new List<Person>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip the header line if it exists
                if (!reader.EndOfStream)
                    reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {

                        string[] values = line.Split(',');
                        var favFoods = new List<string>
                        {
                            values[6],
                            values[14]
                        };
                        bool beenInKristiansand = values[15].ToLower() == "yes" ? true : false;
                        bool likesBaguettes = values[16].ToLower() == "yes" ? true : false;

                        // Create a new Person object and populate its properties
                        Person person = new Person
                        {
                            Name = values[0],
                            IdentifiesAs = values[1],
                            Age = int.Parse(values[2]),
                            City = values[3],
                            Country = values[4],
                            Occupation = values[5],
                            FavoriteFoods = favFoods,
                            ShoeSize = int.Parse(values[7]),
                            HairColor = values[8],
                            RealHairColor = values[9],
                            EyeColor = values[10],
                            WatchBrand = values[11],
                            CellPhoneBrand = values[12],
                            FavoriteDrink = values[13],
                            BeenInKristiansand = beenInKristiansand,
                            LikeBaguettes = likesBaguettes,
                            CoffeeContainer = (CoffeeContainer)Enum.Parse(typeof(CoffeeContainer), values[17])
                        };

                        people.Add(person);
                    }
                }
            }
            return people;

        }
    }
}
