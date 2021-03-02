import React, { Component } from 'react';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
import {JobFinderHelper} from '../Infrastructure/JobFinderHelper';
import {UserSessionHelper} from '../Infrastructure/UserSessionHelper';
//import {MessageConstant} from '../Constants/MessageConstant';


export class Job extends Component{
    
    constructor(props) {
        super(props);

        this.state = {
            candidates: [],
            idJob: this.props.location.state.idJob,
            nmJob: this.props.location.state.nmJob,
            messageValidation: ""
        };

        this.btnAcceptCandidate_Click = this.btnAcceptCandidate_Click.bind(this);
        this.btnRejectCandidate_Click = this.btnRejectCandidate_Click.bind(this);
    }

    renderCandidates(){
        return (
            <table className="table table-hover">
                <thead>
                    <tr>
                        <th>id</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Telephone</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.candidates.map(candidate =>
                        <tr key={candidate.idCandidate}>
                            <td>{candidate.idCandidate}</td>
                            <td>{candidate.customer.nmUser}</td>
                            <td>{candidate.customer.deEmail}</td>
                            <td>{candidate.customer.nuTelephone}</td>
                            <td>
                                {this.renderButtons(candidate.idCandidate, candidate.wasAccept, candidate.wasReject)}
                            </td>
                            {/* <td><button className="btn btn-primary" onClick={(e) => this.btnViewJob_click(job.idJob, job.deTitle)}>
                                <i className="fa fa-eye"></i>
                                </button>
                            </td> */}
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    renderButtons(pIdCandidate, pWasAccept, pWasReject){
        if (pWasAccept === false && pWasReject === false){
            return (<div>
                <button className="btn btn-success" onClick={(e) => this.btnAcceptCandidate_Click(pIdCandidate)}><i className="fa fa-thumbs-up"></i></button>
                <button className="btn btn-danger"  onClick={(e) => this.btnRejectCandidate_Click(pIdCandidate)}><i className="fa fa-thumbs-down"></i></button>
            </div>)
        }
        else if (pWasAccept === true && pWasReject === false){
            return(<div>
                <span className="badge badge-success">Accepted</span>
            </div>)
        }else{
            return(<div>
                <span className="badge badge-danger">Rejected</span>
            </div>)
        }
    }


    render(){

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : 
            <div>{this.renderCandidates()}</div>;


            return <div className="container">
                <div className="row">
                    <div className="col-md-12">
                        <div className="box">
                            <h1>{this.state.nmJob}</h1>
                            <p className="text-muted"></p>
                            <label className="text-danger">{this.state.messageValidation}</label>
                            {contents}
                        </div>
                    </div>
                </div>
            </div>;
    }

    /**
     * Called after render
     */
    componentDidMount() {
        this.getCandidate();
    }

    async getCandidate(){
        const apiResponse = await JobFinderWS.getCandidates(UserSessionHelper.getToken(), this.state.idJob);
        this.setState({ candidates: apiResponse.data, loading: false });
    }

    // Events
    async btnAcceptCandidate_Click(pIdCandidate){
        const apiResponse = await JobFinderWS.acceptCandidate(UserSessionHelper.getToken(), pIdCandidate, this.state.idJob);

         // show errors
         let message = JobFinderHelper.prepareReturnMessagesResponse(apiResponse);
         if (message !== ""){
             this.setState({ messageValidation: message });
         }

         // clean the states
        if (apiResponse.status === 200) {
            this.setState({ messageValidation: "" });
            this.setState({ loading: true });
            this.getCandidate();
        }
    }

    async btnRejectCandidate_Click(pIdCandidate){
        const apiResponse = await JobFinderWS.rejectCandidate(UserSessionHelper.getToken(), pIdCandidate, this.state.idJob);

         // show errors
         let message = JobFinderHelper.prepareReturnMessagesResponse(apiResponse);
         if (message !== ""){
             this.setState({ messageValidation: message });
         }

         // clean the states
        if (apiResponse.status === 200) {
            this.setState({ messageValidation: "" });
            this.setState({ loading: true });
            this.getCandidate();
        }
    }

}