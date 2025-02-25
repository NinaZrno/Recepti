import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css'
import { Container } from 'react-bootstrap'
import NavBarEdunova from './components/NavBarEdunova'
import { Route, Routes } from 'react-router-dom'
import { RouteNames } from './constants'
import Pocetna from './pages/Pocetna'
import ReceptiPregled from './pages/recepti/ReceptiPregled'
import ReceptiDodaj from './pages/recepti/ReceptiDodaj'
import ReceptiPromjena from './pages/recepti/ReceptiPromjena'



function App() {

  return (
    <>
      <Container>
        <NavBarEdunova />
        
        <Routes>
          <Route path={RouteNames.HOME} element={<Pocetna />} />
          <Route path={RouteNames.SMJER_PREGLED} element={<ReceptiPregled />} />
          <Route path={RouteNames.SMJER_NOVI} element={<ReceptiDodaj />} />
          <Route path={RouteNames.SMJER_PROMJENA} element={<ReceptiPromjena />} />
        </Routes>

        <hr />
        &copy; Recepti 2025
      </Container>
     
    </>
  )
}

export default App
