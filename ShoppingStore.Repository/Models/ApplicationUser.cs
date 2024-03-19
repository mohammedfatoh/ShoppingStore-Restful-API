﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Repository.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100), MinLength(3)]
        public string FirstName { get; set; }

        [Required, MaxLength(100), MinLength(3)]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public byte[]? ProfilePicture { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
