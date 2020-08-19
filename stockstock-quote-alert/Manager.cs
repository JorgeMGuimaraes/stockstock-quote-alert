namespace stockstock_quote_alert {
    using RestSharp;
    using System;
    using System.Threading;
    /// <summary>
    /// Componente principal de acompanhamento de valores de stock.
    /// </summary>
    internal class Manager {
        #region Campos
        private string CaminhoArquivo { get; } = "configs.txt";
        private Config Config { get; }
        private Stock Stock { get; set; }
        private Correio Correio { get; }
        private const double
            segundosIniciais    = 1d,
            intervaloOperacoes  = 15d;
        #endregion
        #region Constructor
        public Manager(string[] args) {
            if(args.Length != 3) {
                Console.WriteLine("Quantidade de parametros diferente do esperado");
                Console.WriteLine("Uso: stockstock-quote-alert.exe STOCK venda compra");
                Console.WriteLine("Ex: stockstock-quote-alert.exe PETR4 21.00 24.00");
                return;
            }

            Config  = IOConfiguracoes.GetConfig(CaminhoArquivo);
            if(Config == null) {
                Console.WriteLine("Favor preencher o arquivo de configuracoes 'configs.txt'");
                return;
            }

            Correio = new Correio(Config);
            Stock   = new Stock {
                Simbolo             = args[0].ToUpper(),
                PrecoCompra         = double.Parse(args[1]),
                PrecoVenda          = double.Parse(args[1]),
                UltimaAtualizacao   = null
            };

            Run();
            return;
        }
        #endregion
        #region Metodos
        /// <summary>
        /// Inicia a operacao de monitoramento. As atualizacoes rodam a cada '<see cref="intervaloOperacoes"/>' minutos.
        /// </summary>
        internal void Run() {
            Console.WriteLine("Pressione 'Enter' a qualquer momento para parar o programa...\n");

            using (var timer = new Timer(_ =>
                OperacaoTemporizada(),
                null,
                TimeSpan.FromSeconds(segundosIniciais),
                TimeSpan.FromMinutes(intervaloOperacoes))) {
                Console.ReadLine();
            }
            return;
        }
        /// <summary>
        /// Conecta e atualiza os valores de stock conforme o temporizador.
        /// </summary>
        private void OperacaoTemporizada() {
            Console.WriteLine($"{DateTime.Now}: Entrando em contato com o servidor...");
            var url = $"{Config.UrlApi}key={Config.ApiKey}&symbol={Stock.Simbolo}";
            var cotacaoAtual = GetCotacaoRemota(url);
            Stock.AtualizarValores(cotacaoAtual);

            if (Stock.ValorAtualizado()) {
                Console.WriteLine("Atualizando valores...");
                if (Stock.PrecoDeCompraAlcancado()) { Correio.EnviarMensagem(Stock, "venda"); }
                else if (Stock.PrecoDeVendaAlcancado()) { Correio.EnviarMensagem(Stock, "compra"); }
            }
            return;
        }
        /// <summary>
        /// Recupera valores de cotacao consumidos da api fornecida.
        /// </summary>
        /// <param name="url">url da api</param>
        /// <returns>string json com os valores do stock.</returns>
        private string GetCotacaoRemota(string url) {
            var client      = new RestClient(url);
            var response    = client.Execute(new RestRequest());
            return response.Content;
        }
        #endregion
    }
}