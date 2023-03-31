using SCPCRazor.Model;
using Npgsql;
using System.Data.SqlClient;
using Dapper;
using SCPCFinal.Models;
using System.Collections.Generic;

namespace SCPCRazor.Data
{
    public class DataSCPCRepo
    {
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

        public Operador Autenticar(string Usuario, string Senha)
        {
            if ((!string.IsNullOrEmpty(Usuario)) && (!string.IsNullOrEmpty(Senha)))
            {
                using (var db = new NpgsqlConnection(GetDataSCPCCon()))
                {
                    string sql = "SELECT * FROM datap.cadope WHERE operador = @Operador and senha = @Senha";
                    var item = db.Query<Operador>(sql, new { Operador = Usuario, Senha = Senha }).FirstOrDefault();

                    if (item != null) return item;
                }
            }

            return null;
        }
        public List<cadpacEntity> BuscarListaPaciente()
        {
            var retorno = new List<cadpacEntity>();
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = "SELECT * FROM cadpac ORDER BY codpac DESC LIMIT 100000";
                retorno = db.Query<cadpacEntity>(sql).ToList();

            }


            return retorno;
        }

        public List<PacienteModel> BuscarListaPacienteModel()
        {
            var retorno = new List<PacienteModel>();
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = @"SELECT codpac as ""Prontuario"", nomepac as ""Nome"", datanasc as ""DataNasc"", planopac as ""Plano"", nomemae as ""Filiacao"", pasta as ""Pasta"", extra07 as ""extra07"", rgpac as ""RG"", cartaosus as ""CartaoSUS"", matricpac as ""Matricula"", coduni as ""Unid"", cpfpac as ""CPF"" FROM cadpac ORDER BY codpac DESC 
                    LIMIT 300000";
                retorno = db.Query<PacienteModel>(sql).ToList();

            }


