import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Login } from './components/Login';
import { Logout } from './components/Logout';
import { Join } from './components/Join';
import { Job } from './components/Job';
import { Jobs } from './components/Jobs';
import { Profile } from './components/Profile';


import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = {
      user: sessionStorage.getItem("user")
    };
  }


  render () {

    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/logout' component={Logout} />
        <Route path='/login' component={Login} />
        <Route path='/Join' component={Join} />
        <Route path='/Job' render={(props) => <Job {...props}></Job>} />
        <Route path='/Jobs' component={Jobs}/>
        <Route path='/Profile' component={Profile} />
      </Layout>
    );
  }
}
