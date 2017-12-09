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

        [HttpGet("/api/ConsultatieMedic/{medicId}")]
        public List<Consultatie> GetConsultatiiMedic(int medicId)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Consultatii WHERE MedicID = @medicId", conn);
            cmd.Parameters.AddWithValue("@medicId", medicId);

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

        [HttpGet("/api/ConsultatieMedicNoua/{medicId}")]
        public List<Consultatie> GetConsultatiiNoiMedic(int medicId)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Consultatii WHERE MedicID = @medicId AND Data > @data", conn);
            cmd.Parameters.AddWithValue("@medicId", medicId);
            cmd.Parameters.AddWithValue("@data", DateTime.Now);

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

        [HttpGet("/api/ConsultatieMedicVeche/{medicId}")]
        public List<Consultatie> GetConsultatiiVechiMedic(int medicId)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Consultatii WHERE MedicID = @medicId AND Data < @data", conn);
            cmd.Parameters.AddWithValue("@medicId", medicId);
            cmd.Parameters.AddWithValue("@data", DateTime.Now);

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

        [HttpGet("/api/numeAnimal/{cnp}")]
        public List<ClasaAuxiliara> GetNumeAnimaleWithCNPProprietar(string cnp)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT ID, Nume FROM Animale WHERE ProprietarID = (SELECT ID FROM Proprietari WHERE CNP =  @cnp)", conn);
            cmd.Parameters.AddWithValue("@cnp", cnp);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var consultatii = new List<ClasaAuxiliara>();

                while (reader.Read())
                {

                    var p = new ClasaAuxiliara
                    {
                        ID = reader.GetInt32(0),
                        Nume = reader.GetString(1)
                    };
                    consultatii.Add(p);
                }

                return consultatii;
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