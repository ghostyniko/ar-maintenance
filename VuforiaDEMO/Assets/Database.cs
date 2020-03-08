using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Threading;
using System.Globalization;

public class Database:MonoBehaviour
{
    private static string conn;

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            conn = "URI=file:" + Application.dataPath + "/database.s3db"; //Path to database.
        }
        else
        {
            conn = "URI=file:" + Application.persistentDataPath + "/database.s3db"; //Path to database.
        }

        Debug.Log("Database " + conn);
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "CREATE TABLE IF NOT EXISTS session (id INTEGER PRIMARY KEY AUTOINCREMENT,name TEXT)";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();

        sqlQuery = "CREATE TABLE IF NOT EXISTS object " +
            "(id INTEGER ,x REAL,y REAL,z REAL,a REAL,b REAL,c REAL,sx REAL,sy REAL,sz REAL,type TEXT, FOREIGN KEY (id) REFERENCES session(id))";

        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;



    }
    public static int  AddNewSession()
    {
        int id = GetLastId();
        id++;

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = string.Format("INSERT INTO session VALUES ({0},{1})", id, id);
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return id;
    }

    private static int GetLastId()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT * from session ORDER BY session.id DESC";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        int id = -1;
        while (reader.Read())
        {

            id = reader.GetInt32(0);
            break;
            
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null; 

        return id;

    }

    public static List<int> GetSessions()
    {

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT * from session ORDER BY session.id";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        List<int> sessions = new List<int>();

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            sessions.Add(id);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return sessions;

    }

    public static void AddObject(GameObject obj, int sessionID,string type)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        var objTrans = obj.transform;
        var pos = objTrans.localPosition;
        Vector3 rot = objTrans.localRotation.eulerAngles;
        Vector3 sc = objTrans.localScale;

        float x = pos.x;
        float y = pos.y;
        float z = pos.z;

        float a = rot.x;
        float b = rot.y;
        float c = rot.z;

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = string.Format("INSERT INTO object VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},'{10}')", sessionID, x,y,z,a,b,c,sc.x,sc.y,sc.z,type);
     //   Debug.LogError("bla "+sqlQuery);
        dbcmd.CommandText = sqlQuery;
       // Debug.LogError(dbcmd.CommandText);

        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;


    }


    public static List<ObjectPose> GetObjects(int sessionID)
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = string.Format("SELECT * FROM object WHERE id={0}",sessionID);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        List<ObjectPose> poses = new List<ObjectPose>();

        while (reader.Read())
        {
            var x = reader.GetFloat(1);
            var y = reader.GetFloat(2);
            var z = reader.GetFloat(3);
            var a = reader.GetFloat(4);
            var b = reader.GetFloat(5);
            var c = reader.GetFloat(6);
            var sx = reader.GetFloat(7);
            var sy = reader.GetFloat(8);
            var sz = reader.GetFloat(9);
            var type = reader.GetString(10);

            poses.Add(new ObjectPose(x, y, z, a, b, c,sx,sy,sz,type));
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return poses;

    }
}
