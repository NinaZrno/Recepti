import { useEffect, useState } from "react"
import SastojciService from "../../services/SastojciService"
import { Button, Table } from "react-bootstrap";
import { NumericFormat } from "react-number-format";
import moment from "moment";
import { GrValidate } from "react-icons/gr";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";


export default function SastojciPregled(){

    const[sastojci, setSastojci] = useState();
    const navigate = useNavigate();

    async function dohvatiSastojke(){
        const odgovor = await SastojciService.get()
        setSastojci(odgovor)
    }

    // hooks (kuka) se izvodi prilikom dolaska na stranicu Smjerovi
    useEffect(()=>{
        dohvatiSastojke();
    },[])


    

   

   

    function obrisi(sifra){
        if(!confirm('Sigurno obrisati')){
            return;
        }
        brisanjeSastojka(sifra);
    }

    async function brisanjeSastojka(sifra) {
        const odgovor = await SastojciService.obrisi(sifra);
        if(odgovor.greska){
            alert(odgovor.poruka);
            return;
        }
        dohvatiSastojke();
    }


    return(
        <>
        <Link
        to={RouteNames.SASTOJAK_NOVI}
        className="btn btn-success siroko"
        >Dodaj novi recept</Link>
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Naziv</th>
                    <th>Mjerna Jedinica</th>
                    <th>Podrijetlo</th>
                    <th>Energija</th>
                    <th>Ugljikohidrati</th>
                    <th>Masti</th>
                    <th>Zasićeni šećeri</th>
                    <th>Vlakna</th>
                    <th>Bjelancevine</th>
                    <th>Sol</th>
                </tr>
            </thead>
            <tbody>
                {sastojci && sastojci.map((sastojak,index)=>(
                    <tr key={index}>
                        <td>
                            {sastojak.naziv}
                        </td>
                        <td>
                            {sastojak.mjernaJedinica}
                        </td>
                        <td>
                            {sastojak.podrijetlo}
                        </td>
                        <td>
                            {sastojak.energija}
                        </td>
                        <td>
                            {sastojak.ugljikohidrati}
                        </td>
                        <td>
                            {sastojak.masti}
                        </td>
                        <td>
                            {sastojak.zasiceniSeceri}
                        </td>
                        <td>
                            {sastojak.vlakna}
                        </td>
                        <td>
                            {sastojak.bjelancevine}
                        </td>
                        <td>
                            {sastojak.sol}
                        </td>
                        

                        <td>
                            <Button
                            onClick={()=>navigate(`/sastojci/${sastojak.sifra}`)}
                            >Promjena</Button>
                            &nbsp;&nbsp;&nbsp;
                            <Button
                            variant="danger"
                            onClick={()=>obrisi(sastojak.sifra)}
                            >Obriši</Button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </Table>
        </>
    )


}