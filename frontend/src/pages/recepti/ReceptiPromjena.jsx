import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import moment from "moment";
import ReceptService from "../../services/ReceptService";
import { useEffect, useState } from "react";


export default function ReceptiPromjena(){

    const navigate = useNavigate();
    const [recept,setRecept] = useState({});
    const [vaucer,setVaucer] = useState(false)
    const routeParams = useParams();

    async function dohvatiRecept(){
        const odgovor = await ReceptService.getBySifra(routeParams.sifra)

        if(odgovor.izvodiSeOd!=null){
            odgovor.izvodiSeOd = moment.utc(odgovor.izvodiSeOd).format('yyyy-MM-DD')
        }
        
        setRecept(odgovor)
        setVaucer(odgovor.vaucer)
    }

    useEffect(()=>{
        dohvatiRecept();
    },[])

    async function promjena(recept){
        const odgovor = await ReceptService.promjena(routeParams.sifra,recept);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.RECEPT_PREGLED)
    }

    function odradiSubmit(e){ // e je event
        e.preventDefault(); // nemoj odraditi zahtjev na server pa standardnom načinu

        let podaci = new FormData(e.target);

        promjena(
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
    Promjena recepta
    <Form onSubmit={odradiSubmit}>

        <Form.Group controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" required 
            defaultValue={recept.naziv}/>
        </Form.Group>

        <Form.Group controlId="vrsta">
            <Form.Label>Vrsta</Form.Label>
            <Form.Control type="text" name="vrsta" step={0.01} 
            defaultValue={recept.vrsta}/>
        </Form.Group>

        <Form.Group controlId="uputa">
            <Form.Label>Uputa</Form.Label>
            <Form.Control type="text" name="uputa" 
            defaultValue={recept.uputa}/>
        </Form.Group>


        <Form.Group controlId="trajanje">
        <Form.Label>Trajanje</Form.Label>
            <Form.Control type="number" name="trajanje" 
            defaultValue={recept.trajanje}/>
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
                    Promjeni recept
                </Button>
            </Col>
        </Row>


    </Form>




   
    </>
    )
}