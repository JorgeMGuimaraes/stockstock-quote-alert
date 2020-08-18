using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace stockstock_quote_alert {
    class Manager {
        #region Campos
        private string nomeStock { get; set; }
        private double valorRefVenda { get; set; }
        private double valorRefCompra { get; set; }
        #endregion
        public Manager(string[] args) {
            if(args.Length != 3) {
                Console.WriteLine("Quantidade de parametros diferente do esperado");
                Console.WriteLine("Uso: stockstock-quote-alert.exe STOCK venda compra");
                Console.WriteLine("stockstock-quote-alert.exe PETR4 21.00 22.00");
            }
            else {
                nomeStock = args[0];
                valorRefVenda = double.Parse(args[1]);
                valorRefCompra = double.Parse(args[2]);
                Console.WriteLine($"nome: {nomeStock} compra: {valorRefCompra} venda: {valorRefVenda}"); 
            }
        }
    }
}
