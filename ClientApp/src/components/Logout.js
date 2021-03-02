import React, { Component } from 'react';
import { Redirect } from 'react-router';

export class Logout extends Component {

    constructor(props) {
        super(props);
        this.state = {
          needRedirect: false
        };
      }

    render(){

        if (this.state.needRedirect === true || sessionStorage.getItem('user') === null){
            return (<Redirect to="/Login" push={true}/>);
        }
        
        return (<button onClick={this.logout}>Logout</button>);
    }


    logout = () => {
        sessionStorage.clear(`user`);
        this.setState({needRedirect: true});
        console.log(sessionStorage.getItem(`user`));
    }

}