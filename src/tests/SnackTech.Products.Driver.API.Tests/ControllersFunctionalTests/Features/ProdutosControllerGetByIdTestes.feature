# language: pt
Funcionalidade: ProdutosController - GET por identificador
  Para garantir que a busca por identificador funcione corretamente
  Como desenvolvedor
  Eu quero testar o método GET do ProdutosController

  Cenario: Buscar produtos por id com sucesso
    Given que eu tenho um identificador válido
    When eu chamar o método GetById
    Then o resultado deve ser um OkObjectResult
    And a lista de produtos deve ser retornada