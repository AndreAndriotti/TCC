using Mono.Data.Sqlite; // 1
using System.Data; // 1
using UnityEngine;

public class SQLiteExample : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    [SerializeField] private int hitCount = 0;

    void Start() // 13
    {
        // Read all values from the table.
        Debug.Log("TOPPPP");
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 14
        Debug.Log("TOPPPP2");
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        Debug.Log("TOPPPP3");
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableSimple"; // 16
        Debug.Log("TOPPPP4");
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17

        while (dataReader.Read()) // 18
        {
            // The `id` has index 0, our `hits` have the index 1.
            Debug.Log("TOPPPP5");
            hitCount = dataReader.GetInt32(1); // 19
        }

        Debug.Log("TOPPPP6");
        // Remember to always close the connection at the end.
        dbConnection.Close(); // 20
    }
/*
    private void OnMouseDown()
    {
        hitCount++;

        // Insert hits into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 9
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTableSimple (id, hits) VALUES (0, " + hitCount + ")"; // 10
        dbCommandInsertValue.ExecuteNonQuery(); // 11

        // Remember to always close the connection at the end.
        Debug.Log("HITCOUT: " + hitCount);
        dbConnection.Close(); // 12
    }
*/
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

    void Update(){
        if(Input.GetMouseButtonDown(0)){
           hitCount++;

        // Insert hits into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 9
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTableSimple (id, hits) VALUES (0, " + hitCount + ")"; // 10
        dbCommandInsertValue.ExecuteNonQuery(); // 11

        // Remember to always close the connection at the end.
        Debug.Log("HITCOUT: " + hitCount);
        dbConnection.Close(); // 12 
        }
    }
}