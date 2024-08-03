import React from 'react'
import Card from 'react-bootstrap/Card'
import { Link } from 'react-router-dom'
import ListGroup from 'react-bootstrap/ListGroup'
import Button from 'react-bootstrap/Button'
import travel from '../assets/travel.svg'
import grant from '../assets/grant.svg'
import debt from '../assets/debt.svg'

// Depending on the type, return the appropriate description and image
let imgSrc = '';
let productDescription = '';
let title = '';
let currDebt = '';
let payoutText = '';
const travelDescription = 'With the student travel product you can travel for free or at a reduced rate by train, tram, bus and metro throughout the Netherlands.';
const grantDescription = 'The basic and/or supplementary grant becomes a gift if you graduate within 10 years. The amount of payouts is determined monthly.';
const loanDescription = 'The interest bearing loan and tuition fees loan will need to paid back after finishing your studies. The interest rate is determined anew each year.';

const ProductCard = ({ type }) => {

  imgSrc = type === 'travel' ? travel : type === 'grant' ? grant : debt
  productDescription = type === 'travel' ? travelDescription : type === 'grant' ? grantDescription : loanDescription
  title = type === 'travel' ? 'Travel Product' : type === 'grant' ? 'Student Grant' : 'Student Loan'
  payoutText = type === 'travel' ? 'Subscription type: weekend' : type === 'grant' ? 'Monthly payout: €357.90' : 'Monthly payout: €250.00'
  currDebt = type === 'travel' ? 'Current debt: €7865.89' : type === 'grant' ? 'Current debt: €0.00' : 'Current debt: €7500.00'

   return (
    <Card className="product-card">
      <Card.Img variant="top" src={imgSrc} style={{ width: '5em' }}/>
      <Card.Body>
        <Card.Title className="card-title">{title}</Card.Title>
        <Card.Text className="card-description">
          {productDescription}
        </Card.Text>
      </Card.Body>
      {
        (type == 'travel' | type == 'loan') ?
        <div>
          <ListGroup className="list-group-flush">
            <ListGroup.Item>{payoutText}</ListGroup.Item>
            <ListGroup.Item>{currDebt}</ListGroup.Item>
            <ListGroup.Item>Start date: 01-09-2020</ListGroup.Item>
            <ListGroup.Item>End date: 01-09-2025</ListGroup.Item>
          </ListGroup>
          <Card.Body>
            <Button variant="dark">Edit details</Button>
          </Card.Body>
        </div>
          : <Card.Body>
          <Link to="/apply">
            <Button variant="success">Apply here</Button>
          </Link>
        </Card.Body>
      }
    </Card>
   )
 }

 export default ProductCard