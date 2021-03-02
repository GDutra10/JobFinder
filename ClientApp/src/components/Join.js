import React, { Component } from 'react';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
import { JobFinderHelper } from '../Infrastructure/JobFinderHelper';
import { MessageConstant } from '../Constants/MessageConstant'

export class Join extends Component {

    constructor(props) {
        super(props);
        this.state = {
            roles: [],
            loading: true,
            messageValidation: ``,
            messageOK: ``,
            nmUser: ``,
            deEmail: ``,
            nuPhone: ``,
            dePassword: ``,
            dePasswordConfirm: ``,
            idRole: `0`,
            userType: `0`
        };

        // This binding is necessary to make `this` work in the callback
        this.registerCustomer_Click = this.registerCustomer_Click.bind(this);
        this.nmUser_Change = this.nmUser_Change.bind(this);
        this.deEmail_Change = this.deEmail_Change.bind(this);
        this.nuPhone_Change = this.nuPhone_Change.bind(this);
        this.dePassword_Change = this.dePassword_Change.bind(this);
        this.dePasswordConfirm_Change = this.dePasswordConfirm_Change.bind(this);
        this.idRole_Change = this.idRole_Change.bind(this);
        this.userType_Change = this.userType_Change.bind(this);
    }

    /**
     * render Role Select
     * @param  {[Object]} roles List of Roles Object
     * @param  {[Event]} onChangeEvent Must be idRole_Change
     */
    static renderSelectRole(roles, value, onChangeEvent) {
        return (
            <select name="role" id="selectRole" className="form-control" onChange={onChangeEvent} value={value}>
                <option value="0">...</option>
                {roles.map(role =>
                    <option value={role.idRole} key={role.idRole}>{role.nmRole}</option>
                )}
        </select>
        );
    }

    /**
     * render component
     */
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <div>
                <div className="form-group">
                    <label htmlFor="inputNameUser">Name *</label>
                    <input type="text" id="inputNameUser" className="form-control" onChange={this.nmUser_Change} value={this.state.nmUser}/>
                </div>

                <div className="form-group">
                    <label htmlFor="inputEmail">E-mail *</label>
                    <input type="email" id="inputEmail" className="form-control" onChange={this.deEmail_Change} value={this.state.deEmail}/>
                </div>

                <div className="form-group">
                    <label htmlFor="inputNuPhone">Phone</label>
                    <input type="text" id="inputNuPhone" className="form-control" onChange={this.nuPhone_Change} value={this.state.nuPhone}/>
                </div>

                <div className="form-group">
                    <label htmlFor="selectRole">Role *</label>
                    { Join.renderSelectRole(this.state.roles.data, this.state.nmUser, this.idRole_Change) }
                </div>

                <div className="form-group">
                    <label htmlFor="inputPassword">Password *</label>
                    <input type="password" id="inputPassword" className="form-control" onChange={this.dePassword_Change} value={this.state.dePassword}/>
                </div>

                <div className="form-group">
                    <label htmlFor="inputPasswordConfirm">Password Confirm *</label>
                    <input type="password" id="inputPasswordConfirm" className="form-control" onChange={this.dePasswordConfirm_Change} value={this.state.dePasswordConfirm}/>
                </div>

                <div className="form-group">
                    <label htmlFor="selectUserType">You are *</label>
                    <select id="selectUserType" className="form-control" onChange={this.userType_Change} value={this.state.userType}>
                        <option value='0'>...</option>
                        <option value='2'>a Recruter/Company</option>
                        <option value='1'>a Worker</option>
                    </select>
                </div>

                <div className="form-group">
                    <label className="text-danger">{this.state.messageValidation}</label>
                    <label className="text-success">{this.state.messageOK}</label>
                </div>

                <button className="btn btn-success" id="btnRegisterCustomer" onClick={this.registerCustomer_Click}>Create account</button>
            </div>;



        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-6">
                        <div className="box box-join">
                            <h1>Create your account</h1>
                            <p className="text-muted">Please provide your personal data!</p>
                            {contents}
                        </div>
                    </div>
                </div>
            </div>
        );
    }

    /**
     * Called after render
     */
    componentDidMount() {
        this.populateSelectRole();
    }

    /// ------------------- Events -------------------

    /**
     * Register new Customer, call API
     */
    async registerCustomer_Click() {
        // clean message validation
        this.setState({ messageValidation: "" });
        this.setState({ messageOK: "" });        

        const apiResponse = await JobFinderWS.registerUser(this.state.nmUser, this.state.deEmail, this.state.dePassword,
            this.state.dePasswordConfirm, this.state.nuPhone, parseInt(this.state.userType), parseInt(this.state.idRole));

        // show errors
        let message = JobFinderHelper.prepareReturnMessagesResponse(apiResponse);
        if (message !== ""){
            this.setState({ messageValidation: message });
        }

        // clean the states
        if (apiResponse.status === 200) {
            this.setState({ messageOK: MessageConstant.getRegisterOkMessage()});

            this.setState({ nmUser: `` });
            this.setState({ deEmail: `` });
            this.setState({ nuPhone: `` });
            this.setState({ dePassword: `` });
            this.setState({ dePasswordConfirm: `` });
            this.setState({ idRole: `0` });
        }
    }
    nmUser_Change(event) {
        this.setState({ nmUser: event.target.value });
    }
    deEmail_Change(event) {
        this.setState({ deEmail: event.target.value });
    }
    nuPhone_Change(event) {
        this.setState({ nuPhone: event.target.value });
    }
    dePassword_Change(event) {
        this.setState({ dePassword: event.target.value });
    }
    dePasswordConfirm_Change(event) {
        this.setState({ dePasswordConfirm: event.target.value });
    }
    idRole_Change(event) {
        this.setState({ idRole: event.target.value });
    }
    userType_Change(event) {
        this.setState({ userType: event.target.value });
    }

    ///------------------- Events - END -------------------

    /**
     * Populate select role with api value
     */
    async populateSelectRole() {
        const apiResponse = await JobFinderWS.getRoles();
        this.setState({ roles: apiResponse, loading: false });
    }
}