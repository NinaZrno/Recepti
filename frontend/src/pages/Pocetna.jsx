import React from 'react';
import slikazanaslovnu from '../assets/slikazanaslovnu.jpg';
import '../App.css';





export default function Pocetna(){
    const backgroundStyle = {
        backgroundImage: `url(${slikazanaslovnu})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        height: '80vh',
        width: '84vw'
    }

   
    return(
      
        <>
        <div style={backgroundStyle}>
         Dobrodošli na moju aplikaciju
        </div>

        </>
       
    )
}