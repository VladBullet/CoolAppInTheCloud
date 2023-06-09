﻿using CoolAppInTheCloud.Data;
using CoolAppInTheCloud.Data.Models;
using System;
using System.Xml.Linq;

namespace CoolAppInTheCloud.Services
{

    public interface IPeopleService
    {
        List<Person> GetAllPeople();
        Person GetPersonByName(string name);
        bool AddPerson(Person person);
        bool DeletePerson(string id);
        bool UpdatePerson(Person person);
        List<Person> Filter(string filter);
    }

    public class PeopleService : IPeopleService
    {
        //private readonly CoolAppInTheCloudDbContext _dbContext;
        private readonly MockDatabase _db = MockDatabase.Instance;

        public List<Person> GetAllPeople()
        {
            return _db.People.ToList();
        }

        public bool UpdatePerson(Person person)
        {
            try
            {
                var dbPerson = GetPersonByName(person.Name);
                _db.People.Remove(dbPerson); // remove old user
                _db.People.Add(person); // add new user
                // await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Person GetPersonByName(string name)
        {
            return _db.People.FirstOrDefault(p => p.Name.Trim() == name.Trim());

        }


        bool IPeopleService.AddPerson(Person person)
        {
            try
            {
                person.Id = Guid.NewGuid().ToString();
                _db.People.Add(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Person> Filter(string filterString)
        {
            string filter = filterString.Trim().ToLower();
            return _db.People.Where(p => p.Name.ToLower().Contains(filter)
                    || p.CellPhoneBrand.ToLower().Contains(filter)
                    || p.City.ToLower().Contains(filter)
                    || p.Country.ToLower().Contains(filter)
                    || p.EyeColor.ToLower().Contains(filter)
                    || p.FavoriteDrink.ToLower().Contains(filter)
                    || p.HairColor.ToLower().Contains(filter)
                    || p.RealHairColor.ToLower().Contains(filter)
                )
                .ToList();

        }

        public bool DeletePerson(string id)
        {
            try
            {
                var person = _db.People.FirstOrDefault(x => x.Id == id);
                return _db.People.Remove(person);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
