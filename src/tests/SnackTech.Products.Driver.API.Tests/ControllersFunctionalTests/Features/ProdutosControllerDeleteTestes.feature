# language: pt
Funcionalidade: ProdutosController - DELETE
  Para garantir que a remoção de produtos funcione corretamente
  Como desenvolvedor
  Eu quero testar o método DELETE do ProdutosController

  Cenario: Remover um produto existente com sucesso
    Given que eu tenho um identificador de produto válido
    When eu chamar o método Delete
    Then o resultado deve ser um OkResult
