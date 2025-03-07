import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import moment from "moment";
import SastojciService from "../../services/SastojciService";
import { useEffect, useState } from "react";


export default function SastojciPromjena(){

    const navigate = useNavigate();
    const [sastojak,setSastojak] = useState({});
    const [vaucer,setVaucer] = useState(false)
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
                mjerna_jedinica: podaci.get('mjerna_jedinica'),
                podrijetlo: podaci.get('podrijetlo'),
                energija: parseInt(podaci.get('energija')),
                ugljikohidrati: parseInt(podaci.get('ugljikohidrati')),
                masti: parseInt(podaci.get('masti')),
                zasiceni_seceri: parseInt(podaci.get('zasiceni_seceri')),
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
            <Form.Control type="text" name="naziv" required />
        </Form.Group>

        <Form.Group controlId="mjerna_jedinica">
            <Form.Label>Mjerna_jedinica</Form.Label>
            <Form.Control type="text" name="mjerna_jedinica" required />
        </Form.Group>

        <Form.Group controlId="podrijetlo">
            <Form.Label>Podrijetlo</Form.Label>
            <Form.Control type="text" name="podrijetlo" required />
        </Form.Group>


        <Form.Group controlId="energija">
        <Form.Label>Energija</Form.Label>
        <Form.Control type="number" name="energija" required />
        </Form.Group>

        <Form.Group controlId="ugljikohidrati">
        <Form.Label>Ugljikohidrati</Form.Label>
        <Form.Control type="number" name="ugljikohidrati" required />
        </Form.Group>

        <Form.Group controlId="masti">
        <Form.Label>Masti</Form.Label>
        <Form.Control type="number" name="masti" required />
        </Form.Group>

        <Form.Group controlId="zasiceni_seceri">
        <Form.Label>Zasiceni_seceri</Form.Label>
        <Form.Control type="number" name="zasiceni_seceri" required />
        </Form.Group>

        <Form.Group controlId="vlakna">
        <Form.Label>Vlakna</Form.Label>
        <Form.Control type="number" name="vlakna" required />
        </Form.Group>

        <Form.Group controlId="bjelancevine">
        <Form.Label>Bjelancevine</Form.Label>
        <Form.Control type="number" name="bjelancevine" required />
        </Form.Group>

        <Form.Group controlId="sol">
        <Form.Label>Sol</Form.Label>
        <Form.Control type="number" name="sol" required />
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