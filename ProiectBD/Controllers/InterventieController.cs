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
    public class InterventieController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public InterventieController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/Interventie/{id}")]
        public Interventie GetInterventie(int id)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Interventii WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                var interventie = new Interventie(reader, conn);

                return interventie;
            }

        }


        [HttpGet("/api/Interventie")]
        public List<Interventie> GetInterventii()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Interventii", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Interventie> interventii = new List<Interventie>();

                while (reader.Read())
                {
                    Interventie p = new Interventie(reader, conn);
                    interventii.Add(p);
                }

                return interventii;
            }
        }

        [HttpPost("/api/Interventie")]
        public IActionResult CreateInterventie([FromBody] Interventie interventie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SqlCommand cmd = new SqlCommand("INSERT INTO Interventii VALUES (@nume, @descriere, @data, @consultatieID)", conn);

            cmd.Parameters.AddWithValue("@nume", interventie.Nume);
            cmd.Parameters.AddWithValue("@descriere", interventie.Descriere);
            cmd.Parameters.AddWithValue("@data", interventie.Data);
            cmd.Parameters.AddWithValue("@consultatieID", interventie.ConsultatieID);


            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

        [HttpPut("/api/Interventie/{id}")]
        public IActionResult UpdateInterventie(int id, [FromBody] Interventie interventie)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Interventii SET Descriere = @descriere, Data = @data, Nume=@nume, ConsultatieID = @consultatieID  WHERE ID = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nume", interventie.Nume);
            cmd.Parameters.AddWithValue("@descriere", interventie.Descriere);
            cmd.Parameters.AddWithValue("@data", interventie.Data);
            cmd.Parameters.AddWithValue("@consultatieID", interventie.ConsultatieID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok();
        }

        [HttpDelete("/api/Interventie/{id}")]
        public IActionResult DeleteInterventie(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Interventii WHERE ID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }
    }
}