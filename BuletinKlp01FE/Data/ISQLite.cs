using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BuletinKlp01FE.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
