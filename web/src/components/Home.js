import React from 'react'
import icon  from '../assets/student-icon.svg';

const Home = () => {
  return (
      <header className="App-header">
          <div className="App-deco">
            <div className="welcome">
              <h1> Welcome to DUO! </h1>
            </div>
            <img src={icon} className="App-greeting" alt="logo" />
          </div>
          <p>
          If you are enrolled as a student in university, higher vocational education, or secondary vocational education, you can apply for student finance. You must be a Dutch national or have the same rights. This means non-Dutch can also apply for student finance.
          Do you fail to meet the nationality criteria for student finance? Then you may still qualify for a tuition fees loan. 
          </p>
      </header>
  )
}

export default Home