using MyNotes.Config;
using MyNotes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNotes.Controllers
{
    public class NoteController : Controller
    {
        public ActionResult Create()
        {
            return View(new Note { ID = 0 });
        }

        [HttpPost]
        public ActionResult Create(Note note)
        {
            //string insertSQL = "INSERT INTO Notes(NoteHeader,NoteContent,NoteTime,DateTime.IsStar) VALUES('" + note.NoteHeader + "','" + note.NoteContent + "','" + note.NoteTime + "','" + note.IsStar + "')";
            //string updateSQL = "UPDATE Notes SET NoteHeader = '" + note.NoteHeader + "',NoteContent = '" + note.NoteContent + "', DateTime = '" + note.NoteTime + "', IsStar = '" + note.IsStar + "' ";

            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(DatabaseConnection.GetConnection()))
                {
                    //using (SqlCommand cmd = new SqlCommand((note.ID > 0) ? updateSQL : insertSQL, con))
                    using (SqlCommand cmd = new SqlCommand("Notes_SaveOrUpdate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", note.ID);
                        cmd.Parameters.AddWithValue("@NoteHeader", note.NoteHeader);
                        cmd.Parameters.AddWithValue("NoteContent", note.NoteContent);
                        cmd.Parameters.AddWithValue("DateTime", note.NoteTime);
                        cmd.Parameters.AddWithValue("IsStar", note.IsStar);

                        if (con.State != System.Data.ConnectionState.Open)
                            con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("GetAll");
            }
            return View("Create", note);
        }

        //[Route("student/search")]
        public ActionResult Search(string search)
        {
            List<Note> notes = GetNotes("Notes_SearchNotes", search);
            return View("GetAll", notes);
        }


        public List<Note> GetNotes(string storeProcedure, string search)
        {
            List<Note> notes = new List<Note>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.GetConnection()))
            {
                //using (SqlCommand cmd = new SqlCommand("SELECT * FROM Notes", con))
                using (SqlCommand cmd = new SqlCommand(storeProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (search != null)
                        cmd.Parameters.AddWithValue("@Filter", search);

                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtNotes = new DataTable();

                    dtNotes.Load(sdr);

                    foreach (DataRow row in dtNotes.Rows)
                    {
                        notes.Add(
                            new Note
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                NoteHeader = row["NoteHeader"].ToString(),
                                NoteContent = row["NoteContent"].ToString(),
                                NoteTime = Convert.ToDateTime(row["DateTime"]),
                                IsStar = Convert.ToInt32(row["IsStar"]) == 1 ? true : false
                            }
                        );
                    }
                }
            }
            return notes;
        }

        static int star = 0;
        public List<Note> GetNotes(string storeProcedure)
        {
            star = star == 1 ? 0 : 1;

            List<Note> notes = new List<Note>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.GetConnection()))
            {
                //using (SqlCommand cmd = new SqlCommand("SELECT * FROM Notes", con))
                using (SqlCommand cmd = new SqlCommand(storeProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                    cmd.Parameters.AddWithValue("@star", star);

                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtNotes = new DataTable();

                    dtNotes.Load(sdr);

                    foreach (DataRow row in dtNotes.Rows)
                    {
                        notes.Add(
                            new Note
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                NoteHeader = row["NoteHeader"].ToString(),
                                NoteContent = row["NoteContent"].ToString(),
                                NoteTime = Convert.ToDateTime(row["DateTime"]),
                                IsStar = Convert.ToInt32(row["IsStar"]) == 1 ? true : false
                            }
                        );
                    }
                }
            }
            return notes;
        }

        public ActionResult GetAll()
        {
            List<Note> notes = GetNotes("Notes_GetAllNote", null);


            return View(notes);
        }

        public ActionResult Star() //0:no star , 1: Yes Star
        {
            List<Note> notes = GetNotes("Notes_GetByStar");

            return View("GetAll", notes);
        }

        public ActionResult Delete(int id)
        {
            if (id < 0)
                return HttpNotFound();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.GetConnection()))
            {
                //using(SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ID = '"+ id +"' ",con))
                using (SqlCommand cmd = new SqlCommand("Notes_DeleteById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("GetAll");
        }

        public ActionResult Edit(int id)
        {
            if (id < 0)
                return HttpNotFound();

            var _note = new Note();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Notes_GetById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();

                    if (sdr.HasRows)
                    {
                        dt.Load(sdr);

                        DataRow row = dt.Rows[0];

                        _note.ID = Convert.ToInt32(row["ID"]);
                        _note.NoteHeader = row["NoteHeader"].ToString();
                        _note.NoteContent = row["NoteContent"].ToString();
                        _note.NoteTime = Convert.ToDateTime(row["DateTime"]);
                        _note.IsStar = Convert.ToInt32(row["IsStar"]) == 1 ? true : false;

                        return View("Create", _note);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
        }
    }
}































