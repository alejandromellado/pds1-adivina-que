using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;

public class DatabaseConnection : MonoBehaviour
{
    // Variables
    private string dbName = "URI=file:AdivinaQue.db";   // almacena la direccion de la base de datos en el proyecto (raiz del proyecto)


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* Establece conexion a la base de datos para obtener la tabla de materias.
     * Retorna una lista con los nombres de todas las materias registradas. */
    public List<string> ObtenerMaterias()
    {
        List<string> materias = new List<string>();

        // Crear y abrir conexion a la base de datos usando la direccion al archivo
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de materias
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM materia;";

                // Ejecutar la consulta crea un objeto IDataReader que puede iterar sobre cada registro en la tabla que resulta de la consulta
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        materias.Add(reader["nombre"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return materias;
    }

    /* Establece conexion a la base de datos y obtiene la lista de temas de una materia.
     * Recibe un string con el nombre de la materia y retorna una lista con los nombres de los temas. */
    public List<string> ObtenerTemas(string materia)
    {
        List<string> temas = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de temas y el id de la materia usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tema WHERE (idMateria = (SELECT idMateria FROM materia WHERE (nombre = '" + materia + "')));";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        temas.Add(reader["nombre"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return temas;
    }

    /* Registra el nombre de un jugador en la base de datos, en caso de que ya este registrado el jugador el comando es ignorado. 
     * Recibe el nombre en forma de string. */
    public void RegistrarUsuario(string nombre)
    {
        int registrosCreados = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT OR IGNORE INTO jugador(nombre)VALUES(@nombre);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@nombre", nombre));

                registrosCreados = command.ExecuteNonQuery();
            }

            connection.Close();
        }

        Debug.Log("Se añadieron " + registrosCreados + " registros a la tabla 'jugador'.");
    }

    public List<string> CargarGaleria(string tema)
    {
        List<string> cartas = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tema WHERE (idTema = (SELECT idTema FROM idTema WHERE (titulo = @tema)));";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@tema", tema));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        cartas.Add(reader["titulo"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return cartas;
    }

    public List<string> ObtenerCartas(int idMateria)
    {
        List<string> cartas = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM carta WHERE idTema IN (SELECT idTema FROM tema WHERE(idMateria = @idMateria));";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idMateria", idMateria.ToString()));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        cartas.Add(reader["imagen"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return cartas;
    }
}
