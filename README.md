# Desafio Balta: Criação de uma simples Api em .Net

Desafio consiste em criar uma Api que tenha autorização e autenticação e um CRUD para o banco de dados base.

## Índice
- <a href="#tecnologias-utilizadas">Tecnologias utilizadas</a>
- <a href="#funcionalidades-do-projeto">Funcionalidades do projeto</a>
- <a href="#como-rodar-este-projeto">Como rodar este projeto?</a>
- <a href="#imagens-endpoints">Imagens endpoints</a>

## Funcionalidades do projeto
- [x] Cadastro de usuário
- [x] Login
- [x] Autorização e autenticação
- [x] Leitura, consultas e alterações dos dados no banco IBGE
## Tecnologias utilizadas

1. [EntityFramaworkCore](https://learn.microsoft.com/en-us/ef/)
2. [.Net 7]()

## Como rodar este projeto?
```bash
#Clone este repositório
$ git clone https://github.com/edilsontorres/DesafioBaltaIbge.git

#Abra o projeto na sua IDE 

#Crie o DB
Na raiz deste repositório você encontrará dois arquivos dentro da pasta SQL Server.

--Schema--
Arquivo contendo a criação da tabela IBGE no banco de dados. Este arquivo deve ser executado primeiro.

--Data--
Arquivo contendo os INSERTS de dados dos municípios obtidos da planilha RELATORIO_DTB_BRASIL_MUNICIPIO contida na raiz deste repositório.

#configure a conexão com banco de dados

#rode a aplicação
$ dotnet run
```

## Demonstração
- [Link demonstração](https://desafioibge.azurewebsites.net/swagger/index.html)

## Imagens de alguns endpoints funcionando
![Swagger](/assets/img1.PNG)
![GetAll](/assets/img2.PNG)
![GetCity](/assets/img3.PNG)
![GetState](/assets/img4.PNG)
![GetIbge](/assets/img5.PNG)


