import './App.css';
import logo from './assets/logo-duo.png';

// Import bootstrap components
import { Navbar } from 'react-bootstrap';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';

// Our separately created components
import Home from './components/Home';
import About from './components/About';
import StudProd from './components/StudentProducts';
import Debts from './components/Debts';
import ProductApplication from './components/ProductApplication';

// To use routing functionalities
import { Route, Routes } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <div>
        <Navbar className="navbar-duo" style={{ background:"white"}}>
            <Navbar.Brand href="/" style={{color: "purple"}}> 
              <img
                alt=""
                src={logo}
                width="275"
                height="140"
              />{' '}
            </Navbar.Brand>
            <Navbar.Brand className="menu-item" href="about"> About </Navbar.Brand>
            <Navbar.Brand className="menu-item" href="products"> My Student Products </Navbar.Brand>
            <Navbar.Brand className="menu-item" href="debts"> My Debts </Navbar.Brand>
            <Navbar.Brand className="menu-item" href="about"> My Messages </Navbar.Brand>
            <Navbar.Brand className="menu-item" href="about"> My Settings </Navbar.Brand>
        </Navbar>
      </div>
      <Routes>
         <Route path='/' element={<Home/>} />
         <Route path='/about' element={<About/>} />
         <Route path='/products' element={<StudProd />} />
         <Route path='/debts' element={<Debts />} />
         <Route path='/apply' element={<ProductApplication />} />
         <Route path='*' element={<h1>Not Found</h1>} />
      </Routes>
    </div>
  );
}

export default App;
