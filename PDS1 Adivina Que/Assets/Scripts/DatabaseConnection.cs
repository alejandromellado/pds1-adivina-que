using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;

public class DatabaseConnection : MonoBehaviour
{
    // el nombre de la db para que pueda ser accedido por todos los metodos
    private string dbName = "URI=file:AdivinaQue.db";

    void Start()
    {

    }

    // metodo para leer los nombres de las materias de las bases de datos
    private void LeerMaterias()
    {
        // crear conexion a la base de datos
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // crear un objeto (llamado 'command') para permitirnos controlar la base
            using (var command = connection.CreateCommand())
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
