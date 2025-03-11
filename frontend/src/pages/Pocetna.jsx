import React from 'react';
import slikazanaslovnu from '...assets/slikazanaslovnu.jpeg';
import '../App.css';





export default function Pocetna(){
    const backgroundStyle = {
        backgroundImage: `url(${slikazanaslovnu})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        height: '80vh',
        width: '84vw'

   
    return(
        <>
        Dobrodošli na moju aplikaciju
        </>
    )
}