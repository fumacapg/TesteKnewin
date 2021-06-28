<h1>Teste de Desenvolvedor de Carlos Carmo</h1>
# 
Este documento README tem como objetivo fornecer as informações necessárias para apresentar o que foi feito e também como executar os testes.

#
<h3>Algoritmos:</h3>
Para atender esta demanda foi criado na solução um Console Application com um menu interativo para escolher entre os algoritmos do teste.

#
`Duplicados na lista`
 
**Específicação:** *Este algoritmo deve receber como parâmetro um vetor contendo uma sequência de números inteiros
e retornar o índice do primeiro item duplicado.*

**Solução:** Primeiramente deve-se escolher a opção 1 do Menu. Depois basta entrar com o vetor que irá retornar o indice do primeiro item duplicado encontrado, caso não haja nenhum item duplicado será retornado -1

Cenário de Testes:
- vetor *1, 2, 3, 4, 5, 6, 7* retorno = *-1*, ou seja, nenhuma duplicidade
- vetor *1, 2, 3, 4, 1, 6, 7* retorno = *4*, ou seja, o item duplicado está no indice 4
- vetor *1, 2, 3, 4, 5, 6, 7* retorno = *-1*, ou seja, nenhuma duplicidade
- vetor *1, 2, 1, 4, 5, 6, 7* retorno = *2*, ou seja, o item duplicado está no indice 2

#
`Palindromo`
 
**Específicação:** *
Definição: Um palindromo é um string que pode ser lida da mesma forma de trás para frente. Por exemplo, "abcba" ou "arara" é um palindromo.

o que é Palindromo? -> https://pt.wikipedia.org/wiki/Pal%C3%ADndromo
 
Faça um método que deve receber uma string como parâmetro e retornar um bool informando se é palíndromo ou não.
*

**Solução:** Primeiramente deve-se escolher a opção 2 do Menu. Depois basta entrar com a palavra que irá retornar se ela é ou não um palindromo.

Cenário de Testes:
- palavra: *carlos* retorno = CARLOS não é um palindromo
- palavra: *arara* retorno = ARARA é um palindromo
- palavra: *contratado* retorno = CONTRATADO não é um palindromo
- palavra: *tenet* retorno = TENET é um palindromo
 
#

<h3>API de Cidades:</h3>
Para atender a especificação foi construida uma API em .NET Core 5 utilizando sqlite para armazenar os dados das cidades e dos usuários. Utilizei estrutura em camadas e utilizei injeção de dependência e criptografia da senha do usuário.


**Específicação:** 

- Uma funcionalidade para fazer login.
- Uma funcionalidade para cadastrar novas cidades:
  - As cidades devem contar no mínimo com:
    - Um nome e uma estrutura que diga com quem ela faz fronteira
    - Ex: 
      - {"Nome": "A", "Fronteira": ["B", "E"]}
      - {"Nome": "São José", "Fronteira": ["Florianópolis", "Palhoça"]}
- Um meio para retornar todas as cidades já cadastradas ( essa não precisa estar autenticado )
- Um meio para procurar uma cidade especifica
- Um meio que retorne as cidades que fazem fronteira com uma cidade específica
  - Ex: Quem faz fronteira com a Cidade B?
- Retornar a soma dos habitantes de um conjunto de cidades
  - Ex: cidade A,B,C possuem 50 mil habitantes
- Um método pra eu poder atualizar os dados de uma cidade, por exemplo mudar a quantidade de habitantes.
- O caminho que devo fazer de uma cidade a outra
  - Ex: sair de cidade Buenos aires e ir até a cidade Florianópolis*
  
<h5>Solução:</h5> 

**Login**

`TOKEN`

Verbo: POST

URL: https://localhost:44391/api/login/token

Função: Obter o token para autenticação na API

Parâmetros de Entrada:
```json
{
  "login": "knewin",
  "senha": "mecontrata"
}
```
Retorno:

Status Code: 200

```json
{
    "usuario": {
        "id": 3,
        "login": "knewin",
        "senha": "",
        "role": "Administrador"
    },
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImtuZXdpbiIsInJvbGUiOiJBZG1pbmlzdHJhZG9yIiwibmJmIjoxNjI0ODg0MzI3LCJleHAiOjE2MjQ4OTE1MjcsImlhdCI6MTYyNDg4NDMyN30.ynXS0lmP3IwrJJpyqcSceRi1VfaKRbY79XcXG09r_kU"
}
```

`Cadastro de Usuário`

Verbo: POST

URL: https://localhost:44391/api/login

Função: Cadastrar um novo usuário na API

Parâmetros de Entrada:
```json
{
  "login": "knewin",
  "senha": "mecontrata",
  "role": "Administrador"
}
```
Retorno:

Status Code: 200

```json
{
    "id": 3,
    "login": "knewin",
    "senha": "$6$J.vZYf5Mi2gCgpwH$bEoahW61fiOIiSFAbwMieTjShR.zYbqa4KhqrlX4cX0046gtw4f/cVitPwpxsItMjN78Vubqpye2JECuyHXa2/",
    "role": "Administrador"
}
```

