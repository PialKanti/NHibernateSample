using System;
using System.Collections.Generic;
using System.Text;

namespace NHibernateSample.Domain
{
    public class Person
    {
        public virtual int Id { get; protected set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual string Gender { get; set; }
    }
}
