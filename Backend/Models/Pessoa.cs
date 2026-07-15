namespace Backend.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; private set; } = string.Empty;
        public int Idade { get; private set; }
        private Pessoa() { }
        public Pessoa(string nome,int idade) {
            AlterarNome(nome);
            AlterarIdade(idade);

        }
        public void AlterarNome(string nome){
        // Verifica se o nome inserido é nulo ou um espaço em branco
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O Nome é Obrigatorio", nameof(nome));
            }
            Nome = nome;
        }
        public void AlterarIdade(int idade) {
        // Verifica se a idade é negativa
            if (idade < 0)
                {
                    throw new ArgumentException("A Idade nao pode ser Negativa", nameof(idade));
                }
            Idade = idade;
        }
        // Verifica se a pessoa é menor de idade
        public bool MenorDeIdade(){
            return Idade < 18;
        }
    }
}
