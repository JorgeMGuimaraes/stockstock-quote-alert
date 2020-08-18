using System.Collections.Generic;
using System.IO;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System;

namespace stockstock_quote_alert {
    /// <summary>
    /// Realiza operacoes de envio de email.
    /// </summary>
    internal class Correio {
        #region Campos
        private MimeMessage mensagem { get; set; }
        private readonly Config confMail;
        private string CaminhoArquivo { get; } = "configs.txt";
        /// <summary>
        /// Configuracoes basicas copiadas do arquivo configs.txt em disco.
        /// </summary>
        private class Config {
            public string MailDe { get; set; }
            public string NomeDe { get; set; }
            public string MailPara { get; set; }
            public string NomePara { get; set; }
            public string MailSenha { get; set; }
            public string Assunto { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        }
        #endregion
        #region Constructor
        internal Correio() {
            confMail = LerConfigDeArquivo(CaminhoArquivo);
            if (ConfigVazia()) {
                Console.WriteLine("Favor preencher arquivo configs.txt com seus dados");
            }
        }

        #endregion
        #region Metodos
        /// <summary>
        /// Gera um objeto de mensagem baseado nas configuracoes do usuario.
        /// TODO: Pode-se implementar um template de corpo de email vindo de um arquivo.
        /// </summary>
        /// <param name="stock">nome da stock acompanhada.</param>
        /// <param name="tipo">tipo da operacao: compra ou venda</param>
        /// <param name="valor">valor fixado da operacao</param>
        /// <returns>Mensagem pronta para ser enviada</returns>
        private MimeMessage Mensagem(string stock, string tipo, double valor) {
            var mail        = new MimeMessage();
            mail.From.Add(new MailboxAddress(confMail.NomeDe, confMail.MailDe));
            mail.To.Add(new MailboxAddress(confMail.NomePara, confMail.MailPara));
            mail.Subject    = $"{confMail.Assunto}: {stock} atingiu valor de {tipo}";
            mail.Body       = new TextPart("plain") {
                Text        = $@"Sr. {confMail.NomePara},
    Conforme suas configurações de compra e venda a stock {stock} atingiu o preço de {tipo}, de {valor}.

{confMail.NomeDe}"
            };
            return mail;
        }
        /// <summary>
        /// Usa os parametros de mensagem para enviar um email ao usuario
        /// TODO: Pode-se generalizar o metodo afim de enviar mesagens para tipos diferentes de servidores, ex.: EnviarMensagem<TipoServidor>.
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="tipo"></param>
        /// <param name="valor"></param>
        public void EnviarMensagem(string stock, string tipo, double valor) {
            using (var cliente = new SmtpClient()) {
                cliente.Connect(confMail.Host, confMail.Port, false);
                cliente.Authenticate(confMail.MailDe, confMail.MailSenha);
                cliente.Send(Mensagem(stock, tipo, valor));
                cliente.Disconnect(true);
            }
            return;
        }
        /// <summary>
        /// Le um arquivo de configuracoes salvo em disco e converte num objeto de configuracoes
        /// </summary>
        /// <returns>Configuracoes gerais de envio de email</returns>
        private Config LerConfigDeArquivo(string nomeArquivo) {
            var configsLidas = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(nomeArquivo)) {
                var itens = line.Split('=');
                configsLidas[itens[0]] = itens[1];
            }
            return new Config {
                MailDe              = configsLidas["MailDe"],
                NomeDe              = configsLidas["NomeDe"],
                MailSenha           = configsLidas["MailSenha"],
                MailPara            = configsLidas["MailPara"],
                NomePara            = configsLidas["NomePara"],
                Assunto             = configsLidas["Assunto"],
                Host                = configsLidas["Host"],
                Port                = int.Parse(configsLidas["Port"])
            };
        }
        /// <summary>
        /// Confere se algum campo do arquivo de configuracao esta em branco.
        /// </summary>
        /// <returns>True, se esta totalmente preenchido.</returns>
        private bool ConfigVazia() {
            return
                confMail.MailDe     == string.Empty ||
                confMail.NomeDe     == string.Empty ||
                confMail.MailPara   == string.Empty ||
                confMail.NomePara   == string.Empty ||
                confMail.MailSenha  == string.Empty ||
                confMail.Assunto    == string.Empty ||
                confMail.Host       == string.Empty ||
                confMail.Port       == 0;
        }
        #endregion
    }
}
