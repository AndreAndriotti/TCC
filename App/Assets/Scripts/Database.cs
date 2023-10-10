using Mono.Data.Sqlite; // 1
using System.Data; // 1
using UnityEngine;
using System.IO;

public class Database : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    int situationNumber;
    string email;
    string userName;

    void Start() 
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

    private IDbConnection CreateAndOpenDatabase() 
    {
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, hits INTEGER )";
        dbCommandCreateTable.ExecuteReader(); 

        return dbConnection;
    }

    public void createUserDatabase()
    {
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS SituationsTracker (id_email VARCHAR(100) PRIMARY KEY, name VARCHAR(100), restaurante_situation INTEGER, restaurante_ops VARCHAR(100), restaurante_ops_attempts VARCHAR(100))";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public void createReportTable()
    {
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS ReportTracker(Cenario VARCHAR(30), Situacao INTEGER, Contexto_Situacao VARCHAR(200), Opcao_Escolhida VARCHAR(150), Tentativa INTEGER);";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public void deleteTableSituation(){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = "DROP TABLE SituationsTracker";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public void deleteTableReport() {
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = "DROP TABLE ReportTracker";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public void SetEmail(string email){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET id_email = '{email}'";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public string GetEmail(){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT id_email FROM SituationsTracker;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            email = dataReader.GetString(0); 
        }

        dbConnection.Close();

        return email;
    }

    public void SetUserName(string userName){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET name = '{userName}'";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public string GetUserName(){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT name FROM SituationsTracker;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            userName = dataReader.GetString(0); 
        }

        dbConnection.Close();

        return userName;
    }

    public void UpdateSituation(string situationName){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_situation = {situationName}_situation + 1";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public void SetSituationNumber(string situationName, int situationNumber){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_situation = {situationNumber}";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public int GetSituationNumber(string situationName){

        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT {situationName}_situation FROM SituationsTracker;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            situationNumber = dataReader.GetInt32(0); 
        }

        dbConnection.Close();

        return situationNumber;
    }

    public void SetSituationOptions(string situationName, string situationOps){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_ops = {situationOps}";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public string GetSituationOptions(string situationName){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT {situationName}_ops FROM SituationsTracker;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            userName = dataReader.GetString(0); 
        }

        dbConnection.Close();

        return userName;
    }
 
    public void SetSituationOpsAttempts(string situationName, string situationOpsAttempts){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

         
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = $"UPDATE SituationsTracker SET {situationName}_ops_attempts = {situationOpsAttempts}";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public string GetSituationOpsAttempts(string situationName){
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT {situationName}_ops_attempts FROM SituationsTracker;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            userName = dataReader.GetString(0); 
        }

        dbConnection.Close();

        return userName;
    }

    public void InsertIntoReportTrackerTable(string cenario_name, int situationNumber, string contexto_situacao, string opcao_escolhida, int tentativa) {
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = $"INSERT INTO REPORTTRACKER (cenario, situacao, contexto_situacao, opcao_escolhida, tentativa) values ('{cenario_name}', {situationNumber}, '{contexto_situacao}', '{opcao_escolhida}', {tentativa});";
        dbCommandCreateTable.ExecuteReader(); 

        dbConnection.Close();
    }

    public int GetSituationsTotalInScenario(string scenarioName) {
        int situationTotal = 0;
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT COUNT(DISTINCT Situacao) FROM ReportTracker WHERE cenario = '{scenarioName}';";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            situationTotal = dataReader.GetInt32(0); 
        }

        dbConnection.Close();

        return situationTotal;
    }

    public string GetOptionChoosen(string scenarioName, int numSituacao, int attempt) {
        string optionChoosen = "";
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT opcao_escolhida FROM ReportTracker WHERE cenario = '{scenarioName}' and situacao = {numSituacao} and tentativa = {attempt};";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            optionChoosen = dataReader.GetString(0); 
        }

        dbConnection.Close();

        return optionChoosen;
    }

    public int GetNumberOfTriesInSituation(string scenarioName, int numSituacao) {
        int numberOfTries = 0;
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT COUNT(*) FROM ReportTracker WHERE cenario = '{scenarioName}' and situacao = {numSituacao};";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            numberOfTries = dataReader.GetInt32(0); 
        }

        dbConnection.Close();

        return numberOfTries;
    }

    public string GetSituationContext(int numSituacao) {
        string context = "";
        IDbConnection dbConnection = openDatabaseConection();
        dbConnection.Open(); 

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT contexto_situacao FROM REPORTTRACKER WHERE situacao  = {numSituacao} GROUP BY contexto_situacao;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

         while (dataReader.Read()) 
        {
            
            context = dataReader.GetString(0); 
        }

        dbConnection.Close();

        return context;
    }

    void Update(){
        
    }  
}