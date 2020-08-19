# Stock Quotes

Programa desenvolvido em C# para avisar ao usuário caso uma cotação de ativo da B3	caia mais do que o nível compra, ou suba além do nível de venda.

## Uso

```cmd
> stockstock-quote-alert.exe PETR4 23,01 22,59
```
> Use vírgula como separador de decimais.

Antes de usar o programa, preencha o arquivo `configs.txt` com seus dados de email, servidor, e api.

### Servidor

A API usada para consultar os dados de cotação é da HG Brasil. Não tenho nenhuma ligação com o grupo, mas esta é bem fácil de usar a gratuíta. Crie sua conta no [site](https://console.hgbrasil.com/keys/new_key_plan), copie a chave (key) e cole no arquivo de configurações.
É possivel adicionar outras APIs, mas antes é necessário alterar um pouco o código para ser compatível com o json de resposta. Pull Requests são bem vindos :)

### Usuários do Gmail
Caso encontre algum problema ao enviar o email pelo Gmail acesse as [configurações de segurança](https://www.google.com/settings/security/) e hobilite os aplicativos menos seguros.
Acesse [sua conta](https://myaccount.google.com/) -> Problemas de segurança encontrados -> Tomar uma providencia -> Ocorrencias de segurança recentes -> `Sim, fui eu`.

## Contribuição

Pull requests são bem vindos e apreciados. Se tiver uma ideia, faça sua contribuição.

## License

Licença:

[MIT](https://choosealicense.com/licenses/mit/)