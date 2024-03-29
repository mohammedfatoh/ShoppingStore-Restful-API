﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Repository.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string NameonCard { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required, CreditCard]
        public string CreditcardNumber { get; set; }

        public string? StatusOfOrder { get; set; }

        public string? Quantities { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

    }
}
