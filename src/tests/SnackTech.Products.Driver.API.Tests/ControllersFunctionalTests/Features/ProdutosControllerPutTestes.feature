# language: pt
Funcionalidade: ProdutosController - PUT
  Para garantir que a edição de produtos funcione corretamente
  Como desenvolvedor
  Eu quero testar o método PUT do ProdutosController

  Cenario: Editar um produto existente com sucesso
    Given que eu tenho um produto existente e dados válidos para edição
    When eu chamar o método Put
    Then o resultado deve ser um OkObjectResult
