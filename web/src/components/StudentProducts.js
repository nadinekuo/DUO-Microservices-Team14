import React from 'react'
import ProductCard from './ProductCard'

 const StudentProducts = () => {
   return (
    <div className='cards-container'>
      <ProductCard type="travel"/>
      <ProductCard type="grant"/>
      <ProductCard type="loan"/>
    </div> 
   )
 }

 export default StudentProducts