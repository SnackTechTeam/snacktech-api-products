# language: pt
Funcionalidade: ProdutosController - POST
  Para garantir que o cadastro de produtos funcione corretamente
  Como desenvolvedor
  Eu quero testar o método POST do ProdutosController

  Cenario: Cadastrar um novo produto com sucesso
    Given que eu tenho um novo produto válido
    When eu chamar o método Post
    Then o resultado deve ser um OkObjectResult