using Backend.Enums;

namespace Backend.Models
{
    public class Transacao
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public int PessoaId { get; private set; }
        public Pessoa Pessoa { get; private set; } = null!;

        private Transacao() { }
        public Transacao(string descricao, decimal valor, TipoTransacao tipo, int pessoaId) {
            // Verifica se a Descrição é vazia
            if(string.IsNullOrWhiteSpace(descricao)) {
                throw new ArgumentException("A descrição nao deve ser vazia", nameof(descricao));
            }
            // Verifica se o Valor é menor ou igual á zero
            else if(valor <= 0){
                throw new ArgumentException("O valor deve ser maior que zero", nameof(valor));
            }
            else {
                Descricao = descricao;
                Valor = valor;
                Tipo = tipo;
                PessoaId = pessoaId;
            }
        }

    }
}
