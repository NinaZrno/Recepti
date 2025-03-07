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
import SastojciPregled from './pages/sastojci/SastojciPregled'
import SastojciDodaj from './pages/sastojci/SastojciDodaj'
import SastojciPromjena from './pages/sastojci/SastojciPromjena'



function App() {

  return (
    <>
      <Container>
        <NavBarEdunova />
        
        <Routes>
          <Route path={RouteNames.HOME} element={<Pocetna />} />
          <Route path={RouteNames.RECEPT_PREGLED} element={<ReceptiPregled />} />
          <Route path={RouteNames.RECEPT_NOVI} element={<ReceptiDodaj />} />
          <Route path={RouteNames.RECEPT_PROMJENA} element={<ReceptiPromjena />} />

          <Route path={RouteNames.SASTOJAK_PREGLED} element={<SastojciPregled />} />
          <Route path={RouteNames.SASTOJAK_NOVI} element={<SastojciDodaj />} />
          <Route path={RouteNames.SASTOJAK_PROMJENA} element={<SastojciPromjena />} />
          
        </Routes>

        <hr />
        &copy; Recepti 2025
      </Container>
     
    </>
  )
}

export default App
