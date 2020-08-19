using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace stockstock_quote_alert {
    /// <summary>
    /// Guarda as informacoes da estoque enquanto o programa roda.
    /// </summary>
    internal class Stock {
        #region Campos
        internal string Simbolo { get; set; }
        internal string Nome { get; set; }
        internal double PrecoCompra { get; set; }
        internal double PrecoVenda { get; set; }
        internal double PrecoAtual { get; set; }
        internal DateTime? UltimaAtualizacao { get; set; }
        #endregion
        #region Metodos
        /// <summary>
        /// Determina se a venda deve ser efetuada com base nos precos.
        /// </summary>
        /// <returns>True, se valor responde a expectativa.</returns>
        internal bool PrecoDeVendaAlcancado() => PrecoAtual >= PrecoVenda;
        /// <summary>
        /// Determina se a compra deve ser efetuada com base nos precos.
        /// </summary>
        /// <returns>True, se valor responde a expectativa.</returns>
        internal bool PrecoDeCompraAlcancado() => PrecoAtual <= PrecoCompra;
        /// <summary>
        /// Verifica se houve atualizacao nos valores
        /// </summary>
        /// <returns>True, se ha sucesso na atualizacao</returns>
        internal bool ValorAtualizado() => UltimaAtualizacao != null;
        /// <summary>
        /// Atualiza os valores de <see cref="PrecoAtual"/> e <see cref="UltimaAtualizacao"/> com dados do servidor
        /// </summary>
        /// <param name="json">string json consumida do servidor</param>
        internal void AtualizarValores(string json) {
            var resultado = JObject.Parse(json);
            try {
                PrecoAtual          = double.Parse((string)resultado["results"][Simbolo]["price"]) / 100;
                UltimaAtualizacao   = DateTime.Parse((string)resultado["results"][Simbolo]["updated_at"]);
            }
            catch (Exception e) {
                UltimaAtualizacao   = null;
                Console.WriteLine($"Erro ao atualizar valores: {e}");
            }

        }
        #endregion
    }
}
