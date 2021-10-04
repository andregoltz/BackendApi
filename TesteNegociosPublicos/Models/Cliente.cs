using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Models
{
    public class Cliente
    {
        #region Atributes
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        #endregion

        #region Methods
        public static List<Cliente> Todos()
        {
            var lista = new List<Cliente>();

            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * FROM clientes", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString(),
                    CPF = reader["CPF"].ToString(),
                    Email = reader["Email"].ToString()
                });
            }

            conn.Close();
            conn.Dispose();
            cmd.Dispose();

            return lista;
        }
        public Cliente Salvar()
        {
            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Clientes(nome, CPF, email) values (@nome, @CPF, @email)", conn);

            cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar, 200) { Value = this.Nome });
            cmd.Parameters.Add(new SqlParameter("@CPF", System.Data.SqlDbType.VarChar, 11) { Value = this.CPF });
            cmd.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar, 100) { Value = this.Email });

            cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            return this;
        }
        public Cliente Atualizar()
        {
            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Clientes SET nome=@nome, CPF=@CPF, email=@email WHERE id=@id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = this.Id });
            cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar, 200) { Value = this.Nome });
            cmd.Parameters.Add(new SqlParameter("@CPF", System.Data.SqlDbType.VarChar, 11) { Value = this.CPF });
            cmd.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar, 100) { Value = this.Email });

            cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            return this;
        }
        public static bool Excluir(int id)
        {
            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Clientes WHERE id=@id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = id});

            cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            return true;
        }
        #endregion
    }
}
