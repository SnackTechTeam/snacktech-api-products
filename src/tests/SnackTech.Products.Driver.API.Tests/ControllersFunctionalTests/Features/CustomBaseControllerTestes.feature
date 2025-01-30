# language: pt
Funcionalidade: CustomBaseController
  Para garantir que o controlador base se comporte corretamente
  Como desenvolvedor
  Eu quero testar os métodos do CustomBaseController

  Cenário: ExecucaoPadrao com sucesso
    Dado que eu tenho uma tarefa válida que retorna um ResultadoOperacao bem-sucedido
    Quando eu chamar o método ExecucaoPadrao
    Então o resultado deve ser um OkObjectResult

  Cenário: ExecucaoPadrao retornando BadRequest
    Dado que eu tenho uma tarefa válida que retorna um ResultadoOperacao com uma mensagem de erro
    Quando eu chamar o método ExecucaoPadrao
    Então o resultado deve ser um BadRequestObjectResult
    E a mensagem de erro deve ser "Erro de lógica"

  Cenário: ExecucaoPadrao retornando InternalServerError a partir da tarefa
    Dado que eu tenho uma tarefa válida que retorna um ResultadoOperacao com uma exceção
    Quando eu chamar o método ExecucaoPadrao
    Então o resultado deve ser um ObjectResult
    E a mensagem de erro deve ser "Erro inesperado"

  Cenário: ExecucaoPadrao retornando InternalServerError a partir do processamento
    Dado que eu tenho uma tarefa que lança uma exceção
    Quando eu chamar o método ExecucaoPadrao
    Então o resultado deve ser um ObjectResult
    E a mensagem de erro deve ser "Erro inesperado"
