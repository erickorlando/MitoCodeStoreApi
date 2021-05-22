﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Dto
{
    public class CustomerDto 
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public string NumberId { get; set; }
    }
}