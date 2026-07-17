import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Home.css";
import BarraLateral from "../../components/BarraLateral";
import { API_URL } from "../../config/api";

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
    {/* Carrega os Dados do Banco de DAdos*/}
    async function carregarTotal() {
      try {
        {/* Recebe os Dados do Banco*/}
        const response = await fetch(`${API_URL}/api/Total/Geral`);
        //Caso nao retorne uma resposta, retorna um erro
        if (!response.ok) {
          throw new Error("Erro ao carregar os totais.");
        }

        const dados: TotalGeral = await response.json();
        setTotal(dados);

      } catch (erro) {
        //Qualquer erro na resposta retorna um erro com a mensagem do backend
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
        {/* Cabeçalho da pagina*/}
        <header>
          <h1 className="h1-title">Controle de Gastos Residenciais</h1>
          <p>Resumo financeiro da residência.</p>
        </header>
        {/*Se estiver carregando algum dado, exibe a mensagem carregando */}
        {carregando && <p>Carregando...</p>}

        {/*Se der erro, exibe mensagem de erro*/}
        {erro && <p className="mensagem-erro">{erro}</p>}

        {/*Caso nao de erro nem esteja carregando nenhum dado, exibe a pagina completa*/}
        {!carregando && !erro && (
          <>
          {/* Receita Geral*/}
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
              {/* Despesa Geral*/}
              <div className="card-total">
                <span>Despesas</span>
                <strong>
                  {(total?.totalGeralDespesas ?? 0).toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </strong>
              </div>
              {/* Saldo Geral*/}
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
            {/* Botao Gerenciar Pessoas*/}
            <section className="atalhos-home">
              <button type="button" onClick={() => navigate("/pessoas")}>
                Gerenciar Pessoas
              </button>
              {/* Botao Gerenciar Transacoes*/}
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
