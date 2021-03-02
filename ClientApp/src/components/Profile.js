import React, { Component } from 'react';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
import { JobFinderHelper } from '../Infrastructure/JobFinderHelper';
import { MessageConstant } from '../Constants/MessageConstant'
import {UserSessionHelper} from '../Infrastructure/UserSessionHelper';

export class Profile extends Component {

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
            idRole: `0`,
            userType: `0`
        };

        // This binding is necessary to make `this` work in the callback
        this.updateProfile_Click = this.updateProfile_Click.bind(this);
        this.nmUser_Change = this.nmUser_Change.bind(this);
        this.deEmail_Change = this.deEmail_Change.bind(this);
        this.nuPhone_Change = this.nuPhone_Change.bind(this);
        this.idRole_Change = this.idRole_Change.bind(this);
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

    render(){

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <div>

            <h1>Profile</h1>
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

            {
                (UserSessionHelper.getIsCompany() === false)
                ? <div className="form-group" id="divRole">
                <label htmlFor="selectRole">Role *</label>
                    { Profile.renderSelectRole(this.state.roles.data, this.state.idRole, this.idRole_Change) }
                </div>
                : null
            }

            <div className="form-group">
                <label className="text-danger">{this.state.messageValidation}</label>
                <label className="text-success">{this.state.messageOK}</label>
            </div>

            <button className="btn btn-success" id="btnUpdateProfile" onClick={this.updateProfile_Click}>Update</button>
        </div>;

        return contents;
    }

    /**
     * Called after render
     */
    componentDidMount() {
        this.componentDidMountAsync();
    }

    async componentDidMountAsync(){
        await this.populateSelectRole();
        await this.getProfile();
        this.setState({ loading: false });
    }

    /// ------------------- Events -------------------
    /**
     * Update Profile
     */
    async updateProfile_Click(){
        // clean message validation
        this.setState({ messageValidation: "" });
        this.setState({ messageOK: "" });    

        const apiResponse = await JobFinderWS.updateProfile(UserSessionHelper.getToken() ,this.state.nmUser, this.state.deEmail, 
            this.state.nuPhone, parseInt(UserSessionHelper.getUserType()), parseInt(this.state.idRole));        

        // show errors
        let message = JobFinderHelper.prepareReturnMessagesResponse(apiResponse);
        if (message !== ""){
            this.setState({ messageValidation: message });
        }

        // clean the states
        if (apiResponse.status === 200) {
            this.setState({ messageOK: MessageConstant.getUpdateProfileMessage()});
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
    idRole_Change(event) {
        this.setState({ idRole: event.target.value });
    }

    ///------------------- Events - END -------------------

    /**
     * Populate select role with api value
     */
    async populateSelectRole() {
        const apiResponse = await JobFinderWS.getRoles();
        this.setState({ roles: apiResponse });
    }

    /**
     * get Profile data and fill states
     */
    async getProfile(){
        const apiResponse = await JobFinderWS.getProfile(UserSessionHelper.getToken());

        // show errors
        let message = JobFinderHelper.prepareReturnMessagesResponse(apiResponse);
        if (message !== ""){
            this.setState({ messageValidation: message });
        }

        // set fields
        if (apiResponse.status === 200) {

            this.setState({ nmUser: apiResponse.data.nmUser });
            this.setState({ deEmail: apiResponse.data.deEmail });
            this.setState({ nuPhone: apiResponse.data.nuTelephone });

            if (UserSessionHelper.getIsCompany() === false){
                this.setState({ idRole: apiResponse.data.idRole });
            }
        }
    }
}