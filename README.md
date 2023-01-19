# Cashflow
## Requisitos de negócios

- Serviço que faça o controle de lançamentos
- Serviço do consolidado diário


## Desenho da Arquitetura
A arquitetura atual do solução consite em 2 serviços executando dentro de container docker em linux e 1 banco de dados SQL Server executando dentro de container docker em linux.
![Arquitetura Versão 1](/arch/arquitetura-v1.png)

## Preparando o ambiente
Deve ser instalado o WSL 2 com o Ubuntu-20.04 caso esteja utilizando uma maquina Windows e docker engine e docker compose no WSL 2. Você pode encontrar o passo a passo de como instalar nos link abaixo

Instalação do WSL
https://learn.microsoft.com/pt-br/windows/wsl/install

Instalação do docker
https://docs.docker.com/engine/install/ubuntu/#install-using-the-repository

## Subindo os container
Abra o terminal do linux na pasta raiz do projeto e execute o comando docker-compose -f docker-compose.yml up --build -d. Aguarde a conclusão da subida dos containers. Entre com as url abaixo para conferir se os serviços está online

 - http://localhost:5145/swagger/index.html - Serviço CashFlow (Fluxo de caixa)
 - http://localhost:5146/swagger/index.html - Serviço Report (Relatório)

## Container 01 - Serviço CashFlow (Fluxo de caixa)
Este serviço é responsavél pelo controle dos dados dos lançamentos bancários de credito e debito. Você pode conferir pela interface do swagger as operações disponivéis

| HTTP| URI | Função |
|--|--|--|
|POST| https://localhost:5145/v1/LancamentoBancario | Cadastrar lançamento bancário |
| PUT | http://localhost:5145/v1/LancamentoBancario | Atualizar  lançamento bancário |
| DELETE | http://localhost:5145/v1/LancamentoBancario/:id | Excluir  lançamento bancário |
| GET | http://localhost:5145/v1/LancamentoBancario/:id | Obter  lançamento bancário por id |
| GET | http://localhost:5145/v1/LancamentoBancario | Obter todos  lançamento bancário |

![Interfaçe swagger](/arch/swagger-cashflow-api.png)

## Container 02 - Serviço Report (Relatório)
Este serviço é responsavél pela extração de relatório consolidado dos lançamentos bancários de credito e debito. Você pode conferir pela interface do swagger as operações disponivéis

| HTTP| URI | Função |
|--|--|--|
| POST| http://localhost:5146/v1/report | Obtem os metadados do relatório |
| GET | http://localhost:5145/v1/Report/processar | Solicita o processamento do relatório |
| GET | http://localhost:5145/v1/report/download | Faz o download do relatório em csv |

![Interfaçe swagger](/arch/swagger-report-api.png)

## Container 03 - Serviço SQL Server
Este serviço fica responsável pelo hospedagem do banco de dados. 

Dados para fazer autenticação

usuario: sa

senha: SqlServer2019!

![Interfaçe SQL Server](/arch/autenticacao-sqlserver.png)


## Arquitetura Evolutiva
A arquitetura da soluções deve ser evolutiva até que se atinja um nivél de maturidade alto. Para a segunda versão do sistema foram indentificados os seguintes pontos de melhoria.

- Uso de kubernates para replicação do sistema em pods.
- Uso do RabbitMQ para armazenar em fila os pedidos de processamento de relatórios.
- Uso do Azure Function escutando a fila no RabbitMQ para indentificar um pedido de processamento de relatórios.
- Upload do relatório no storage account.
- Sempre refatorar o código para melhoria.
- Habilitar o uso de HTTPS e desabilitar o HTTP

![Arquitetura Versão 2](/arch/arquitetura-v2.png)
