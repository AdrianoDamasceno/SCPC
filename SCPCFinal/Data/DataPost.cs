using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;

namespace SCPCFinal.Data
{
    public class DataPost
    {
        private string _conexao;
        public DataPost(string conexao)
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

        public dynamic GetItem(string sql)
        {
            using (var db = new NpgsqlConnection(_conexao))
            {
                return db.Query(sql).FirstOrDefault();
            }
        }

        public void GetList(string sql, List<dynamic> retorno, object tipo)
        {
            using (var db = new NpgsqlConnection(_conexao))
            {
                retorno = db.Query(sql).ToList();
            }
        }
    }
}
