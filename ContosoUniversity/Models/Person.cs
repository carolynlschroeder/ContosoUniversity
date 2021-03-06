﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ContosoUniversity.Models
{
    public abstract class Person
    {
        public int ID { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]

        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName

        {
            get

            {

                return LastName + ", " + FirstMidName;

            }

        }

    }

}

