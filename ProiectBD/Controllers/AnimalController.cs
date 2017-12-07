using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ProiectBD.Models;

namespace ProiectBD.Controllers
{
    public class AnimalController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public AnimalController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/Animal/{id}")]
        public Animal GetAnimal(int id)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Animale WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                var animal = new Animal(reader, conn);

                return animal;
            }

        }

        [HttpGet("/api/Animale/{proprietarId}")]
        public List<Animal> GetAnimaleProrpietar(int proprietarId)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Animale WHERE ProprietarID = @proprietarId", conn);
            cmd.Parameters.AddWithValue("@proprietarId", proprietarId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Animal> animale = new List<Animal>();

                while (reader.Read())
                {
                    Animal p = new Animal(reader, conn);
                    animale.Add(p);
                }

                return animale;
            }

        }


        [HttpGet("/api/Animal")]
        public List<Animal> GetAnimale()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Animale", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Animal> animale = new List<Animal>();

                while (reader.Read())
                {
                    Animal p = new Animal(reader, conn);
                    animale.Add(p);
                }

                return animale;
            }
        }

        [HttpPost("/api/Animal")]
        public IActionResult CreateAnimal([FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //var animal = new Animal(animale);
         
            SqlCommand cmd = new SqlCommand("INSERT INTO Animale VALUES (@codMicrocip, @dataNasterii, @serieCarnet, @numarCarnet, @codPasaport, @Nume, @culoare, @rasa, @categorie, @sex, @proprietarID)", conn);

            cmd.Parameters.AddWithValue("@codMicrocip", animal.CodMicrocip);
            cmd.Parameters.AddWithValue("@dataNasterii", animal.DataNasterii);
            cmd.Parameters.AddWithValue("@serieCarnet", animal.SerieCarnet);
            cmd.Parameters.AddWithValue("@numarCarnet", animal.NumarCarnet);
            cmd.Parameters.AddWithValue("@codPasaport", animal.CodPasaport);
            cmd.Parameters.AddWithValue("@Nume", animal.Nume);
            cmd.Parameters.AddWithValue("@culoare", animal.Culoare);
            cmd.Parameters.AddWithValue("@rasa", animal.Rasa);
            cmd.Parameters.AddWithValue("@categorie", animal.Categorie);
            cmd.Parameters.AddWithValue("@sex", animal.Sex);
            cmd.Parameters.AddWithValue("@proprietarID", animal.ProprietarID);


            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

        [HttpPut("/api/Animal/{id}")]
        public IActionResult UpdateAnimal(int id, [FromBody] Animal animal)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Animale SET Nume = @nume, CodMicrocip = @codMicrocip, DataNasterii=@dataNasterii, SerieCarnet = @serieCarnet, NumarCarnet=@numarCarnet," +
                " CodPasaport = @codPasaport, Culoare = @culoare , Rasa = @rasa, Categorie = @categorie, Sex = @sex, ProprietarID = @proprietarID  WHERE ID = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@codMicrocip", animal.CodMicrocip);
            cmd.Parameters.AddWithValue("@dataNasterii", animal.DataNasterii);
            cmd.Parameters.AddWithValue("@serieCarnet", animal.SerieCarnet);
            cmd.Parameters.AddWithValue("@numarCarnet", animal.NumarCarnet);
            cmd.Parameters.AddWithValue("@codPasaport", animal.CodPasaport);
            cmd.Parameters.AddWithValue("@Nume", animal.Nume);
            cmd.Parameters.AddWithValue("@culoare", animal.Culoare);
            cmd.Parameters.AddWithValue("@rasa", animal.Rasa);
            cmd.Parameters.AddWithValue("@categorie", animal.Categorie);
            cmd.Parameters.AddWithValue("@sex", animal.Sex);
            cmd.Parameters.AddWithValue("@proprietarID", animal.ProprietarID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok();
        }

        [HttpDelete("/api/Animal/{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Animale WHERE ID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }
    }
}