using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ProiectBD.Models;
using System.Data;

namespace ProiectBD.Controllers
{
    public class ProprietarController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public ProprietarController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/Proprietar/{id}")]
        public Proprietar GetProprietar(int id)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Proprietari WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                var proprietar = new Proprietar(reader, conn);

                return proprietar;
            }

        }


        [HttpGet("/api/Proprietar")]
        public List<Proprietar> GetProprietari()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Proprietari", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Proprietar> personRecords = new List<Proprietar>();

                while (reader.Read())
                {
                    Proprietar p = new Proprietar(reader, conn);
                    personRecords.Add(p);
                }

                return personRecords;
            }
        }

        [HttpPost("/api/Proprietar")]
        public IActionResult CreateProprietar([FromBody] Proprietar proprietar)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SqlCommand cmd = new SqlCommand("INSERT INTO Proprietari VALUES (@Nume, @Prenume, @CNP, @SerieCI, @NumarCI, @Judet, @Localitate, @Adresa, @Telefon, @Email)", conn);
            //cmd.Parameters.Add("@Nume", SqlDbType.VarChar).Value = proprietar.Nume;
            cmd.Parameters.AddWithValue("@Nume", proprietar.Nume);
            cmd.Parameters.AddWithValue("@Prenume", proprietar.Prenume);
            cmd.Parameters.AddWithValue("@CNP", proprietar.CNP);
            cmd.Parameters.AddWithValue("@SerieCI", proprietar.SerieCI);
            cmd.Parameters.AddWithValue("@NumarCI", proprietar.NumarCI);
            cmd.Parameters.AddWithValue("@Judet", proprietar.Judet);
            cmd.Parameters.AddWithValue("@Localitate", proprietar.Localitate);
            cmd.Parameters.AddWithValue("@Adresa", proprietar.Adresa);
            cmd.Parameters.AddWithValue("@Telefon", proprietar.Telefon);
            cmd.Parameters.AddWithValue("@Email", proprietar.Email);


            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

        [HttpPut("/api/Proprietar/{id}")]
        public IActionResult UpdateProprietar(int id, [FromBody] Proprietar proprietar)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Proprietari SET Nume = @nume, Prenume = @prenume, CNP=@cnp, SerieCI = @serieCI, NumarCI=@numarCI," +
                " Judet = @judet, Localitate = @localitate , Adresa = @adresa, Telefon = @telefon, Email = @email WHERE ID = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nume", proprietar.Nume);
            cmd.Parameters.AddWithValue("@prenume", proprietar.Prenume);
            cmd.Parameters.AddWithValue("@cnp", proprietar.CNP);
            cmd.Parameters.AddWithValue("@serieCI", proprietar.SerieCI);
            cmd.Parameters.AddWithValue("@numarCI", proprietar.NumarCI);
            cmd.Parameters.AddWithValue("@judet", proprietar.Judet);
            cmd.Parameters.AddWithValue("@localitate", proprietar.Localitate);
            cmd.Parameters.AddWithValue("@adresa", proprietar.Adresa);
            cmd.Parameters.AddWithValue("@telefon", proprietar.Telefon);
            cmd.Parameters.AddWithValue("@email", proprietar.Email);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok();
        }

        [HttpDelete("/api/Proprietar/{id}")]
        public IActionResult DeleteProprietar(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Proprietari WHERE ID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

    }
}