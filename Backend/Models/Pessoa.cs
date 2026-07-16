namespace Backend.Models
{
    public class Pessoa
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public int Idade { get; private set; }
        public ICollection<Transacao> Transacoes { get; private set; } = new List<Transacao>();
        private Pessoa() { }
        public Pessoa(string nome,int idade) {
            Atualizar(nome, idade);
        }
        public void Atualizar(string nome,int idade){
        // Verifica se o nome inserido é nulo ou um espaço em branco
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O Nome é Obrigatorio", nameof(nome));
            }
            if (idade < 0)
                {
                    throw new ArgumentException("A Idade nao pode ser Negativa", nameof(idade));
            }
            Nome = nome;
            Idade = idade;
        }
        // Verifica se a pessoa é menor de idade
        public bool MenorDeIdade(){
            return Idade < 18;
        }
    }
}
