import { useEffect, useState } from "react"
import ReceptService from "../../services/ReceptService"
import { Button, Table } from "react-bootstrap";
import { NumericFormat } from "react-number-format";
import moment from "moment";
import { GrValidate } from "react-icons/gr";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";


export default function ReceptiPregled(){

    const[recepti, setRecepti] = useState();
    const navigate = useNavigate();

    async function dohvatiRecepte(){
        const odgovor = await ReceptService.get()
        setRecepti(odgovor)
    }

    // hooks (kuka) se izvodi prilikom dolaska na stranicu Smjerovi
    useEffect(()=>{
        dohvatiRecepte();
    },[])


    function formatirajDatum(datum){
        if(datum==null){
            return 'Nije definirano'
        }
        return moment.utc(datum).format('DD. MM. YYYY.')
    }

    function vaucer(v){
        if(v==null) return 'gray'
        if(v) return 'green'
        return 'red'
    }

    function vaucerText(v){
        if(v==null) return 'Nije definirano'
        if(v) return 'Vaučer'
        return 'NIJE vaučer'
    }

    function obrisi(sifra){
        if(!confirm('Sigurno obrisati')){
            return;
        }
        brisanjeRecepta(sifra);
    }

    async function brisanjeRecepta(sifra) {
        const odgovor = await ReceptService.obrisi(sifra);
        if(odgovor.greska){
            alert(odgovor.poruka);
            return;
        }
        dohvatiRecepte();
    }


    return(
        <>
        <Link
        to={RouteNames.SMJER_NOVI}
        className="btn btn-success siroko"
        >Dodaj novi smjer</Link>
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Naziv</th>
                    <th>Vrsta</th>
                    <th>Uputa</th>
                    <th>Trajanje</th>
                    <th>Akcija</th>
                </tr>
            </thead>
            <tbody>
                {recepti && recepti.map((recept,index)=>(
                    <tr key={index}>
                        <td>
                            {recept.naziv}
                        </td>
                        <td>
                            {recept.vrsta}
                        </td>
                        <td>
                            {recept.uputa}
                        </td>
                        <td>
                            {recept.trajanje}
                        </td>
                        <td>
                            {recept.akcija}
                        </td>
                        

                        <td>
                            <Button
                            onClick={()=>navigate(`/recepti/${recept.sifra}`)}
                            >Promjena</Button>
                            &nbsp;&nbsp;&nbsp;
                            <Button
                            variant="danger"
                            onClick={()=>obrisi(recept.sifra)}
                            >Obriši</Button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </Table>
        </>
    )


}