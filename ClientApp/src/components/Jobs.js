import React, { Component } from 'react';
//import { Redirect } from 'react-router';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
//import {JobFinderHelper} from '../Infrastructure/JobFinderHelper';
import {UserSessionHelper} from '../Infrastructure/UserSessionHelper';
import {JobCard} from './JobCard';
import {JobsByCompany} from './JobsByCompany';


export class Jobs extends Component {

    constructor(props) {
        super(props);
        this.state = {
            roles: [],
            jobs: [],
            loading: true,
            idRole: 0
        };

        // This binding is necessary to make `this` work in the callback
        this.idRole_Change = this.idRole_Change.bind(this);
        this.btnSearchJobs_click = this.btnSearchJobs_click.bind(this);
    }

    static renderSelectRole(roles, onChangeEvent) {
        return (
            <select name="selectRole" id="selectRole" className="form-control mb-2 mr-sm-2" onChange={onChangeEvent}>
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

                    <label htmlFor="selectRole" className="mr-sm-2">Role *:</label>
                    { Jobs.renderSelectRole(this.state.roles.data, this.idRole_Change) }

                    <button type="button" className="btn btn-primary mb-2" id="btnSearchJobs" onClick={this.btnSearchJobs_click}>Find Jobs</button>
                </form>

                <div className="jobs">
                    {this.state.jobs}
                </div>

            </div>;

        const user = UserSessionHelper.getUser();
        if (user == null){
            return (<div></div>);
        }
        else if (UserSessionHelper.getIsCompany()){
            return (<JobsByCompany></JobsByCompany>);
        }
        else{
            return (
                <div className="container">
                    <div className="row">
                        <div className="col-md-12">
                            <div className="box">
                                <h1>Jobs Search</h1>
                                <p className="text-muted"></p>
                                {contents}
                            </div>
                        </div>
                    </div>
                </div>
            );
        }
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

    async btnSearchJobs_click(){
        let apiResponse = await JobFinderWS.getJobsByRole(UserSessionHelper.getToken(), this.state.idRole); 
        
        if (apiResponse !== null && apiResponse.data !== null){
            this.addJobsCard(apiResponse.data);
        }
    }

    // ---------- EVENTS END

    /**
     * Populate select role with api value
     */
    async populateSelectRole() {
        const apiResponse = await JobFinderWS.getRoles();
        this.setState({ roles: apiResponse, loading: false });
    }

    
    addJobsCard(pJobs){

        this.setState( { jobs:[]});

        let jobsComponents = [];

        for (let i in pJobs){

            jobsComponents = jobsComponents.concat(
                <JobCard key={pJobs[i].idJob}
                    idJob={pJobs[i].idJob}
                    deTitle={pJobs[i].deTitle} 
                    dtRegister={pJobs[i].dtRegister} 
                    nmCompany={pJobs[i].company.nmUser}
                    nmRole={pJobs[i].role.nmRole} 
                    deDescription={pJobs[i].deDescription} 
                    vlSalaryMin={pJobs[i].vlSalaryMin}
                    vlSalaryMax={pJobs[i].vlSalaryMax}/>
            );
        }

        this.setState( { jobs: jobsComponents});
    }

}