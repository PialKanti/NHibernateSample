using FluentNHibernate.Mapping;
using NHibernateSample.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHibernateSample.FluentMaps
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(p => p.Id);
            Map(p => p.FirstName);
            Map(p => p.LastName);
            Map(p => p.Age);
            Map(p => p.Gender);
        }
    }
}
