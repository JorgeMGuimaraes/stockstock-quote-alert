using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stockstock_quote_alert {
    internal class IOConfiguracoes {
        #region Campos
        #endregion
        #region Metodos
        /// <summary>
        /// Le um arquivo de configuracoes salvo em disco e converte num objeto de configuracoes
        /// </summary>
        /// <returns>Configuracoes gerais de envio de email</returns>
        internal static Config GetConfig(string nomeArquivo) {
                        var configsLidas = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(nomeArquivo)) {
                var itens = line.Split('=');
                configsLidas[itens[0]] = itens[1];
            }
            var config = new Config {
                MailDe              = configsLidas["MailDe"],
                NomeDe              = configsLidas["NomeDe"],
                MailSenha           = configsLidas["MailSenha"],
                MailPara            = configsLidas["MailPara"],
                NomePara            = configsLidas["NomePara"],
                Assunto             = configsLidas["Assunto"],
                Host                = configsLidas["Host"],
                Port                = int.Parse(configsLidas["Port"]),
                UrlApi              = configsLidas["UrlApi"],
                ApiKey              = configsLidas["ApiKey"]
            };
            return (ConfigValida(config)) ? config : null;
        }
        /// <summary>
        /// Confere se algum campo do arquivo de configuracao esta em branco.
        /// </summary>
        /// <returns>True, se esta totalmente preenchido.</returns>
        private static bool ConfigValida(Config ConfigMail) {
            return
                ConfigMail.MailDe       != string.Empty &&
                ConfigMail.NomeDe       != string.Empty &&
                ConfigMail.MailPara     != string.Empty &&
                ConfigMail.NomePara     != string.Empty &&
                ConfigMail.MailSenha    != string.Empty &&
                ConfigMail.Assunto      != string.Empty &&
                ConfigMail.Host         != string.Empty &&
                ConfigMail.Port         != 0;
        }
    }
    #endregion
}
