using System;

namespace ECTurbo.Codigos
{
    public class BuscaCNPJ
    {
        public string TipoEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Porte { get; set; }
        public string NaturezaJuridica { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string SituacaoCadastral { get; set; }
        public string DataAbertura { get; set; }

        public string AtividadePrincipal { get; set; }
        public dynamic AtividadesSecundarias { get; set; }
        public string Estado { get; set; }

        public bool Buscar(string Cnpj)
        {
            //Tratamento para cnpj com mais ou menos que 14 digitos
            //00.000.000/0000-00
            string c = Cnpj.Replace(".", "")
                        .Replace("/", "")
                        .Replace("-", "")
                        .Replace(" ", "")
                        .Replace(",", "");

            if (c.Length == 0)
                return false;

            if (c.Length != 14)
            {
                Funcoes.MsgErro("O CNPJ informado precisa conter 14 números.");
                return false;
            }

            string url = "https://brasilapi.com.br/api/cnpj/v1/" + c;
            dynamic Resposta = APIs.Requisicao(url);


            if (Resposta == null)
            {
                Funcoes.MsgErro("Não foi possível consultar o CNPJ.");
                return false;
            }

            if (Resposta.message != null)
            {
                string msg = Resposta.message.ToString().Replace(".", "")
                       .Replace("/", "")
                       .Replace("-", "")
                       .Replace(" ", "")
                       .Replace(",", "");

                if (msg == "CNPJ" + Cnpj + "inválido")
                {
                    Funcoes.MsgErro("O CNPJ " + Cnpj + " é inválido");
                    return false;
                }

                if (msg == "CNPJ" + Cnpj + "nãoencontrado")
                {
                    Funcoes.MsgErro("O CNPJ " + Cnpj + " não foi localizado");
                    return false;
                }

            }

            TipoEmpresa = Resposta.descricao_identificador_matriz_filial.ToString();
            NomeFantasia = Resposta.nome_fantasia.ToString();
            RazaoSocial = Resposta.razao_social.ToString();

            Porte = Resposta.porte.ToString();
            NaturezaJuridica = Resposta.natureza_juridica.ToString();
            Logradouro = Resposta.logradouro.ToString();

            Numero = Resposta.numero.ToString();
            Bairro = Resposta.bairro.ToString();
            Complemento = Resposta.complemento.ToString();

            Cep = Resposta.cep.ToString();
            Municipio = Resposta.municipio.ToString();
            UF = Resposta.uf.ToString();

            Estado = Funcoes.PegarEstadoUF(UF);

            Email = Resposta.email.ToString();
            Telefone = Resposta.ddd_fax.ToString();

            SituacaoCadastral = Resposta.descricao_situacao_cadastral.ToString();

            DataAbertura =
                Convert.ToDateTime(Resposta.data_inicio_atividade.ToString()).ToString("dd/MM/yyyy");

            AtividadePrincipal = Resposta.cnae_fiscal_descricao.ToString();
            AtividadesSecundarias = Resposta.cnaes_secundarios;

            return true;
        }
    }
}
