using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back_mascota.Models
{
    public class Mascota
    {
        public int idMascotas { get; set; }
        public string  nombre { get; set; }
        public int edad { get; set; }
        public string descripcion { get; set; }
    
        public Mascota() { }

        public Mascota(int idMascotas, string nombre, int edad, string descripcion)
        {
            this.idMascotas = idMascotas;
            this.nombre = nombre;
            this.edad = edad;
            this.descripcion = descripcion;
        }

        public Mascota(string nombre, int edad, string descripcion)
        {
            this.nombre = nombre;
            this.edad = edad;
            this.descripcion = descripcion;
        }

    }


}