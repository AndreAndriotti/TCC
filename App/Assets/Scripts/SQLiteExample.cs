using Mono.Data.Sqlite; // 1
using System.Data; // 1
using UnityEngine;

public class SQLiteExample : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    void Start() // 13
    {
        
    }

    private IDbConnection CreateAndOpenDatabase() // 3
    {
        // Open a connection to the database.
        string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, hits INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        return dbConnection;
    }

    public void createUserDatabase(){
        // Open a connection to the database.
        string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS SituationsTracker (id_email VARCHAR(100) PRIMARY KEY, name VARCHAR(100), restaurante_situation INTEGER)"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void deleteTableSituation(){
        string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "DROP TABLE SituationTracker"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void testInsertValues(){
        string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "INSERT INTO SituationsTracker (id_email, name, restaurante_situation) VALUES ('teste1@email.com', 'carlos', 1)"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    void Update(){
        
    }
}