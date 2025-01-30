# language: pt
Funcionalidade: ProdutosController - GET por categoria
  Para garantir que a busca por categoria funcione corretamente
  Como desenvolvedor
  Eu quero testar o método GET do ProdutosController

  Cenario: Buscar produtos por categoria com sucesso
    Given que eu tenho um identificador de categoria válido
    When eu chamar o método GetByCategory
    Then o resultado deve ser um OkObjectResult
    And a lista de produtos deve ser retornada