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
        private Config ConfigMail { get; }
        #endregion
        #region Constructor
        internal Correio(Config config) => ConfigMail = config;
        #endregion
        #region Metodos
        /// <summary>
        /// Gera um objeto de mensagem baseado nas configuracoes do usuario.
        /// TODO: Pode-se implementar um template de corpo de email lido de um arquivo.
        /// </summary>
        /// <param name="stock">Stock acompanhada.</param>
        /// <param name="tipo">tipo da operacao: compra ou venda</param>
        /// <returns>Mensagem pronta para ser enviada</returns>
        private MimeMessage Mensagem(Stock stock, string tipo) {
            var mail        = new MimeMessage();
            mail.From.Add(new MailboxAddress(ConfigMail.NomeDe, ConfigMail.MailDe));
            mail.To.Add(new MailboxAddress(ConfigMail.NomePara, ConfigMail.MailPara));
            mail.Subject    = $"{ConfigMail.Assunto}: {stock.Simbolo} atingiu valor de {tipo}";
            mail.Body       = new TextPart("plain") {
                Text        = $@"    Sr. {ConfigMail.NomePara},

Conforme suas configurações de compra e venda a stock {stock.Simbolo} atingiu o preço de {tipo} de R$ {stock.PrecoAtual}.

Dados Gerais:
    - Stock: {stock.Simbolo} - {stock.Nome}
    - Preço de {tipo}: R$ {((tipo == "venda") ? stock.PrecoVenda : stock.PrecoCompra)}
    - Preço Atual: R$ {stock.PrecoAtual}
    - Atualizado em: {stock.UltimaAtualizacao} (Dados do servidor)"
                };
            return mail;
        }
        /// <summary>
        /// Usa os parametros de mensagem para enviar um email ao usuario
        /// TODO: Pode-se generalizar o metodo afim de enviar mesagens para tipos diferentes de servidores, ex.: EnviarMensagem<ITipoServidor>.
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="tipo"></param>
        public void EnviarMensagem(Stock stock, string tipo) {
            Console.WriteLine($"Enviando nova mensagem: {stock.Simbolo} -> {tipo}");
            using (var cliente = new SmtpClient()) {
                cliente.Connect(ConfigMail.Host, ConfigMail.Port, false);
                cliente.Authenticate(ConfigMail.MailDe, ConfigMail.MailSenha);
                cliente.Send(Mensagem(stock, tipo));
                cliente.Disconnect(true);
            }
            Console.WriteLine($"Mensagem enviada.");
            return;
        }
        #endregion
    }
}
