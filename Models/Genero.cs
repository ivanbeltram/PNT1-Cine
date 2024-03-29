﻿using ORT_PNT1_Proyecto_2022_2C_I_ReservaEspectaculo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ORT_PNT1_Proyecto_2022_2C_I_ReservaEspectaculo.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = MensajesDeError.Requerido)]
        public string Nombre { get; set; }
        public List<Pelicula> Peliculas { get; set; }

        public Genero()
        {
            Peliculas = new List<Pelicula>();
        }
        public void AgregarPelicula(Pelicula pelicula)
        {
            Peliculas.Add(pelicula);
        }
        public bool EliminarPelicula(Pelicula pelicula)
        {
            return Peliculas.Remove(pelicula);
        }
    }
}