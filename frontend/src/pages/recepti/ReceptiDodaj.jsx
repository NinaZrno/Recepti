import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import moment from "moment";
import ReceptService from "../../services/ReceptService";


export default function ReceptiDodaj(){

    const navigate = useNavigate();

    async function dodaj(recept){
        const odgovor = await ReceptService.dodaj(recept);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.RECEPT_PREGLED)
    }

    function odradiSubmit(e){ // e je event
        e.preventDefault(); // nemoj odraditi zahtjev na server pa standardnom načinu

        let podaci = new FormData(e.target);

        dodaj(
            {
                naziv: podaci.get('naziv'),
                vrsta: podaci.get('vrsta'),
                uputa: podaci.get('uputa'),
                trajanje: parseInt(podaci.get('trajanje'))
            }
        );
    }

    return(
    <>
    Dodavanje recepta
    <Form onSubmit={odradiSubmit}>

        <Form.Group controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" required />
        </Form.Group>

        <Form.Group controlId="vrsta">
            <Form.Label>Vrsta</Form.Label>
            <Form.Control type="text" name="vrsta" required />
        </Form.Group>

        <Form.Group controlId="uputa">
            <Form.Label>Uputa</Form.Label>
            <Form.Control type="text" name="uputa" required />
        </Form.Group>


        <Form.Group controlId="trajanje">
        <Form.Label>Trajanje</Form.Label>
        <Form.Control type="number" name="trajanje" required />
        </Form.Group>

        <hr/>

        <Row>
            <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                <Link
                to={RouteNames.RECEPT_PREGLED}
                className="btn btn-danger siroko"
                >Odustani</Link>
            </Col>
            <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko">
                    Dodaj recept
                </Button>
            </Col>
        </Row>


    </Form>




   
    </>
    )
}