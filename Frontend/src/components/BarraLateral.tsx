import { NavLink } from "react-router-dom";
import "./BarraLateral.css"
function BarraLateral() {
  return (
    <aside className="sidebar">
      <h2>Controle de Gastos</h2>
      <nav>
        <NavLink to="/">Inicio</NavLink>
        <NavLink to="/pessoas">Pessoas</NavLink>
        <NavLink to="/transacoes">Transações</NavLink>
      </nav>
    </aside>
  );
}

export default BarraLateral;