`Alteração de Usuário`

Verbo: PUT

URL: https://localhost:44391/api/login/{id}

Função: Alterar os dados do Usuário

Parâmetros de Entrada:

Precisa informar o id do usuário além do json com os dados modificados conforme abaixo:

```json
{
  "id": 3,
  "login": "knewin",
  "senha": "contratado"
  "role": "Administrador"
}
```

Retorno:

Status Code: 200

```json
{
    "id": 3,
    "login": "knewin",
    "senha": "$6$Eg/sL4EZw0juXiib$pCrfPbEejy5nTYgn.W3NdwCYx1942MNbFxYYshhBMB19tCViU2W3eQz/RiMffW8fJk5hZs.2nCso203GMjd5e1",
    "role": "Administrador"
}
```

`Exclusão do Usuário`

Verbo: DELETE

URL: https://localhost:44391/api/login/{id}

Função: Excluir o Usuário

Parâmetros de Entrada:
```
Precisa informar o id do usuário
```

Retorno:

Status Code: 200

```
Excluído com sucesso
```
#

**Cidade**

`Cadastro de Cidade`

Verbo: POST

URL: https://localhost:44391/api/cidade

Função: Cadastrar uma nova cidade na API

Parâmetros de Entrada:

```json
{
	"nome": "Mongagua",
	"numeroHabitantes": 180000,
	"fronteiras": [
		{
			"cidadeFronteira": "Praia Grande"
		},
		{
			"cidadeFronteira": "Itanhaem"
		},
		{
			"cidadeFronteira": "São Paulo"
		}
	]
}
```

Retorno:

Status Code: 200

```json
{
    "id": 5,
    "nome": "Mongagua",
    "numeroHabitantes": 180000,
    "fronteiras": [
        {
            "id": 9,
            "cidadeFronteira": "Praia Grande",
            "cidadeId": 5
        },
        {
            "id": 10,
            "cidadeFronteira": "Itanhaem",
            "cidadeId": 5
        },
        {
            "id": 11,
            "cidadeFronteira": "São Paulo",
            "cidadeId": 5
        }
    ]
}
```

`Alteração de Cidade`

Verbo: PUT

URL: https://localhost:44391/api/cidade/{id}

Função: Alterar os dados da Cidade

Parâmetros de Entrada:

Precisa informar o id da cidade além do json com os dados modificados conforme abaixo:

```json
{
  "id": 6,
  "nome": "Peruibe",
  "numeroHabitantes": 100000,
  "fronteiras": [
    {
      "id": 12,
      "cidadeFronteira": "Itanhaem",
      "cidadeId": 6
    },
    {
      "id": 13,
      "cidadeFronteira": "Pedro de Toledo",
      "cidadeId": 6
    },
    {
      "id": 14,
      "cidadeFronteira": "São Paulo",
      "cidadeId": 6
    }
  ]
}
```

Retorno:

Status Code: 200

```json
{
  "id": 6,
  "nome": "Peruibe",
  "numeroHabitantes": 100000,
  "fronteiras": [
    {
      "id": 12,
      "cidadeFronteira": "Itanhaem",
      "cidadeId": 6
    },
    {
      "id": 13,
      "cidadeFronteira": "Pedro de Toledo",
      "cidadeId": 6
    },
    {
      "id": 14,
      "cidadeFronteira": "São Paulo",
      "cidadeId": 6
    }
  ]
}
```

`Exclusão da Cidade`

Verbo: DELETE

URL: https://localhost:44391/api/cidade/{id}

Função: Excluir a Cidade

Parâmetros de Entrada:

```
Precisa informar o id da cidade
```

Retorno:

Status Code: 200

```
Excluído com sucesso
```

`Buscar todas as Cidades`

Verbo: GET

URL: https://localhost:44391/api/cidade/

Função: Retornar todas as cidades cadastradas

Parâmetros de Entrada:

```
Não há
```

Retorno:

Status Code: 200

```json
[
  {
    "id": 1,
    "nome": "Praia Grande",
    "numeroHabitantes": 350000,
    "fronteiras": [
      {
        "id": 1,
        "cidadeFronteira": "Mongagua",
        "cidadeId": 1
      },
      {
        "id": 2,
        "cidadeFronteira": "São Vicente",
        "cidadeId": 1
      }
    ]
  },
  {
    "id": 2,
    "nome": "São Vicente",
    "numeroHabitantes": 223597,
    "fronteiras": [
      {
        "id": 3,
        "cidadeFronteira": "Praia Grande",
        "cidadeId": 2
      },
      {
        "id": 4,
        "cidadeFronteira": "Cubatão",
        "cidadeId": 2
      },
      {
        "id": 5,
        "cidadeFronteira": "Santos",
        "cidadeId": 2
      }
    ]
  },
  {
    "id": 3,
    "nome": "Itanhaém",
    "numeroHabitantes": 180000,
    "fronteiras": []
  },
  {
    "id": 4,
    "nome": "Santos",
    "numeroHabitantes": 400000,
    "fronteiras": [
      {
        "id": 6,
        "cidadeFronteira": "São Vicente",
        "cidadeId": 4
      },
      {
        "id": 7,
        "cidadeFronteira": "Guarujá",
        "cidadeId": 4
      },
      {
        "id": 8,
        "cidadeFronteira": "Cubatão",
        "cidadeId": 4
      }
    ]
  },
  {
    "id": 5,
    "nome": "Mongagua",
    "numeroHabitantes": 180000,
    "fronteiras": [
      {
        "id": 9,
        "cidadeFronteira": "Praia Grande",
        "cidadeId": 5
      },
      {
        "id": 10,
        "cidadeFronteira": "Itanhaem",
        "cidadeId": 5
      },
      {
        "id": 11,
        "cidadeFronteira": "São Paulo",
        "cidadeId": 5
      }
    ]
  }
]
```

