---

## 🧠 Princípios SOLID Evidentes

Este projeto foi desenvolvido seguindo boas práticas de arquitetura e programação orientada a objetos, especialmente os princípios SOLID:

- **SRP (Single Responsibility Principle):**
  - Cada classe tem uma responsabilidade única. Por exemplo, `PokemonService` cuida apenas da lógica de negócio, enquanto `PokemonRepository` trata da persistência, e `PokeApiClient` é responsável pela integração externa.

- **OCP (Open/Closed Principle):**
  - O sistema permite extensão sem modificação, através do uso de interfaces como `IPokemonRepository` e `IPokeApiClient`. Novas implementações podem ser adicionadas sem alterar o código existente.

- **DIP (Dependency Inversion Principle):**
  - As dependências são invertidas: classes dependem de abstrações, não de implementações concretas. Isso é evidenciado pelo uso de interfaces e injeção de dependência, tornando o projeto desacoplado e facilitando testes.

---
