import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import moment from "moment";
import SastojciService from "../../services/SastojciService";
import { useEffect, useState } from "react";


export default function SastojciPromjena(){

    const navigate = useNavigate();
    const [sastojak,setSastojak] = useState({});
    const routeParams = useParams();

    async function dohvatiSastojak(){
        const odgovor = await SastojciService.getBySifra(routeParams.sifra)

        if(odgovor.izvodiSeOd!=null){
            odgovor.izvodiSeOd = moment.utc(odgovor.izvodiSeOd).format('yyyy-MM-DD')
        }
        
        setSastojak(odgovor)
        setVaucer(odgovor.vaucer)
    }

    useEffect(()=>{
        dohvatiSastojak();
    },[])

    async function promjena(sastojak){
        const odgovor = await SastojciService.promjena(routeParams.sifra,sastojak);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.SASTOJAK_PREGLED)
    }

    function odradiSubmit(e){ // e je event
        e.preventDefault(); // nemoj odraditi zahtjev na server pa standardnom načinu

        let podaci = new FormData(e.target);

        promjena(
            {
                naziv: podaci.get('naziv'),
                mjernaJedinica: podaci.get('mjernaJedinica'),
                podrijetlo: podaci.get('podrijetlo'),
                energija: parseInt(podaci.get('energija')),
                ugljikohidrati: parseInt(podaci.get('ugljikohidrati')),
                masti: parseInt(podaci.get('masti')),
                zasiceniSeceri: parseInt(podaci.get('zasiceniSeceri')),
                vlakna: parseInt(podaci.get('vlakna')),
                bjelancevine: parseInt(podaci.get('bjelancevine')),
                sol: parseInt(podaci.get('sol'))
            }
        );
    }

    return(
    <>
    Promjena recepta
    <Form onSubmit={odradiSubmit}>

    <Form.Group controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" required defaultValue={sastojak.naziv} />
        </Form.Group>

        <Form.Group controlId="mjernaJedinica">
            <Form.Label>Mjerna_jedinica</Form.Label>
            <Form.Control type="text" name="mjernaJedinica" required  defaultValue={sastojak.mjernaJedinica}/>
        </Form.Group>

        <Form.Group controlId="podrijetlo">
            <Form.Label>Podrijetlo</Form.Label>
            <Form.Control type="text" name="podrijetlo" required  defaultValue={sastojak.podrijetlo}/>
        </Form.Group>


        <Form.Group controlId="energija">
        <Form.Label>Energija</Form.Label>
        <Form.Control type="number" name="energija" required  defaultValue={sastojak.energija}/>
        </Form.Group>

        <Form.Group controlId="ugljikohidrati">
        <Form.Label>Ugljikohidrati</Form.Label>
        <Form.Control type="number" name="ugljikohidrati" required  defaultValue={sastojak.ugljikohidrati}/>
        </Form.Group>

        <Form.Group controlId="masti">
        <Form.Label>Masti</Form.Label>
        <Form.Control type="number" name="masti" required  defaultValue={sastojak.masti}/>
        </Form.Group>

        <Form.Group controlId="zasiceniSeceri">
        <Form.Label>Zasiceni_seceri</Form.Label>
        <Form.Control type="number" name="zasiceniSeceri" required  defaultValue={sastojak.zasiceniSeceri}/>
        </Form.Group>

        <Form.Group controlId="vlakna">
        <Form.Label>Vlakna</Form.Label>
        <Form.Control type="number" name="vlakna" required  defaultValue={sastojak.vlakna}/>
        </Form.Group>

        <Form.Group controlId="bjelancevine">
        <Form.Label>Bjelancevine</Form.Label>
        <Form.Control type="number" name="bjelancevine" required  defaultValue={sastojak.bjelancevine}/>
        </Form.Group>

        <Form.Group controlId="sol">
        <Form.Label>Sol</Form.Label>
        <Form.Control type="number" name="sol" required  defaultValue={sastojak.sol}/>
        </Form.Group>
        <Row>
            <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                <Link
                to={RouteNames.SASTOJAK_PREGLED}
                className="btn btn-danger siroko"
                >Odustani</Link>
            </Col>
            <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko">
                    Promjeni sastojak
                </Button>
            </Col>
        </Row>


    </Form>




   
    </>
    )
}