using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

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

            SqlCommand cmd = new SqlCommand("SELECT * FROM clientes WHERE deletado = 0", conn);

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

            if (!string.IsNullOrEmpty(this.Nome) && !string.IsNullOrEmpty(this.CPF) && !string.IsNullOrEmpty(this.Email))
            {
                SqlCommand cmd1 = new SqlCommand("SELECT Id FROM Clientes Where CPF = @CPF and Deletado = 0", conn);
                cmd1.Parameters.Add(new SqlParameter("@CPF", System.Data.SqlDbType.VarChar, 14) { Value = this.CPF });

                SqlDataReader reader = cmd1.ExecuteReader();

                var cpf = reader.Read();

                reader.Close();
                cmd1.Dispose();

                if (!cpf)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Clientes Where email = @email and Deletado = 0", conn);
                    cmd2.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar, 100) { Value = this.Email.ToLower() });

                    SqlDataReader reader1 = cmd2.ExecuteReader();

                    var email = reader1.Read();

                    reader1.Close();
                    cmd2.Dispose();

                    if (!email)
                    {
                        var emailValid = IsValidEmail(this.Email);

                        if (emailValid)
                        {
                            var CPFFormatted = Convert.ToInt64(Regex.Replace(this.CPF, "[^0-9a-zA-Z]+", ""));
                            var cpfValid = Valida.Cpf(CPFFormatted);

                            if (cpfValid)
                            {
                                SqlCommand cmd = new SqlCommand("INSERT INTO Clientes(nome, CPF, email,deletado) values (@nome, @CPF, @email,@deletado)", conn);

                                cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar, 200) { Value = this.Nome });
                                cmd.Parameters.Add(new SqlParameter("@CPF", System.Data.SqlDbType.VarChar, 14) { Value = this.CPF });
                                cmd.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar, 100) { Value = this.Email.ToLower() });
                                cmd.Parameters.Add(new SqlParameter("@deletado", System.Data.SqlDbType.Bit) { Value = 0 });

                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                throw new Exception("CPF Inválido");
                            }

                        }
                        else
                        {
                            throw new Exception("Email Inválido");
                        }

                    }
                    else
                    {
                        throw new Exception("Já existe esse Email Cadastrado");
                    }
                }
                else
                {
                    throw new Exception("Já existe esse CPF Cadastrado");
                }

            }
            else
            {
                throw new Exception("Existem campos vazios");
            }
            conn.Dispose();
            conn.Close();
            return this;
        }
        public Cliente Atualizar()
        {
            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            if (!string.IsNullOrEmpty(this.Nome) && !string.IsNullOrEmpty(this.CPF) && !string.IsNullOrEmpty(this.Email))
            {
                var emailValid = IsValidEmail(this.Email);

                if (emailValid)
                {
                    var CPFFormatted = Convert.ToInt64(Regex.Replace(this.CPF, "[^0-9a-zA-Z]+", ""));
                    var cpfValid = Valida.Cpf(CPFFormatted);

                    if (cpfValid)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Clientes SET nome=@nome, CPF=@CPF, email=@email WHERE id=@id", conn);
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = this.Id });
                        cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar, 200) { Value = this.Nome });
                        cmd.Parameters.Add(new SqlParameter("@CPF", System.Data.SqlDbType.VarChar, 14) { Value = this.CPF });
                        cmd.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar, 100) { Value = this.Email });

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new Exception("CPF Inválido");
                    }

                }
                else
                {
                    throw new Exception("Email Inválido");
                }

            }
            else
            {
                throw new Exception("Existem campos vazios");
            }
            conn.Close();
            conn.Dispose();
            return this;
        }
        public static bool Excluir(int id)
        {
            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Clientes SET deletado = 1 WHERE id=@id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = id });

            cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        #endregion
    }
}
