import BarraLateral from "../../components/BarraLateral";
import "./Pessoas.css";
import { useState, useEffect } from "react";

type Pessoa = {
  id: number;
  nome: string;
  idade: number;
};

function Pessoas() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

  const [pesquisa, setPesquisa] = useState("");

  const [mostrarFormulario, setMostrarFormulario] = useState(false);

  const [nome, setNome] = useState("");

  const [idade, setIdade] = useState("");

  const [pessoaEditandoId, setPessoaEditandoId] = useState<number | null>(null);

  useEffect(() => {
    async function CarregarPessoas() {
      const response = await fetch("https://localhost:7198/api/Pessoa");
      const dados = await response.json();
      setPessoas(dados);
    }
    CarregarPessoas();
  }, []);

  const pessoasFiltradas = pessoas.filter((pessoa) =>
    pessoa.nome.toLowerCase().includes(pesquisa.toLowerCase()),
  );

  async function cadastrarPessoa() {
    if (!nome.trim || !idade) {
      alert("Preencha o nome e a idade.");
      return;
    }
    try {
      const response = await fetch("https://localhost:7198/api/Pessoa", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          nome: nome.trim(),
          idade: Number(idade),
        }),
      });

      if (!response.ok) {
        const mensagem = await response.text();
        throw new Error(mensagem || "Erro ao cadastrar pessoa.");
      }

      const novaPessoa: Pessoa = await response.json();

      setPessoas((pessoasAtuais) => [...pessoasAtuais, novaPessoa]);

      limparFormulario();
    } catch (erro) {
      console.error("Erro ao cadastrar pessoa:", erro);
      alert("Não foi possível cadastrar a pessoa.");
    }
  }

  async function excluirPessoa(id: number) {
    const confirmar = window.confirm(
      "Tem certeza que deseja excluir esta pessoa?",
    );

    if (!confirmar) {
      return;
    }

    try {
      const response = await fetch(`https://localhost:7198/api/Pessoa/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        const mensagem = await response.text();
        throw new Error(mensagem || "Erro ao excluir pessoa.");
      }

      setPessoas((pessoasAtuais) =>
        pessoasAtuais.filter((pessoa) => pessoa.id !== id),
      );
    } catch (erro) {
      console.error("Erro ao excluir pessoa:", erro);
      alert("Não foi possível excluir a pessoa.");
    }
  }

  function limparFormulario() {
    setNome("");
    setIdade("");
    setPessoaEditandoId(null);
    setMostrarFormulario(false);
  }

  function iniciarEdicao(pessoa: Pessoa) {
    setPessoaEditandoId(pessoa.id);
    setNome(pessoa.nome);
    setIdade(String(pessoa.idade));
    setMostrarFormulario(true);
  }

  async function editarPessoa() {
    if (pessoaEditandoId === null) {
      return;
    }

    if (!nome.trim() || !idade) {
      alert("Preencha o nome e a idade.");
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7198/api/Pessoa/${pessoaEditandoId}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            nome: nome.trim(),
            idade: Number(idade),
          }),
        },
      );

      if (!response.ok) {
        const mensagem = await response.text();
        throw new Error(mensagem || "Erro ao editar pessoa.");
      }

      setPessoas((pessoasAtuais) =>
        pessoasAtuais.map((pessoa) =>
          pessoa.id === pessoaEditandoId
            ? {
                ...pessoa,
                nome: nome.trim(),
                idade: Number(idade),
              }
            : pessoa,
        ),
      );

      limparFormulario();
    } catch (erro) {
      console.error("Erro ao editar pessoa:", erro);
      alert("Não foi possível editar a pessoa.");
    }
  }

  return (
    <div className="pessoas-layout">
      <BarraLateral />

      <main className="pessoas-conteudo">
        <header>
          <h1 className="h1-title">Pessoas</h1>
          <p>Cadastre e gerencie as pessoas da residência.</p>
        </header>

        <div>
          <input
            type="search"
            placeholder="Buscar Pessoas..."
            className="input-pesquisa"
            value={pesquisa}
            onChange={(evento) => setPesquisa(evento.target.value)}
          />
          <button
            type="button"
            className="btnNovaPessoa"
            onClick={() => {
              setPessoaEditandoId(null);
              setNome("");
              setIdade("");
              setMostrarFormulario(true);
            }}
          >
            + Nova Pessoa
          </button>
          {mostrarFormulario && (
            <div className="formulario-pessoa">
              <h2>
                {pessoaEditandoId === null ? "Nova Pessoa" : "Editar Pessoa"}
              </h2>

              <div>
                <label htmlFor="nome">Nome</label>
                <input
                  id="nome"
                  type="text"
                  placeholder="Digite o nome"
                  value={nome}
                  onChange={(evento) => setNome(evento.target.value)}
                />
              </div>

              <div>
                <label htmlFor="idade">Idade</label>
                <input
                  id="idade"
                  type="number"
                  placeholder="Digite a idade"
                  value={idade}
                  onChange={(evento) => setIdade(evento.target.value)}
                />
              </div>

              <div>
                <button
                  type="button"
                  onClick={
                    pessoaEditandoId === null ? cadastrarPessoa : editarPessoa
                  }
                >
                  {pessoaEditandoId === null
                    ? "Cadastrar"
                    : "Salvar alterações"}
                </button>

                <button type="button" onClick={() => limparFormulario()}>
                  Cancelar
                </button>
              </div>
            </div>
          )}
        </div>

        <table className="tabela-pessoas">
          <thead>
            <tr>
              <th>Nome</th>
              <th>Idade</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {pessoasFiltradas.map((pessoa) => (
              <tr key={pessoa.id}>
                <td>{pessoa.nome}</td>
                <td>{pessoa.idade} Anos</td>
                <td>
                  <button type="button" className="btnVerPessoa">
                    Visualizar
                  </button>
                  <button
                    type="button"
                    className="btnEditarPessoa"
                    onClick={() => iniciarEdicao(pessoa)}
                  >
                    Editar
                  </button>
                  <button
                    type="button"
                    className="btnExcluirPessoa"
                    onClick={() => excluirPessoa(pessoa.id)}
                  >
                    Excluir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </main>
    </div>
  );
}
export default Pessoas;
