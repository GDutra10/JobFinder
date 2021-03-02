import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import {UserSessionHelper} from '../Infrastructure/UserSessionHelper';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      user: UserSessionHelper.getUser()
    };

    setInterval(() => {
      this.setState({user: UserSessionHelper.getUser()});
    }, 1000);
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
  let content = this.showMenusIfLogged();

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">JobFinder</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              {content}
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }


  showMenusIfLogged() {

    if (this.state.user !== null){
      return (<ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Profile">Profile</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Jobs">Jobs</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Logout">Logout</NavLink>
                </NavItem>
      </ul>);
    }else{
      return (<ul className="navbar-nav flex-grow">
        <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Login">Login</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Join">Sign up</NavLink>
                </NavItem>
      </ul>);
    }
  }


}