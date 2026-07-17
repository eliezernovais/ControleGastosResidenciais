import { useEffect, useState } from "react";
import BarraLateral from "../../components/BarraLateral";
import "./Transacoes.css";
import { API_URL } from "../../config/api";

type Transacao = {
  id: number;
  descricao: string;
  valor: number;
  tipo: number;
  pessoaId: number;
};

type Pessoa = {
  id: number;
  nome: string;
  idade: number;
};

function Transacoes() {
  const [mostrarFormulario, setMostrarFormulario] = useState(false);

  const [descricao, setDescricao] = useState("");

  const [valor, setValor] = useState("");

  const [tipo, setTipo] = useState("1");

  const [pessoaId, setPessoaId] = useState("");

  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

  const [transacoes, setTransacoes] = useState<Transacao[]>([]);

  const [carregando, setCarregando] = useState(true);

  const [erro, setErro] = useState("");

  const [pesquisa, setPesquisa] = useState("");

  const [filtroTipo, setFiltroTipo] = useState("todos");

  const transacoesFiltradas = transacoes.filter((transacao) => {
    const descricaoCorresponde = transacao.descricao
      .toLowerCase()
      .includes(pesquisa.toLowerCase());

    const tipoCorresponde =
      filtroTipo === "todos" || transacao.tipo === Number(filtroTipo);

    return descricaoCorresponde && tipoCorresponde;
  });

  useEffect(() => {
    async function carregarDados() {
      try {
        //Recebe os Dados do Banco
        const [responseTransacoes, responsePessoas] = await Promise.all([
          fetch(`${API_URL}/api/Transacao`),
          fetch(`${API_URL}/api/Pessoa`),
        ]);

        //Mensagem de Erro se nao retornar Dados de Transacoes
        if (!responseTransacoes.ok) {
          throw new Error("Não foi possível buscar as transações.");
        }

        //Mensagem de Erro se nao retornar Dados de Pessoas
        if (!responsePessoas.ok) {
          throw new Error("Não foi possível buscar as pessoas.");
        }
        const dadosTransacoes: Transacao[] = await responseTransacoes.json();

        const dadosPessoas: Pessoa[] = await responsePessoas.json();

        setTransacoes(dadosTransacoes);
        setPessoas(dadosPessoas);
      } catch (erro) {
        //Retorna a mensagem de erro do Backend
        console.error("Erro ao carregar os dados:", erro);
        setErro("Não foi possível carregar os dados.");
      } finally {
        setCarregando(false);
      }
    }

    carregarDados();
  }, []);

  //Retorna o nome da pessoa pelo ID
  function buscarNomePessoa(pessoaId: number) {
    const pessoaEncontrada = pessoas.find((pessoa) => pessoa.id === pessoaId);

    return pessoaEncontrada?.nome ?? "Pessoa não encontrada";
  }
  //Cadastra a transacao no Banco de Dados*
  async function cadastrarTransacao() {
    if (!descricao.trim() || !valor || !pessoaId) {
      {
        /* */
      }
      alert("Preencha todos os campos.");
      return;
    }

    try {
      const response = await fetch(`${API_URL}/api/Transacao`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          descricao: descricao.trim(),
          valor: Number(valor),
          tipo: Number(tipo),
          pessoaId: Number(pessoaId),
        }),
      });
      //Se o Banco nao retornar resposta Retorna Mensagem de erro
      if (!response.ok) {
        const mensagem = await response.text();

        throw new Error(mensagem || "Não foi possível cadastrar a transação.");
      }

      const novaTransacao: Transacao = await response.json();

      setTransacoes((transacoesAtuais) => [...transacoesAtuais, novaTransacao]);

      setDescricao("");
      setValor("");
      setTipo("1");
      setPessoaId("");
      setMostrarFormulario(false);
    } catch (erro) {
      //Retorna a mensagem de erro do Backend
      console.error("Erro ao cadastrar transação:", erro);

      const mensagem =
        erro instanceof Error
          ? erro.message
          : "Não foi possível cadastrar a transação.";

      alert(mensagem);
    }
  }

  return (
    <div className="transacoes-layout">
      <BarraLateral />

      <main className="transacoes-conteudo">
        {/* Cabeçalho da pagina*/}
        <header className="transacoes-cabecalho">
          <div>
            <h1 className="h1-title">Transações</h1>
            <p>Consulte todas as receitas e despesas cadastradas.</p>
          </div>
          {/* Botao de Nova Transação*/}
          <button
            type="button"
            className="btnNovaTransacao"
            onClick={() => setMostrarFormulario(true)}
          >
            + Nova Transação
          </button>
          {/* Formulario de Nova Transação*/}
        </header>
        {/* Mostra o formulario se for verdadeiro*/}
        {mostrarFormulario && (
          <section className="formulario-transacao">
            <h2>Nova Transação</h2>

            <div>
              <label htmlFor="descricao">Descrição</label>
              {/* Input da Descrição da Transacao*/}
              <input
                id="descricao"
                type="text"
                placeholder="Digite a descrição"
                value={descricao}
                onChange={(evento) => setDescricao(evento.target.value)}
              />
            </div>

            <div>
              {/*Input do Valor da Transação*/}
              <label htmlFor="valor">Valor</label>
              <input
                id="valor"
                type="number"
                min="0.01"
                step="0.01"
                placeholder="Digite o valor"
                value={valor}
                onChange={(evento) => setValor(evento.target.value)}
              />
            </div>

            <div>
              <label htmlFor="tipo">Tipo</label>
              {/*Select do Tipo da Transação */}
              <select
                id="tipo"
                value={tipo}
                onChange={(evento) => setTipo(evento.target.value)}
              >
                <option value="1">Despesa</option>
                <option value="2">Receita</option>
              </select>
            </div>

            <div>
              <label htmlFor="pessoa">Pessoa</label>
              {/*Select da Pessoa responsavel pela transacao */}
              <select
                id="pessoa"
                value={pessoaId}
                onChange={(evento) => setPessoaId(evento.target.value)}
              >
                <option value="">Selecione uma pessoa</option>

                {pessoas.map((pessoa) => (
                  <option key={pessoa.id} value={pessoa.id}>
                    {pessoa.nome}
                  </option>
                ))}
              </select>
            </div>

            <div className="formulario-acoes">
              {/*Botao de Salvar Transacao*/}
              <button
                type="button"
                className="btn-salvar"
                onClick={() => cadastrarTransacao()}
              >
                Salvar
              </button>
              {/*Botao de Cancelar Transação*/}
              <button
                type="button"
                className="btn-cancelar"
                onClick={() => setMostrarFormulario(false)}
              >
                Cancelar
              </button>
            </div>
          </section>
        )}
        {/*Filtro das Transações*/}
        <div className="filtros-transacoes">
          {/*Input do Filtro das Transações */}
          <input
            type="search"
            placeholder="Buscar por descrição..."
            value={pesquisa}
            onChange={(evento) => setPesquisa(evento.target.value)}
          />
          {/*Select do Tipo da Transação */}
          <select
            value={filtroTipo}
            onChange={(evento) => setFiltroTipo(evento.target.value)}
          >
            <option value="todos">Todos</option>
            <option value="1">Despesas</option>
            <option value="2">Receitas</option>
          </select>
        </div>
        {/* Se estiver Carregando Mostra mensagem de carregando*/}
        {carregando && <p>Carregando transações...</p>}
        {/* Se der erro mostra mensagem de erro*/}
        {erro && <p className="mensagem-erro">{erro}</p>}
        {/* Caso nao esteja carregando nem ocorrido nenhum erro, mostra a tabela */}
        {!carregando && !erro && (
          <table className="tabela-transacoes">
            {/* Tabela das Transações*/}
            <thead>
              <tr>
                <th>Descrição</th>
                <th>Pessoa</th>
                <th>Tipo</th>
                <th>Valor</th>
              </tr>
            </thead>

            <tbody>
              {/* Mostra as Transações Filtradas*/}
              {transacoesFiltradas.length === 0 ? (
                <tr>
                  {/*Mensagem se nao encontrar nenhuma transação */}
                  <td colSpan={4}>Nenhuma transação cadastrada.</td>
                </tr>
              ) : (
                transacoesFiltradas.map((transacao) => (
                  //Mapeia as Transações Encontradas para sua respectiva Linha, sendo a key da linha o id da transação
                  <tr key={transacao.id}>
                    {/* Descrição da Transação na Tabela*/}
                    <td>{transacao.descricao}</td>
                    {/* Nome da Pessoa responsavel pela transação na Tabela */}
                    <td>{buscarNomePessoa(transacao.pessoaId)}</td>
                    {/* Tipo da Transação na Tabela*/}
                    <td>{transacao.tipo === 1 ? "Despesa" : "Receita"}</td>
                    {/*Valor convertido em reais na tabela */}
                    <td>
                      {transacao.valor.toLocaleString("pt-BR", {
                        style: "currency",
                        currency: "BRL",
                      })}
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        )}
      </main>
    </div>
  );
}

export default Transacoes;
