# Neon Forge

## Objetivo do jogo
Neon Forge é um protótipo de **idle/clicker** em que o jogador forja itens neon e evolui sua forja. O objetivo é concluir o progresso de cada item batendo com o martelo até que o projeto seja finalizado. A cada item concluído, o jogador ganha créditos e passa para o próximo nível.

## Recursos principais
- Sistema de progresso com aumento de **HP** do item a cada nível.
- Moeda interna (**créditos**) usada para comprar melhorias.
- Upgrades de **tap** (força do clique) e **DPS** automático.
- Itens lendários a cada 10 níveis que rendem recompensas extras.
- Salvamento automático de progresso.

## Abrindo o projeto
1. Instale o [Unity Hub](https://unity.com/download) e a Unity **6000.2.0f1** (versão recomendada).
2. No Unity Hub, clique em **Add** e selecione a pasta do repositório.
3. Abra o projeto e, após o carregamento, clique em **Play** para iniciar.

## Como jogar
- Clique ou toque na área da forja para aplicar dano ao item.
- Concluindo o item você ganha créditos e avança de nível.
- Use os botões de **upgrade** para melhorar o tap, o DPS e contratar bots.

## Scripts principais
- **GameManager**: gerencia níveis, créditos, poder de toque e DPS.
- **SaveSystem / SaveData**: realiza o salvamento e o carregamento do progresso.
- **ForgeItem**: representa o item sendo forjado e controla sua barra de progresso.
- **UpgradeSystem**: lógica de compra de melhorias de tap, DPS e bots.
- **TapController**: detecta cliques/toques na tela e aciona o martelo.
- **UIController**: atualiza os textos de nível, créditos e status.

## Possíveis extensões futuras
- Novos tipos de itens e efeitos visuais durante a forja.
- Mais categorias de upgrades e árvore de habilidades.
- Implementação de **FloatingText** para feedback de dano/recompensas.
- Integração de áudio e animações.
- Suporte a multiplataforma (mobile/desktop).

---
Protótipo criado para fins de estudo e demonstração.
