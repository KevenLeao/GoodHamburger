Descrição do projeto:
Este é um sistema de pedidos para uma hamburgueria desenvolvido em .NET 8 utilizando Blazor Interactive Server.
O sistema permite navegar pelo menu, montar combos com descontos automáticos e gerenciar o histórico de pedidos.


Requisitos para rodar:
- - SDK do .NET 8.
- Clonar o repositório do git ou fazer o download da pasta.
- Na pasta do projeto, executar o comando dotnet watch ou dotnet run.
- Acessar o link gerado (normalmente em http://localhost:5000) ou na porta indicada no terminal.

Estrutura do projeto:
- /wwwroot/css/app.css: Página de estilização global.
- /Models: Página única para a classe de dados (Dados que serão trazidos no cardápio, utilizando o foreach)
- /Components: CartView.razor (Componente do carrinho reutilizável entre as páginas).
- /Pages: Views principais (AllMenu, Sandwiches, Extras, Orders).
- /Services: OrderService.cs (Cérebro do app, gerencia estado do pedido e os cálculos de desconto).
- 
Lógica dos descontos:
- Burger + Fries + Soft Drink = 20% OFF
- Burger + Soft Drink = 15% OFF
- Burger + Fries = 10% OFF
* Regra: Máximo de 1 item por categoria e 3 itens no total por pedido.

O desconto é aplicado automaticamente, pois no cérebro do sistema, temos um "event Action? OnChange;" que verifica se o carrinho foi alterado, faz a análise do que foi adicionado ou removido, e faz o calculo estabelicido pelas regras montadas na página de serviço

A página "Orders" simula o gerenciamento de uma API:
- Listar: Carrega os pedidos do HistoricoPedidos no Service.
- Criar: Botão "Place Order" no carrinho, que envia o pedido completo já com o desconto somado para a próxima página de pedidos (Orders)
- Atualizar: Select de Status (Pending/Preparing/Completed).
- Deletar: Ícone de lixeira em cada card de pedido.

A estilização do sistema foi feita com muito CSS puro, para deixar uma view bonita e também utilizando emojis, que são interpretados pelas strings, trazendo uma parte visual bacana para o navegador (Exemplo: O emoji 🍔 tem o código U+1F354)
