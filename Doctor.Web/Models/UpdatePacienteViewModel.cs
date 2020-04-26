﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Web.Models
{
    public class UpdatePacienteViewModel
    {
        public int PacienteId { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tamaño entre 3 a 50 caracteres")]
        public string Nombre { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tamaño entre 3 a 50 caracteres")]
        public string ApellidoPaterno { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tamaño entre 3 a 50 caracteres")]
        public string ApellidoMaterno { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tamaño entre 3 a 50 caracteres")]
        public string Direccion { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Tamaño entre 3 a 15 caracteres")]
        public string Telefono { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Tamaño entre 3 a 15 caracteres")]
        public string Celular { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tamaño entre 3 a 50 caracteres")]
        public string Correo { get; set; }


    }
}
