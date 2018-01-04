using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ProiectBD.Models;
using ProiectBD.Models.Auxiliar;

namespace ProiectBD.Controllers
{
    public class RetetaController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public RetetaController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/Reteta/{id}")]
        public Reteta GetReteta(int id)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Retete WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                var reteta = new Reteta(reader, conn);

                return reteta;
            }

        }


        [HttpGet("/api/Reteta")]
        public List<Reteta> GetRetete()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Retete", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Reteta> retete = new List<Reteta>();

                while (reader.Read())
                {
                    Reteta p = new Reteta(reader, conn);
                    retete.Add(p);
                }

                return retete;
            }
        }


        [HttpGet("/api/reteteMedic/{medicId}")]
        public List<RetetaConsultatie> GetReteteMedic(int medicId)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT R.ID, R.Numar, R.Descriere, R.ConsultatieID, C.Descriere, C.Data " +
                "FROM Medici M, Consultatii C, Retete R WHERE M.ID = C.MedicID AND C.ID = R.ConsultatieID AND M.ID = @medicId ", conn);

            cmd.Parameters.AddWithValue("@medicId", medicId);
            cmd.Parameters.AddWithValue("@data", DateTime.Now);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var retete = new List<RetetaConsultatie>();

                while (reader.Read())
                {

                    var p = new RetetaConsultatie
                    {
                        ID = reader.GetInt32(0),
                        Numar = reader.GetString(1),
                        Descriere = reader.GetString(2),
                        ConsultatieID = reader.GetInt32(3),
                        DescriereConsultatie = reader.GetString(4),
                        DataConsultatie = reader.GetDateTime(5).Date
                    };
                    retete.Add(p);
                }

                return retete;
            }

        }

        [HttpPost("/api/Reteta")]
        public IActionResult CreateReteta([FromBody] Reteta reteta)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SqlCommand cmd = new SqlCommand("INSERT INTO Retete VALUES (@numar, @descriere, @consultatieID)", conn);

            cmd.Parameters.AddWithValue("@numar", reteta.Numar);
            cmd.Parameters.AddWithValue("@descriere", reteta.Descriere);
            cmd.Parameters.AddWithValue("@consultatieID", reteta.ConsultatieID);


            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

        [HttpPut("/api/Reteta/{id}")]
        public IActionResult UpdateReteta(int id, [FromBody] Reteta reteta)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Retete SET Descriere = @descriere, Numar=@numar, ConsultatieID = @consultatieID  WHERE ID = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@numar", reteta.Numar);
            cmd.Parameters.AddWithValue("@descriere", reteta.Descriere);
            cmd.Parameters.AddWithValue("@consultatieID", reteta.ConsultatieID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok();
        }

        [HttpDelete("/api/Reteta/{id}")]
        public IActionResult DeleteReteta(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Retete WHERE ID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }
    }
}