import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import BarraLateral from "../../components/BarraLateral";
import "./PessoaDetalhes.css";
import { API_URL } from "../../config/api";

type Pessoa = {
  id: number;
  nome: string;
  idade: number;
};

type Transacao = {
  id: number;
  descricao: string;
  valor: number;
  tipo: number;
  pessoaId: number;
};

type TotalPessoa = {
  pessoaId: number;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
};

function PessoaDetalhes() {
  const navigate = useNavigate();

  const [total, setTotal] = useState<TotalPessoa | null>(null);

  const [transacoes, setTransacoes] = useState<Transacao[]>([]);

  const { id } = useParams();

  const [pessoa, setPessoa] = useState<Pessoa | null>(null);

  const [carregando, setCarregando] = useState(true);

  const [erro, setErro] = useState("");

  useEffect(() => {
    async function carregarDados() {
      try {
        const [response, responseTransacoes, responseTotal] = await Promise.all(
          [
            fetch(`${API_URL}/api/Pessoa/${id}`),
            fetch(`${API_URL}/api/Transacao/Pessoa/${id}`),
            fetch(`${API_URL}/api/Total/Pessoa/${id}`),
          ],
        );
        if (responseTotal.ok) {
          const dadosTotal: TotalPessoa = await responseTotal.json();
          setTotal(dadosTotal);
        } else if (responseTotal.status === 404) {
          setTotal(null);
        } else {
          throw new Error("Erro ao carregar os totais.");
        }
        if (responseTransacoes.ok) {
          const dadosTransacoes: Transacao[] = await responseTransacoes.json();

          setTransacoes(dadosTransacoes);
        } else if (responseTransacoes.status === 404) {
          setTransacoes([]);
        } else {
          throw new Error("Erro ao carregar as transações.");
        }
        if (!response.ok) {
          throw new Error("Pessoa não encontrada.");
        }

        const dados: Pessoa = await response.json();

        setPessoa(dados);
      } catch (erro) {
        console.error("Erro ao carregar pessoa:", erro);
        setErro("Não foi possível carregar os dados da pessoa.");
      } finally {
        setCarregando(false);
      }
    }

    carregarDados();
  }, [id]);
  return (
    <div className="pessoa-detalhes-layout">
      <BarraLateral />

      <main className="pessoa-detalhes-conteudo">
        {carregando ? (
          <p>Carregando...</p>
        ) : erro ? (
          <p>{erro}</p>
        ) : pessoa === null ? (
          <p>Pessoa não encontrada.</p>
        ) : (
          <>
            <button
              type="button"
              className="btn-voltar"
              onClick={() => navigate("/pessoas")}
            >
              ← Voltar
            </button>

            <header className="header-pessoa-selecionada">
              <h1>{pessoa.nome}</h1>
              <p>{pessoa.idade} anos</p>
            </header>

            <section className="pessoa-informacoes">
              <h2>Informações pessoais</h2>

              <div className="informacoes-grid">
                <div>
                  <span>ID</span>
                  <strong>{pessoa.id}</strong>
                </div>

                <div>
                  <span>Nome</span>
                  <strong>{pessoa.nome}</strong>
                </div>

                <div>
                  <span>Idade</span>
                  <strong>{pessoa.idade} anos</strong>
                </div>
              </div>
            </section>
            <section className="cards-totais">
              <div className="card-total card-receita">
                <span>Receitas</span>
                <strong>
                  {/* Se o total nao for nulo, pega as receitastotais, se nao usa 0*/}
                  {(total?.totalReceitas ?? 0).toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </strong>
              </div>

              <div className="card-total card-despesa">
                <span>Despesas</span>
                <strong>
                  {(total?.totalDespesas ?? 0).toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </strong>
              </div>

              <div className="card-total card-saldo">
                <span>Saldo</span>
                <strong>
                  {(total?.saldo ?? 0).toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </strong>
              </div>
            </section>
            <section className="secao-transacoes">
              <h2>Transações</h2>

              {transacoes.length === 0 ? (
                <p>Nenhuma transação encontrada para esta pessoa.</p>
              ) : (
                <table className="tabela-transacoes">
                  <thead>
                    <tr>
                      <th>Descrição</th>
                      <th>Tipo</th>
                      <th>Valor</th>
                    </tr>
                  </thead>

                  <tbody>
                    {transacoes.map((transacao) => (
                      <tr key={transacao.id}>
                        <td>{transacao.descricao}</td>

                        <td>{transacao.tipo === 1 ? "Despesa" : "Receita"}</td>

                        <td>
                          {transacao.valor.toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL",
                          })}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </section>
          </>
        )}
      </main>
    </div>
  );
}

export default PessoaDetalhes;
