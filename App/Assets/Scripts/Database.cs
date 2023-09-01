using Mono.Data.Sqlite; // 1
using System.Data; // 1
using UnityEngine;
using System.IO;

public class Database : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

     int situationNumber;
     string userName;

    void Start() // 13
    {
        
    }

    private IDbConnection openDatabaseConection()
    {
        string filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";
        Debug.Log(filepath);
        
        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");

        #if UNITY_ANDROID 
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite"); 
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 } 

                // Utilizado para Debug
                if (loadDb.error != null)
                {
                    Debug.LogError("Error: " + loadDb.error);
                }
                else
                { 
                    
                    var scriptText = loadDb.text;
                    var filesDownloaded = true;
                    Debug.Log(scriptText);
                }
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        string dbUri = "URI=file:" + filepath; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5

        return dbConnection;
    }

    private IDbConnection CreateAndOpenDatabase() // 3
    {
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, hits INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        return dbConnection;
    }

    public void createUserDatabase()
    {
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS SituationsTracker (id_email VARCHAR(100) PRIMARY KEY, name VARCHAR(100), restaurante_situation INTEGER, restaurante_ops VARCHAR(100))"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void deleteTableSituation(){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "DROP TABLE SituationsTracker"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void testInsertValues(){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "INSERT INTO SituationsTracker (id_email, name, restaurante_situation, restaurante_ops) VALUES ('teste@email.com', 'Carlos', 0, '00000000000')"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void UpdateSituation(string situationName){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_situation = {situationName}_situation + 1"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void SetSituationNumber(string situationName, int situationNumber){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_situation = {situationNumber}"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public int GetSituationNumber(string situationName){

        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 6
        dbCommandReadValues.CommandText = $"SELECT {situationName}_situation FROM SituationsTracker WHERE (id_email = 'teste@email.com');"; // 7
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) // 18
        {
            // The `id` has index 0, our `hits` have the index 1.
            situationNumber = dataReader.GetInt32(0); // 19
        }

        dbConnection.Close();

        return situationNumber;
    }

    public string GetUserName(){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); // 6

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 6
        dbCommandReadValues.CommandText = $"SELECT name FROM SituationsTracker WHERE (id_email = 'teste@email.com');"; // 7
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) // 18
        {
            // The `id` has index 0, our `hits` have the index 1.
            userName = dataReader.GetString(0); // 19
        }

        dbConnection.Close();

        return userName;
    }
 
    void Update(){
        
    }  
}