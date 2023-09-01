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

    private IDbConnection CreateAndOpenDatabase() // 3
    {
        var filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            Debug.Log("ENTROU");

        #if UNITY_ANDROID 
        Debug.Log("ENTROUUUUUUUUU");
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite");  // this is the path to your StreamingAssets in android
                var i = 1;
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath

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
                Debug.Log(filepath);
                Debug.Log("LoadDb: " + loadDb);
                Debug.Log("Bytes: " + loadDb.bytes);
                Debug.Log("isDone: " + loadDb.isDone); 
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        // Open a connection to the database.
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //string dbUri = StreamingAssetPathForReal() + "MyDatabase.sqlite";
        string dbUri = "URI=file:" + filepath; // 4
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
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        //dbConnection.Open(); // 6

        var filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            Debug.Log("ENTROU");

        #if UNITY_ANDROID 
        Debug.Log("ENTROUUUUUUUUU");
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite");  // this is the path to your StreamingAssets in android
                var i = 1;
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath

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
                Debug.Log(filepath);
                Debug.Log("LoadDb: " + loadDb);
                Debug.Log("Bytes: " + loadDb.bytes);
                Debug.Log("isDone: " + loadDb.isDone); 
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        // Open a connection to the database.
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //string dbUri = StreamingAssetPathForReal() + "MyDatabase.sqlite";
        string dbUri = "URI=file:" + filepath; // 4
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
        dbCommandCreateTable.CommandText = "DROP TABLE SituationsTracker"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void testInsertValues(){
        var filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            Debug.Log("ENTROU");

        #if UNITY_ANDROID 
        Debug.Log("ENTROUUUUUUUUU");
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite");  // this is the path to your StreamingAssets in android
                var i = 1;
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath

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
                Debug.Log(filepath);
                Debug.Log("LoadDb: " + loadDb);
                Debug.Log("Bytes: " + loadDb.bytes);
                Debug.Log("isDone: " + loadDb.isDone); 
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        // Open a connection to the database.
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //string dbUri = StreamingAssetPathForReal() + "MyDatabase.sqlite";
        string dbUri = "URI=file:" + filepath; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "INSERT INTO SituationsTracker (id_email, name, restaurante_situation) VALUES ('teste@email.com', 'André', 0)"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void UpdateSituation(string situationName){
        var filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            Debug.Log("ENTROU");

        #if UNITY_ANDROID 
        Debug.Log("ENTROUUUUUUUUU");
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite");  // this is the path to your StreamingAssets in android
                var i = 1;
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath

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
                Debug.Log(filepath);
                Debug.Log("LoadDb: " + loadDb);
                Debug.Log("Bytes: " + loadDb.bytes);
                Debug.Log("isDone: " + loadDb.isDone); 
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        // Open a connection to the database.
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //string dbUri = StreamingAssetPathForReal() + "MyDatabase.sqlite";
        string dbUri = "URI=file:" + filepath; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_situation = {situationName}_situation + 1"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public void SetSituationNumber(string situationName, int situationNumber){
        var filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            Debug.Log("ENTROU");

        #if UNITY_ANDROID 
        Debug.Log("ENTROUUUUUUUUU");
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite");  // this is the path to your StreamingAssets in android
                var i = 1;
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath

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
                Debug.Log(filepath);
                Debug.Log("LoadDb: " + loadDb);
                Debug.Log("Bytes: " + loadDb.bytes);
                Debug.Log("isDone: " + loadDb.isDone); 
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        // Open a connection to the database.
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //string dbUri = StreamingAssetPathForReal() + "MyDatabase.sqlite";
        string dbUri = "URI=file:" + filepath; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_situation = {situationNumber}"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        dbConnection.Close();
    }

    public int GetSituationNumber(string situationName){

        var filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            Debug.Log("ENTROU");

        #if UNITY_ANDROID 
        Debug.Log("ENTROUUUUUUUUU");
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite");  // this is the path to your StreamingAssets in android
                var i = 1;
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath

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
                Debug.Log(filepath);
                Debug.Log("LoadDb: " + loadDb);
                Debug.Log("Bytes: " + loadDb.bytes);
                Debug.Log("isDone: " + loadDb.isDone); 
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        // Open a connection to the database.
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //string dbUri = StreamingAssetPathForReal() + "MyDatabase.sqlite";
        string dbUri = "URI=file:" + filepath; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 6
        dbCommandReadValues.CommandText = $"SELECT {situationName}_situation FROM SituationsTracker WHERE (id_email = 'teste1@email.com');"; // 7
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
        var filepath = $"{Application.persistentDataPath}/MyDatabase.sqlite";

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            Debug.Log("ENTROU");

        #if UNITY_ANDROID 
        Debug.Log("ENTROUUUUUUUUU");
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "MyDatabase.sqlite");  // this is the path to your StreamingAssets in android
                var i = 1;
                while (!loadDb.isDone) {
                    Debug.Log("Bytes Downloaded: " + loadDb.bytesDownloaded);
                 }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath

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
                Debug.Log(filepath);
                Debug.Log("LoadDb: " + loadDb);
                Debug.Log("Bytes: " + loadDb.bytes);
                Debug.Log("isDone: " + loadDb.isDone); 
                File.WriteAllBytes(filepath, loadDb.bytes);
        #endif
        }
 
        // Open a connection to the database.
        //string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        //string dbUri = StreamingAssetPathForReal() + "MyDatabase.sqlite";
        string dbUri = "URI=file:" + filepath; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
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

    static string StreamingAssetPathForReal()
    {
        #if UNITY_EDITOR
        return "file://" + Application.dataPath + "/StreamingAssets/";
        #elif UNITY_ANDROID
        return "jar:file://" + Application.dataPath + "!/assets/";
        #elif UNITY_IOS
        return "file://" + Application.dataPath + "/Raw/";
        #endif
    }
}