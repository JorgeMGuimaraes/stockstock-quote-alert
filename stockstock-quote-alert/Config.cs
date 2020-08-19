namespace stockstock_quote_alert {
    /// <summary>
    /// Configuracoes basicas copiadas do arquivo configs.txt em disco.
    /// </summary>
    internal class Config {
        public string MailDe { get; set; }
        public string NomeDe { get; set; }
        public string MailPara { get; set; }
        public string NomePara { get; set; }
        public string MailSenha { get; set; }
        public string Assunto { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UrlApi { get; set; }
    }
}