`Buscar Cidade por Id`

Verbo: GET

URL: https://localhost:44391/api/cidade/{id}

Função: Buscar uma cidade pelo id

Parâmetros de Entrada:

```
Precisa informar o id da cidade
```

Retorno:

Status Code: 200

```json
{
    "id": 4,
    "nome": "Santos",
    "numeroHabitantes": 400000,
    "fronteiras": [
      {
        "id": 6,
        "cidadeFronteira": "São Vicente",
        "cidadeId": 4
      },
      {
        "id": 7,
        "cidadeFronteira": "Guarujá",
        "cidadeId": 4
      },
      {
        "id": 8,
        "cidadeFronteira": "Cubatão",
        "cidadeId": 4
      }
    ]
  }
```

`Buscar Cidade por Nome`

Verbo: GET

URL: https://localhost:44391/api/cidade/nome/{nome}

Função: Buscar uma cidade pelo nome ou parte do nome

Parâmetros de Entrada:

```
Precisa informar o nome da cidade ou parte do nome por exemplo: Santos ou San
```

Retorno:

Status Code: 200

```json
{
    "id": 4,
    "nome": "Santos",
    "numeroHabitantes": 400000,
    "fronteiras": [
      {
        "id": 6,
        "cidadeFronteira": "São Vicente",
        "cidadeId": 4
      },
      {
        "id": 7,
        "cidadeFronteira": "Guarujá",
        "cidadeId": 4
      },
      {
        "id": 8,
        "cidadeFronteira": "Cubatão",
        "cidadeId": 4
      }
    ]
  }
```

`Buscar Fronteiras da Cidade`

Verbo: GET

URL: https://localhost:44391/api/cidade/fronteiras/{cidadeNome}

Função: Buscar uma cidade pelo nome ou parte do nome

Parâmetros de Entrada:

```
Precisa informar o nome da cidade ou parte do nome por exemplo: Santos ou San
```

Retorno:

Status Code: 200

```json
[
    {
        "id": 2,
        "nome": "São Vicente",
        "numeroHabitantes": 223597,
        "fronteiras": [
            {
                "id": 3,
                "cidadeFronteira": "Praia Grande",
                "cidadeId": 2
            },
            {
                "id": 4,
                "cidadeFronteira": "Cubatão",
                "cidadeId": 2
            },
            {
                "id": 5,
                "cidadeFronteira": "Santos",
                "cidadeId": 2
            }
        ]
    }
]
```

`Número de Habitantes`

Verbo: POST

URL: https://localhost:44391/api/cidade/habitantes

Função: Buscar o número de habitantes das cidades informadas via parâmetro

Parâmetros de Entrada:

```
[
	1,2,3
]
```

Retorno:

Status Code: 200

```
A soma de habitantes das cidades: Praia Grande, São Vicente, Itanhaém é de: 753.597
```

`Rotas`

Verbo: GET

URL: https://localhost:44391/api/cidade/rotas/{id}

Função: Retorna as rotas possíveis de acordo com a cidade de origem informada pelo Id

Parâmetros de Entrada:

```
Precisa informar o id da cidade
```

Retorno:

Status Code: 200

```
Saindo de Santos você pode ir para São Vicente, Guarujá e Cubatão
```

<h3>Schemas</h3>

**Usuario**

```json
{
	id	integer($int32)
	login	string	nullable: true
	senha	string	nullable: true
	role	string	nullable: true
}
```

**Cidade**

```json
{
	id	integer($int32)
	nome	string	nullable: true
	numeroHabitantes	integer($int32)
	fronteiras	[...]
}
```

**Fronteira**

```json
{
	id	integer($int32)
	cidadeFronteira	string	nullable: true
	cidadeId	integer($int32)
	cidade	Cidade{...}
}
```

<h3>Observações da implementação</h3>

Para esta solução há alguns pontos a serem melhorados que não foram implementados por questão do tempo são elas:

- Faltou implementar algumas consistências como campos nulos verificar valores, etc.
- Implementar o Model da camada da API afim de preservar a modelagem interna.
- Ao implementar o model na camada da API usar o AutoMapper para fazer as conversões
- As fronteiras estão como string o ideal seria serem entidades com autorelacionamento
- A última demanda da api não ficou muito clara, no meu entendimento era para mostrar as opções de destino partindo de uma cidade inicial


