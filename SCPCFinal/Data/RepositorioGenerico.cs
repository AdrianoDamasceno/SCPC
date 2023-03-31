using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Npgsql;

namespace SCPCFinal.Data
{
    public class RepositorioGenericoPost<T> where T : class
    {
        private string _conexao;
        public RepositorioGenericoPost(string conexao)
        {
            _conexao = String.IsNullOrEmpty(conexao) ? GetDb2Con() : conexao;
        }
        public string GetDataSCPCCon()
        {
            string host = "168.192.0.90";
            string username = "PACIENTE";
            string password = "PAC";
            string database = "datascpc";
            return "Host=" + host + ";Username=" + username + ";Password=" + password + ";Database=" + database;
        }
        public string GetDb2Con()
        {
            string host = "168.192.0.90";
            string username = "PACIENTE";
            string password = "PAC";
            string database = "db2";
            return "Host=" + host + ";Username=" + username + ";Password=" + password + ";Database=" + database;
        }

        public T GetItem(string key)
        {
            using (var db = new NpgsqlConnection(_conexao))
            {
                return db.Get<T>(key);
            }
        }

        public List<T> GetItemsSQL(string sql, object parametros)
        {
            using (var db = new NpgsqlConnection(_conexao))
            {
                return db.Query<T>(sql, parametros).ToList();  
            }
        }

        public List<T> GetItemsSQL(string sql)
        {
            using (var db = new NpgsqlConnection(_conexao))
            {
                return db.Query<T>(sql).ToList();
            }
        }
    }
}
