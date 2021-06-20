using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyNotes.Config
{
    public class DatabaseConnection
    {
        public static string GetConnection()
        {
#error Databasei Bagla Web.config de sonra buradaki mesaji sil. (Indirilen dosyada databasei olusturmak icin CreateDatabase.sql konuldu.)
            return ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString;
        }
    }
}