import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Home.css";
import BarraLateral from "../../components/BarraLateral";

type TotalGeral = {
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoGeral: number;
};

function Home() {
  const [total, setTotal] = useState<TotalGeral | null>(null);

  const [carregando, setCarregando] = useState(true);

  const [erro, setErro] = useState("");

  const navigate = useNavigate();

  useEffect(() => {
    async function carregarTotal() {
      try {
        const response = await fetch("https://localhost:7198/api/Total/Geral");

        if (!response.ok) {
          throw new Error("Erro ao carregar os totais.");
        }

        const dados: TotalGeral = await response.json();
        setTotal(dados);
      } catch (erro) {
        console.error("Erro ao carregar total geral:", erro);
        setErro("Não foi possível carregar os totais.");
      } finally {
        setCarregando(false);
      }
    }

    carregarTotal();
  }, []);

  return (
    <div className="home-layout">
      <BarraLateral />

      <main className="home-conteudo">
        <header>
          <h1 className="h1-title">Controle de Gastos Residenciais</h1>
          <p>Resumo financeiro da residência.</p>
        </header>

        {carregando && <p>Carregando...</p>}

        {erro && <p className="mensagem-erro">{erro}</p>}

        {!carregando && !erro && (
          <>
            <section className="cards-totais">
              <div className="card-total">
                <span>Receitas</span>
                <strong>
                  {(total?.totalGeralReceitas ?? 0).toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </strong>
              </div>

              <div className="card-total">
                <span>Despesas</span>
                <strong>
                  {(total?.totalGeralDespesas ?? 0).toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </strong>
              </div>

              <div className="card-total">
                <span>Saldo</span>
                <strong>
                  {(total?.saldoGeral ?? 0).toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </strong>
              </div>
            </section>

            <section className="atalhos-home">
              <button type="button" onClick={() => navigate("/pessoas")}>
                Gerenciar Pessoas
              </button>

              <button type="button" onClick={() => navigate("/transacoes")}>
                Gerenciar Transações
              </button>
            </section>
          </>
        )}
      </main>
    </div>
  );
}

export default Home;
