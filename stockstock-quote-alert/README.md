# Stock Quotes

Programa desenvolvido em C# para avisar ao usuário caso uma cotação de ativo da B3	caia mais do que o nível compra, ou suba além do nível de venda.

## Uso

´´´
> stockstock-quote-alert.exe
Quantidade de parametros diferente do esperado
Uso: stockstock-quote-alert.exe STOCK venda compra
Ex: stockstock-quote-alert.exe PETR4 24,00 21,00
Usar virgula como separador decimal.

> stockstock-quote-alert.exe PETR4 23,01 22,59
´´´
Antes de usar o programa, preencha o arquivo `configs.txt` com seus dados de email, servidor, e api

### Servidor

A API usada para consultar os dados de cotação é da HG Brasil. Não tenho nenhuma ligação com o grupo, mas esta é bem fácil de usar a gratuíta. Crie sua conta no [site](https://console.hgbrasil.com/keys/new_key_plan), copie a chave (key) e cole no arquivo de configurações.
É possivel adicionar outras APIs, mas antes é necessário alterar um pouco o código para ser compatível com o json de resposta. Pull Requests são bem vindos :)

### Usuários do Gmail
Caso encontre algum problema ao enviar o email pelo Gmail acesse as [configurações de segurança] (https://www.google.com/settings/security/) e hobilite os aplicativos menos seguros.
Acesse [sua conta]-> Problemas críticos de segurança encontrados -> Tomar uma providencia -> Ocorrencias de segurança recentes -> `Sim, fui eu`.

## Contribuição

Pull requests são bem vindos e apreciados. Faça sua contribuição.

## License

Licença:

[MIT](https://github.com/FreakMegalodon/vscode_text_env/blob/master/LICENSE)