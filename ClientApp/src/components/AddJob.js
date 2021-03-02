import React, { Component } from 'react';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
import {JobFinderHelper} from '../Infrastructure/JobFinderHelper';
import {UserSessionHelper} from '../Infrastructure/UserSessionHelper';
import {MessageConstant} from '../Constants/MessageConstant';


export class AddJob extends Component{

    constructor(props) {
        super(props);
        this.state = {
            roles: [],
            loading: true,
            messageValidation: ``,
            messageOK: ``,
            deTitle: "",
            deDescription: "",
            vlSalaryMin: "",
            vlSalaryMax: "",
            idRole: "0",
            populateJobs: props.populateJobs
        };

        // This binding is necessary to make `this` work in the callback
        this.idRole_Change = this.idRole_Change.bind(this);
        this.deTitle_change = this.deTitle_change.bind(this);
        this.deDescription_change = this.deDescription_change.bind(this);
        this.vlSalaryMin_change = this.vlSalaryMin_change.bind(this);
        this.vlSalaryMax_change = this.vlSalaryMax_change.bind(this);

        this.btnRegisterJob_click = this.btnRegisterJob_click.bind(this);
    }


    static renderSelectRole(roles, value, onChangeEvent) {
        return (
            <select name="selectRole" id="selectRole" className="form-control col-md-12" 
                onChange={onChangeEvent} value={value}>
                <option value="0">...</option>
                {roles.map(role =>
                    <option value={role.idRole} key={role.idRole}>{role.nmRole}</option>
                )}
        </select>
        );
    }


    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : 
            <div>
                <form className="form-inline">
                    
                    <div className="form-group col-md-6">
                        <label htmlFor="inputDeTitle" className="mr-sm-2">Title *</label>
                        <input type="text" id="inputDeTitle" className="form-control col-md-12" onChange={this.deTitle_change} value={this.state.deTitle}/>
                    </div>

                    <div className="form-group col-md-6">
                        <label htmlFor="selectRole" className="mr-sm-2">Role *</label>
                        { AddJob.renderSelectRole(this.state.roles.data, this.state.idRole, this.idRole_Change) }
                    </div>

                    <div className="form-group col-md-6">
                        <label htmlFor="inputVlSalaryMin" className="mr-sm-2">Salary Min</label>
                        <input type="text" id="inputVlSalaryMin" className="form-control col-md-12" onChange={this.vlSalaryMin_change} value={this.state.vlSalaryMin}/>
                    </div>

                    <div className="form-group col-md-6">
                        <label htmlFor="inputVlSalaryMin" className="mr-sm-2">Salary Max</label>
                        <input type="text" id="inputVlSalaryMax" className="form-control col-md-12" onChange={this.vlSalaryMax_change} value={this.state.vlSalaryMax}/>
                    </div>

                    <div className="form-group col-md-12">
                        <label htmlFor="inputDeDescription" className="mr-sm-2">Description *</label>
                        <textarea  type="text" rows="5" id="inputDeDescription" className="col-md-12 form-control" onChange={this.deDescription_change} value={this.state.deDescription}></textarea>
                    </div>

                    <div className="form-group col-md-12">
                        <label className="text-danger">{this.state.messageValidation}</label>
                        <label className="text-success">{this.state.messageOK}</label>
                    </div>

                    <div className="form-group my-2">
                        <button type="button" className="btn btn-success" id="btnRegisterJob" onClick={this.btnRegisterJob_click}>Add</button>
                    </div>
                </form>
            </div>;

        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-12">
                        <div className="box">
                            <h1>Add Job</h1>
                            <p className="text-muted"></p>
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

    // ---------- EVENTS
    idRole_Change(event) {
        this.setState({ idRole: event.target.value });
    }
    deTitle_change(event) {
        this.setState({ deTitle: event.target.value });
    }
    deDescription_change(event) {
        this.setState({ deDescription: event.target.value });
    }
    vlSalaryMin_change(event) {
        this.setState({ vlSalaryMin: event.target.value });
    }
    vlSalaryMax_change(event) {
        this.setState({ vlSalaryMax: event.target.value });
    }

    async btnRegisterJob_click(){
        this.setState({ messageOK: ""});
        this.setState({ messageValidation: ""});

        let apiResponse = await JobFinderWS.registerJob(UserSessionHelper.getToken(), this.state.deTitle, this.state.deDescription, 
            parseInt(this.state.idRole), parseFloat(this.state.vlSalaryMin), parseFloat(this.state.vlSalaryMax));

        if (apiResponse !== null && apiResponse !== undefined){
            if (apiResponse.status === 200){
                this.setState({ messageOK: MessageConstant.getRegisterJobOKMessage()});

                this.setState({ deTitle: ``});
                this.setState({ deDescription: ``});
                this.setState({ vlSalaryMax: ``});
                this.setState({ vlSalaryMin: ``});
                this.setState({ idRole: `0`});

                // reaload jobs from JobsByCompany Component
                this.state.populateJobs();
            }
            else{
                this.setState({ messageValidation: JobFinderHelper.prepareReturnMessagesResponse(apiResponse) });
            }
        }
        else{
            this.setState({ messageValidation: MessageConstant.getFailAPIResponseMessage()});
        }

    }

    // ---------- EVENTS - END

    /**
     * Populate select role with api value
     */
    async populateSelectRole() {
        const apiResponse = await JobFinderWS.getRoles();
        this.setState({ roles: apiResponse, loading: false });
    }

} 