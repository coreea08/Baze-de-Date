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
    public class ConsultatieController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public ConsultatieController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/Consultatie/{id}")]
        public Consultatie GetConsultatie(int id)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Consultatii WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                var consult = new Consultatie(reader, conn);

                return consult;
            }

        }


        [HttpGet("/api/Consultatie")]
        public List<Consultatie> GetConsultatii()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Consultatii", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Consultatie> consultatii = new List<Consultatie>();

                while (reader.Read())
                {
                    Consultatie p = new Consultatie(reader, conn);
                    consultatii.Add(p);
                }

                return consultatii;
            }
        }

        [HttpPost("/api/Consultatie")]
        public IActionResult CreateConsultatie([FromBody] Consultatie consult)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SqlCommand cmd = new SqlCommand("INSERT INTO Consultatii VALUES (@descriere, @data, @observatii, @animalID, @medicID)", conn);

            cmd.Parameters.AddWithValue("@descriere", consult.Descriere);
            cmd.Parameters.AddWithValue("@data", consult.Data);
            cmd.Parameters.AddWithValue("@observatii", consult.Observatii);
            cmd.Parameters.AddWithValue("@animalID", consult.AnimalID);
            cmd.Parameters.AddWithValue("@medicID", consult.MedicID);


            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

        [HttpPut("/api/Consultatie/{id}")]
        public IActionResult UpdateConsultatie(int id, [FromBody] Consultatie consultatie)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Consultatii SET Descriere = @descriere, Data = @data, Observatii=@observatii, AnimalID=@animalID, MedicID = @medicID  WHERE ID = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@descriere", consultatie.Descriere);
            cmd.Parameters.AddWithValue("@data", consultatie.Data);
            cmd.Parameters.AddWithValue("@observatii", consultatie.Observatii);
            cmd.Parameters.AddWithValue("@animalID", consultatie.AnimalID);
            cmd.Parameters.AddWithValue("@medicID", consultatie.MedicID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok();
        }

        [HttpDelete("/api/Consultatie/{id}")]
        public IActionResult DeleteConsultatie(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Consultatii WHERE ID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }
    }
}