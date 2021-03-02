import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
//import {JobFinderHelper} from '../Infrastructure/JobFinderHelper';
import {UserSessionHelper} from '../Infrastructure/UserSessionHelper';
import {AddJob} from './AddJob';
import {Job} from './Job';


export class JobsByCompany extends Component {

    constructor(props) {
        super(props);
        this.state = {
            needRedirect: false,
            jobs: [],
            loading: true,
            idRole: 0,
            idJobRender: 0,
            deTitleRender: ""
        };
    }

    renderJobs(jobs) {
        return (
            <table className="table table-hover">
                <thead>
                    <tr>
                        {/* <th>id</th> */}
                        <th>Title</th>
                        <th>Description</th>
                        <th>Role</th>
                        <th>Salary</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {jobs.map(job =>
                        <tr key={job.idJob}>
                            {/* <td>{job.idJob}</td> */}
                            <td>{job.deTitle}</td>
                            <td>{job.deDescription}</td>
                            <td>{job.role.nmRole}</td>
                            <td>{job.vlSalaryMin} / {job.vlSalaryMax}</td>
                            <td><button className="btn btn-primary" onClick={(e) => this.btnViewJob_click(job.idJob, job.deTitle)}>
                                <i className="fa fa-eye"></i>
                                </button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : 
            <div>
                <div className="jobs">
                    {this.renderJobs(this.state.jobs)}
                </div>

            </div>;

        if (this.state.needRedirect === true){
            return (<Redirect to={{
                pathname: "/Job",
                state: { idJob: this.state.idJobRender, nmJob: this.state.deTitleRender}
            }}/>);
            // return (<Job idJob={this.state.idJobRenderidJob} nmJob={this.state.deTitleRender}></Job>);
        }

        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-12">
                        <AddJob populateJobs={this.populateJobs.bind(this)}></AddJob>
                        <div className="box">
                            <h1>My Jobs</h1>
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
        this.populateJobs();
    }

    // ---------- EVENTS

    btnViewJob_click(pIdJob, pDeTitle){
        this.setState({needRedirect: true});
        this.setState({idJobRender: parseInt(pIdJob)});
        this.setState({deTitleRender: pDeTitle});
    }

    // ---------- EVENTS END

    /**
     * Populate jobs state with api value
     */
    async populateJobs() {
        const apiResponse = await JobFinderWS.getJobsByCompany(UserSessionHelper.getToken());
        
        if (apiResponse !== null && apiResponse !== undefined &&
             apiResponse.data !== null && apiResponse.data !== undefined){
            this.setState({ jobs: apiResponse.data, loading: false });
        }
    }
}