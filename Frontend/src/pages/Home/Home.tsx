import "./Home.css";
import BarraLateral from "../../components/BarraLateral";
function Home() {
  return (
      <div className = "home-layout">
        <BarraLateral />
        <main className = "home-conteudo">
          <div>
            <h1>Controle de Gastos Residenciais</h1>
          </div>
          <div>
            <h2>Sistema de Controle de gastos Residenciais</h2>
          </div>
          <div>
            <button>Cadastro de Pessoas</button>
          </div>
          <div>
            <button>Cadastro de Transações</button>
          </div>
          <div>
            <button>Consulta de Totais</button>
          </div>
        </main>
      </div>
  );
}

export default Home;
