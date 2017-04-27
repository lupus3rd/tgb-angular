using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularDemo.Domain
{
    public static class Conversions
    {
        public static T ChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }

    public interface IEntity
    {
        //get the Id as a string
        string GetId();
    }
    public class Entity : EntityWithTypedId<int>
    {

    }

    public class EntityWithTypedId<T> : IEntity
    {
        [Key]
        public T Id { get; set; }

        public string GetId()
        {
            if (Id == null)
                return null;
            return Id.ToString();
        }


    }

    public class ValidationPatterns
    {
        public const string URL = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?";
        public const string EMAIL = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        public const string PNG = @"^.*\.png$";
        public const string SNAPSHOTFILE = @"^.*\.tse$";
        public const string ZIPFILE = @"^.*\.zip$";
        public const string TYRE_SIZE = @"^(P|LT|ST|T)?\d{3}(/\d{2,3})?(B|D|R)(?<size>\d+\.?\d*).*$";
        public const string DECIMAL = @"^\d+\.?\d*$";
    }
}   
