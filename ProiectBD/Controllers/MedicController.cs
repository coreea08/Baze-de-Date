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
    [Produces("application/json")]
    [Route("api/Medic")]
    public class MedicController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public MedicController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/Medic/{id}")]
        public Medic GetMedic(int id)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Medici WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                var medic = new Medic(reader, conn);


                return medic;
            }
            

        }


        [HttpGet("/api/Medic")]
        public List<Medic> GetMedici()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Medici", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Medic> medici = new List<Medic>();

                while (reader.Read())
                {
                    Medic p = new Medic(reader, conn);
                    medici.Add(p);
                }

                return medici;
            }
        }

        [HttpPost("/api/Medic")]
        public IActionResult CreateMedic([FromBody] Medic medic)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SqlCommand cmd = new SqlCommand("INSERT INTO Medici VALUES (@Nume, @Prenume, @Telefon, @Email, @Adresa)", conn);
            //cmd.Parameters.Add("@Nume", SqlDbType.VarChar).Value = medic.Nume;
            cmd.Parameters.AddWithValue("@Nume", medic.Nume);
            cmd.Parameters.AddWithValue("@Prenume", medic.Prenume);
            cmd.Parameters.AddWithValue("@Adresa", medic.Adresa);
            cmd.Parameters.AddWithValue("@Telefon", medic.Telefon);
            cmd.Parameters.AddWithValue("@Email", medic.Email);


            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

        [HttpPut("/api/Medic/{id}")]
        public IActionResult UpdateMedic(int id, [FromBody] Medic medic)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SqlCommand cmd = new SqlCommand("UPDATE Medici SET Nume = @nume, Prenume = @prenume, Telefon = @telefon, Email = @email, Adresa = @adresa WHERE ID = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nume", medic.Nume);
            cmd.Parameters.AddWithValue("@prenume", medic.Prenume);
            cmd.Parameters.AddWithValue("@adresa", medic.Adresa);
            cmd.Parameters.AddWithValue("@telefon", medic.Telefon);
            cmd.Parameters.AddWithValue("@email", medic.Email);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok();
        }

        [HttpDelete("/api/Medic/{id}")]
        public IActionResult DeleteMedic(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Medici WHERE ID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }
    }
}