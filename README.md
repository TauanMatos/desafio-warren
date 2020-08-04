# Desafio

Implementar um sistema de controle de conta corrente bancária, processando solicitações de depósito, resgates e pagamentos. Um ponto extra seria rentabilizar o dinheiro parado em conta de um dia para o outro como uma conta corrente remunerada.

# - Tecnologias usadas

  - .Net Core Web Api
  - Entity Framework Core
  - Arquitetura orientada a domínio: DDD
  - Banco de dados MySql
  - Angular 9

# - Execução do projeto

### Banco de dados

Antes de colocar a API pra rodar, é necessário alterar as configurações de banco de dados no arquivo appsettings.json.
A princípio o banco está hosteado no db4free.net.

A API possui um DbInitializer, então não é necessário aplicar as migrations.

### Frontend

Para executar o frontend em Angular 2+ é necessária a instalação dos pacotes utilizados. Para isso, utilize o seguinte comando: 

```sh
$ cd frontend
$ npm install
$ ng serve
```

# - Usuários da aplicação

##### Usuário com movimentações na conta
Username: client1

Password: Client@1

##### Usuário com a conta zerada
Username: client2

Password: Clien@2
