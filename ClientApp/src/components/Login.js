import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
import {JobFinderHelper} from '../Infrastructure/JobFinderHelper';
import {UserSessionHelper} from '../Infrastructure/UserSessionHelper';

export class Login extends Component {

    constructor(props) {
        super(props);
        this.state = {
            needRedirect: false,
            messageValidation: ``,
            deEmail: ``,
            dePassword: ``,
            userType: `0`
        };

        // This binding is necessary to make `this` work in the callback
        this.btnLogin_click = this.btnLogin_click.bind(this);
        this.deEmail_Change = this.deEmail_Change.bind(this);
        this.dePassword_Change = this.dePassword_Change.bind(this);
        this.userType_Change = this.userType_Change.bind(this);
    }

    render() {
        if (sessionStorage.getItem(`user`) !== null ||
        this.state.needRedirect === true){
            return (
                <Redirect to="/Home" push={true}/>
            );
        }

        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-6">
                    <div className="box box-login">
                            <h1>Job Finder</h1>
                            <p className="text-muted">Please enter your login and password!</p>

                            <div className="form-group">
                                <label htmlFor="inputEmail">E-mail</label>
                                <input type="email" id="inputEmail" className="form-control" onChange={this.deEmail_Change}/>
                            </div>

                            <div className="form-group">
                                <label htmlFor="inputPassword">Password</label>
                                <input type="password" id="inputPassword" className="form-control" onChange={this.dePassword_Change}/>
                            </div>

                            <div className="form-group">
                                <label htmlFor="selectUserType">You are</label>
                                <select id="selectUserType" className="form-control" onChange={this.userType_Change}>
                                    <option value='0'>...</option>
                                    <option value='2'>a Recruter/Company</option>
                                    <option value='1'>a Worker</option>
                                </select>
                            </div>

                            <div className="form-group">
                                <label id="lblValidationMessage" className="text-danger">{this.state.messageValidation}</label>
                            </div>

                            <button className="btn btn-primary" onClick={this.btnLogin_click}>Login</button>
                        </div>
                    </div>
                </div>
            </div>
            );
    }



    // -------------- EVENTS --------------

    async btnLogin_click(){
        // clean message
        this.setState({ messageValidation : ""})

        // consume api
        const apiResponse = await JobFinderWS.tryLogin(this.state.deEmail, this.state.dePassword, parseInt(this.state.userType));

        console.log(apiResponse);

        // prepare messages if has any message
        let message = JobFinderHelper.prepareReturnMessagesResponse(apiResponse);

        if (message === ''){
            UserSessionHelper.setUser(apiResponse.data);
            this.setState({ needRedirect: true});
        }
        else{
            this.setState({ messageValidation: message });
        }
    }

    deEmail_Change(event) {
        this.setState({ deEmail: event.target.value });
    }
    dePassword_Change(event) {
        this.setState({ dePassword: event.target.value });
    }
    userType_Change(event) {
        this.setState({ userType: event.target.value });
    }


    // -------------- END EVENTS --------------

}
