﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VelazquezYahir.Models.Domain
{
    public class Role
    {
        [Key]
        public int PkRole { get; set; }
        public string Nombre { get; set; }
    }
}