            return retorno;
        }

        public List<PacienteModel> BuscarListaPacienteModel(string chave, string parametro)
        {
            var retorno = new List<PacienteModel>();
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = @$"SELECT codpac, nomepac, datanasc, planopac, nomemae, pasta, extra07, rgpac, cartaosus, matricpac, coduni, cpfpac 
                            FROM cadpac 
                            WHERE {chave}={parametro} ORDER BY codpac DESC LIMIT 300000";
                retorno = db.Query<PacienteModel>(sql).ToList();

            }


            return retorno;
        }

        public List<EspecialidadesDto> BuscarEspecialidades()
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = @"SELECT codesp as ""Codigo"", nomeesp as ""Descricao""";

                return db.Query<EspecialidadesDto>(sql).ToList();
            }
        }

        public FormularioModel BuscarFormulario(string codpac)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = @$"SELECT codpac as ""NumProntuario"", nomepac as ""NomePaciente"", nomepac as ""NomeSocial"", datanasc as ""DataNasc"", logradouro as ""Endereco"", numlogr as ""NumEndereco"", b.nombai as ""Bairro"", m.nomemun as ""Municipio"", ceppac as ""CEP"", estadociv as ""EstadoCivil"", m.estado as ""UF"", nomepai as ""NomePai"", nomemae as ""NomeMae"", coduni as ""RacaCor"", extra07 as ""extra07"", sexo as ""Sexo"", coduni as ""Profissao"", rgpac as ""RG"", cpfpac as ""CPF"", foneresid as ""Telefone"", observpac as ""Observacao"" FROM cadpac p
                    LEFT JOIN cadbai b ON p.codbai = b.codbai
                    LEFT JOIN cadmun m ON m.codmun = p.codmun

                            WHERE codpac={codpac} ORDER BY nomepac DESC LIMIT 300000";
                return db.Query<FormularioModel>(sql).FirstOrDefault();

            }


            return null;
        }

        public List<ConsumoProdutosModel> BuscarConsumoProdutos(string codpac)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT cp.datacons AS ""Data"", cp.codprod AS ""Codigo"", pr.descricao AS ""Descricao"", pr.unidade AS ""Unidade"", cp.qtdcons AS ""Quantidade"", cc.nomecc AS ""CentroCusto"" FROM 
                    conspac cp
                    INNER JOIN arqatend a ON cp.numatend = a.numatend
                    INNER JOIN cadpac p ON p.codpac = a.codpac
                    INNER JOIN cadcc cc ON cc.codcc = cp.codcc
                    INNER JOIN tabprod pr ON pr.codprod = cp.codprod
                    WHERE 1 = 1
                    AND a.codpac = {codpac}
                    AND tipoproc = 'P'
                    ORDER BY cp.datacons
                    LIMIT 4000
                            ";
                return db.Query<ConsumoProdutosModel>(sql).ToList();

            }
            return null;
        }

        public List<ConsumoServicosModel> BuscarConsumoServicos(string codpac)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT cp.datacons AS ""Data"", cp.codintsv AS ""Codigo"", sv.descintsv AS ""Descricao"", cp.qtdcons AS ""Quantidade"", cc.nomecc AS ""CentroDeServico"" FROM
                    conspac cp
                    INNER JOIN arqatend a ON cp.numatend = a.numatend
                    INNER JOIN cadpac p ON p.codpac = a.codpac
                    INNER JOIN cadcc cc ON cc.codcc = cp.codcc
                    --INNER JOIN tabprod pr ON pr.codprod = cp.codprod
                    INNER JOIN tabintsv sv ON sv.codintsv = cp.codintsv

                    WHERE 1 = 1
                    AND a.codpac = {codpac}
                    AND tipoproc = 'S'
                    ORDER BY cp.datacons
                    LIMIT 4000
                    ";
                return db.Query<ConsumoServicosModel>(sql).ToList();
            }
            return null;
        }

        public List<ExamesModel> BuscarExames(string codpac)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT cabserv.numatend AS ""NumAtend"", cabserv.numreqserv AS ""NumReq"", cadprest.nomeprest AS ""PrestSol"", cabserv.datasol AS ""DataSol"", 
                        i.numitem AS ""NumItem"", tabintsv.descintsv AS ""Descricao"", i.qtdsol AS ""QtdSol"", i.datarlz AS ""DataRlz"", r.textresult AS ""TextoResultado""
                        FROM cabserv
                        JOIN arqatend ON cabserv.numatend = arqatend.numatend
                        JOIN cadpac ON cadpac.codpac = arqatend.codpac
                        JOIN itmserv i ON cabserv.numreqserv = i.numreqserv
                        JOIN tabintsv ON tabintsv.codintsv = i.codsvsol
                        JOIN cadprest ON cadprest.codprest = cabserv.prestsol
                        JOIN result r ON r.numreqserv = i.numreqserv AND i.numitem = r.numitem
                        WHERE 1=1
                        AND cadpac.codpac = {codpac}
                        ORDER BY cabserv.datasol
                        LIMIT 60000
                        ";
                return db.Query<ExamesModel>(sql).ToList();
            }
            return null;
        }

        public List<PrescricaoModel> BuscarPrescricao(string codpac)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT
                itmpresc.tipitemgru AS ""Tipo"", itmpresc.dataini AS ""DataIni"", cabpresc.numatend AS ""NumAtend"", cabpresc.numprescr AS ""NumPrescr"", itmpresc.numitem AS ""NumItem"", itmpresc.codprod AS ""CodProd"",
                itmpresc.codintsv AS ""CodIntSv"", tabprod.descricao AS ""Descricao"", tabintsv.descintsv AS ""DescIntSv"", itmpresc.descricao AS ""Descricao2"", itmpresc.cancelado AS ""Cancelado"", itmpresc.via AS ""Via"",
                itmpresc.periodo AS ""Periodo"", itmpresc.qtdtotal AS ""QtdTotal""
                FROM cabpresc 
                JOIN arqatend ON arqatend.numatend = cabpresc.numatend
                JOIN cadpac ON cadpac.codpac = arqatend.codpac
                JOIN itmpresc ON cabpresc.numprescr = itmpresc.numprescr
                LEFT JOIN tabprod ON tabprod.codprod = itmpresc.codprod
                LEFT JOIN tabintsv ON tabintsv.codintsv = itmpresc.codintsv
                WHERE 1=1

                AND cadpac.codpac = {codpac}
                ORDER BY itmpresc.dataini   
                LIMIT 5000";
                return db.Query<PrescricaoModel>(sql).ToList();
            }
            return null;
        }

        public List<EvolucaoModel> BuscarEvolucao(string codpac)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT a.numatend AS ""NumAtend"", m.numseq AS ""NumSeq"", m.datagrav AS ""DataGrav"", s.nomeprest AS ""NomePrest"", t.nometexto AS ""NomeTexto"", m.corpotexto AS ""CorpoTexto""
                FROM arqatend a
                JOIN cadpac p ON a.codpac = p.codpac
                JOIN evomed m ON a.numatend = m.numatend
                JOIN cadprest s ON m.codprest = s.codprest
                JOIN cadtexto t ON m.numtexto = t.numtexto
                WHERE 1=1
                AND p.codpac = {codpac}
                UNION
                SELECT a.numatend AS ""NumAtend"", e.numseq AS ""NumSeq"", e.datagrav AS ""DataGrav"", s.nomeprest AS ""NomePrest"", t.nometexto AS ""NomeTexto"", e.corpotexto AS ""CorpoTexto""
                FROM arqatend a
                JOIN cadpac p ON a.codpac = p.codpac
                JOIN evoenf e ON e.numatend = a.numatend
                JOIN cadprest s ON e.codprest = s.codprest
                JOIN cadtexto t ON e.numtexto = t.numtexto
                WHERE 1=1
                AND p.codpac = {codpac}
                LIMIT 9990";
                return db.Query<EvolucaoModel>(sql).ToList();
            }
            return null;
        }

        public List<AtendimentoModel> BuscarAtendimento(string codpac)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT a.numatend as ""NumAtend"", a.tipoatend as ""TipoAtend"", a.datasai as ""DataSai"", a.datatend as ""DataAtend"", a.codplaco as ""CodPlano"", cs.nomeserv as ""NomeServ"", cc.nomecc as ""CentroCusto""
                     , cp.nomeprest as ""NomePrest""
                    FROM arqatend a
                    INNER JOIN cadserv cs ON a.codserv = cs.codserv
                    INNER JOIN cadcc cc ON cc.codcc = a.codcc
                    INNER JOIN cadprest cp ON cp.codprest = a.codprest
                    WHERE 1=1
                    AND a.codpac = {codpac}
                    ORDER BY a.numatend DESC
                    LIMIT 99990";
                return db.Query<AtendimentoModel>(sql).ToList();
            }
            return null;
        }

        public List<CentroCustoModel> BuscarCentroCusto()
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT codesp as ""Codigo"", nomeesp as ""Descricao"" 
                FROM cadesp 
                LIMIT 99990";

                return db.Query<CentroCustoModel>(sql).ToList();
            }
        }

        public List<EspecialidadesModel> BuscarEspecialidadesM()
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT codcc as ""Codigo"", nomecc as ""Descricao"" 
                FROM cadcc 
                LIMIT 99990";

                return db.Query<EspecialidadesModel>(sql).ToList();
            }
        }

        public List<ConvenioModel> BuscarConvenio()
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT codserv as ""Codigo"", nomeserv as ""Descricao"" 
                FROM cadserv
                LIMIT 99990";
                return db.Query<ConvenioModel>(sql).ToList();
            }
        }

        public List<ServicoModel> BuscarServico()
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT codconv as ""Codigo"", razaoconv ""Descricao"" 
                FROM cadconv 
                LIMIT 99990";
                return db.Query<ServicoModel>(sql).ToList();
            }
        }

        public List<InternacaoPeriodoModel> BuscarInternacao(DateTime dtendtrada, DateTime dtsaida)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT a.numatend as ""NumAtend"", a.tipoatend as ""TipoAtend"", p.nomepac as ""Paciente"", a.datatend as ""DataAtend"",  a.datasai as ""DataSai"", a.codplaco as ""CodPlano"", a.codserv as ""NomeServ"", cc.nomecc as ""CentroCusto"", i.codlei as ""Leito""
                FROM arqatend a
                INNER JOIN arqint i ON a.numatend = i.numatend
                INNER JOIN cadpac p ON a.codpac = p.codpac
                INNER JOIN cadserv cs ON a.codserv=cs.codserv
                INNER JOIN cadcc cc ON cc.codcc = a.codcc
                INNER JOIN cadprest cp ON cp.codprest = a.codprest
                WHERE a.tipoatend = 'I'
                AND a.datatend < '{dtendtrada.ToString("yyyy-MM-dd HH:mm:ss")}'
                AND a.datasai >= '{dtsaida.ToString("yyyy-MM-dd HH:mm:ss")}'
                ORDER BY i.codlei
                LIMIT 5000";
                return db.Query<InternacaoPeriodoModel>(sql).ToList();
            }
        }

        public List<TransferenciaPeriodoModel> BuscarTransferencia(string numatend)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT t.numatend as ""NumAtend"", t.datahora as ""DataHora"", t.codlei as ""CodLei"", t.dtentrada as ""DTEntrada"" 
                FROM transfin t 
                WHERE t.numatend = '{numatend}'

                LIMIT 5000

                ";

                return db.Query<TransferenciaPeriodoModel>(sql).ToList();
            }
        }
        public List<PacientesCaconModel> BuscarPacientesCacon()
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT codpac, nomepac, datanasc, extra01, extra02, extra03, extra04, extra05, extra06, extra07, extra08, extra09, extra10, extra11, extra12
                  FROM cadpac WHERE extra08 is not null
                  ORDER BY nomepac
                 LIMIT 20000";

                return db.Query<PacientesCaconModel>(sql).ToList();
            }
        }
        public List<ProdutosPeriodoModel> BuscarProdutosPeriodo(string codpac, string data1, string data2)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"SELECT cs.numatend, cs.datacons, cc.nomecc, p.descricao, p.customedio as ""ValorUni"", cs.qtdcons as ""Quantidade""
                FROM conspac cs JOIN arqatend a ON cs.numatend = a.numatend JOIN cadpac pac ON a.codpac = pac.codpac
                JOIN cadcc cc ON cs.codcc = cc.codcc
                LEFT JOIN
                tabprod p ON cs.codprod = p.codprod JOIN itmentr ie ON ie.codprod = cs.codprod JOIN cabentr ce ON ce.numentr = ie.numentr
                WHERE 1 = 1
                AND pac.codpac = { codpac}
                AND cs.datacons BETWEEN '{data1}' AND '{data2}'
                AND ce.tipomov = 'E1'
                GROUP BY cs.numatend, cs.datacons, pac.nomepac, cc.nomecc, p.descricao, p.customedio, cs.qtdcons
                LIMIT 10000";

                return db.Query<ProdutosPeriodoModel>(sql).ToList();
            }
        }

        public List<ServicosPeriodoModel> BuscarServicosPeriodo(string codpac, string data1, string data2)
        {
            using (var db = new NpgsqlConnection(GetDb2Con()))
            {
                string sql = $@"
                SELECT cs.numatend, cs.datacons, cc.nomecc, s.descintsv, tbp.valorhosp + tbp.valorprof + tbp.valorsadt + tbp.valorane as ""ValorUni"", cs.qtdcons as ""Quantidade""
                    FROM conspac cs JOIN arqatend a ON cs.numatend = a.numatend JOIN cadpac pac ON a.codpac = pac.codpac JOIN cadcc cc ON cs.codcc = cc.codcc
                    LEFT JOIN
                    tabintsv s ON s.codintsv = cs.codintsv JOIN tabama ta ON s.codintsv = ta.codintsv JOIN tabpprop tbp ON ta.codsppadr = tbp.codsppadr
                    WHERE cs.datacons BETWEEN '{data1}' AND '{data2}'
                    AND tbp.codtbpprop = 'B4'
                    AND pac.codpac = { codpac }
                    GROUP BY cs.numatend, cs.datacons, cc.nomecc, s.descintsv, cs.qtdcons, tbp.valorhosp + tbp.valorprof + tbp.valorsadt + tbp.valorane
                    LIMIT 100000";
                return db.Query<ServicosPeriodoModel>(sql).ToList();
            }
        }
    }

}
