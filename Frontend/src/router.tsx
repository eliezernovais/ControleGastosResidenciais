import {Route, Routes, BrowserRouter} from 'react-router-dom'
import Home from './pages/Home/Home';
import Pessoas from './pages/Pessoas/Pessoas';
import PessoaDetalhes from './pages/Pessoas/PessoaDetalhes';
import Transacoes from './pages/Transacoes/Transacoes'
function Router(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" element = {<Home/>}/>
                <Route path="/pessoas" element = {<Pessoas/>}/>
                <Route path="/pessoas/:id" element = {<PessoaDetalhes/>}/>
                <Route path="/transacoes" element = {<Transacoes/>}/>
            </Routes>
        </BrowserRouter>
    )
}

export default Router;