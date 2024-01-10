# ProjetoFinalEFCore
 Projeto final de Entity Framework

Esse projeto foi baseado no curso "https://desenvolvedor.io/curso-online-dominando-o-entity-framework-core" e coloquei nele o que eu julgo ser as melhores ideias que o curso proporciona.

Tem a pratica em um sistema que simula uma API que cadastra Jogos, Estudio desenvolvedor de jogos, Genero de jogos e etc.

A ideia do sistema foi só para eu ter uma linha de raciocinio, conseguir colocar dados e fazer algumas queries.

Então basicamente você pode criar alguns generos de jogos, alguns estudios, em qual plataforma ele está disponivel e no fim criar o jogo com todas essas informações, quando fizer a consulta do jogo deverá trazer com ele todos os generos atrelados, assim como o estudio e as plataformas, e a mesma ideia quando você buscar um genero especifico ou plataforma, quando fizer a busca o sistema devera trazer todos os jogos juntos correspondente ao genero que você pesquisou.

A maioria das entidades tem poucas propriedades justamente para quando quiser avançar com o projeto também praticar o EF Core, colocando novas queries, gerando novas migrações e ir evoluindo.

Criei também algumas classes somente para servir de consulta sobre ideias que achei interessante e que vão servir muito bem para futuros projetos no qual utilizarei o EF Core.

OBS: Esse projeto não está separado em camadas e não tem muitas validações porque o foco não era esse, tentei fazer o mais simples possível com o foco maior no repositorio.