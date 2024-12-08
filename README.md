# Sistema Básico de Blog

Este repositório contém um sistema básico de blog desenvolvido em **C#**, utilizando o framework **.NET 8**. O projeto adota **Domain-Driven Design (DDD)**, **Clean Architecture**, e os princípios de **Clean Code**. Além disso, o padrão **CQRS** foi implementado para separar as operações de leitura e escrita. 

---

## 📚 **Objetivo do Sistema**
O objetivo é fornecer um exemplo funcional de um blog simples, onde os usuários podem:
- Visualizar postagens.
- Criar novas postagens.
- Editar postagens existentes.
- Excluir postagens.

O sistema inclui uma funcionalidade de comunicação em tempo real, notificando os usuários de novas postagens através de **WebSockets**.

---

## 🏗️ **Principais Tecnologias e Arquitetura**
### Linguagem e Frameworks
- **C#** com **.NET 8**
- **Entity Framework Core** para acesso e manipulação de dados.

### Padrões e Princípios
- **Domain-Driven Design (DDD)**: Organização baseada em domínios para desacoplar responsabilidades.
- **Clean Architecture**: Separação em camadas bem definidas:
  - **Domínio**: Contém as regras de negócio.
  - **Aplicação**: Gerencia os casos de uso.
  - **Infraestrutura**: Integração com banco de dados e serviços externos.
  - **Interface de Usuário (UI)**: Implementação do frontend ou API.
- **Clean Code**: Código legível e de fácil manutenção.
- **SOLID**: Princípios aplicados para garantir a modularidade e flexibilidade do sistema.
- **CQRS**: Separação clara entre comandos (escrita) e consultas (leitura).

### Comunicação em Tempo Real
- **WebSockets**: Para notificar usuários em tempo real sobre novas postagens.

---

## 🛠️ **Estrutura do Projeto**

```plaintext
src/
├── Miniblog.App.Domain/        # Camada de Domínio: Entidades, Value Objects e Interfaces de Repositório.
├── Miniblog.App.Application/   # Camada de Aplicação: Casos de uso, DTOs, e Handlers (CQRS).
├── Miniblog.App.Data/          # Camada de Infraestrutura (Dados): Implementação de repositórios e contexto do Entity Framework.
├── Miniblog.App.Security/      # Camada de Infraestrutura (Segurança): Implementação de helpers e providers para a camada de segurança.
├── Miniblog.App.Api/           # Camada de Interface: Api com as rotas e configuração do WebSocket.
├── Miniblog.App.Console/       # Aplicação responsável para testar a comunicação com o WebSocket.
