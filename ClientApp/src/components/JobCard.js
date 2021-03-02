import React, { Component } from 'react';
import { JobFinderWS } from '../Infrastructure/JobFinderWS';
import { JobFinderHelper } from '../Infrastructure/JobFinderHelper';
import { UserSessionHelper } from '../Infrastructure/UserSessionHelper';
import { MessageConstant } from '../Constants/MessageConstant';

export class JobCard extends Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            messageValidationOk: "",
            messageValidationError: "",
            idJob: props.idJob
        };

        // This binding is necessary to make `this` work in the callback
        this.btnCandidate_click = this.btnCandidate_click.bind(this);
    }

    render(){

        return (
            <div className="card">
                <div className="card-body">
                    <h4 className="card-title">{this.props.deTitle}</h4>
                    <p className="card-text">{this.props.dtRegister.toString().slice(0,10)}</p>
                    <p className="card-text"><i className="fas fa-building"></i> {this.props.nmCompany}</p>
                    <p className="card-text"><i className="fas fa-laptop"></i> {this.props.nmRole}</p>
                    <p className="card-text"><i className="fas fa-file"></i> {this.props.deDescription}</p>
                    <p className="card-text"><i className="fa fa-usd" aria-hidden="true"></i> {this.props.vlSalaryMin} - {this.props.vlSalaryMax}</p>
                    <button type="button" className="btn btn-primary" onClick={this.btnCandidate_click}>Candidate</button>
                    <p className="card-text" className="text-success">{this.state.messageValidationOk}</p>
                    <p className="card-text" className="text-danger">{this.state.messageValidationError}</p>
                </div>
            </div>
        );
    }


    async btnCandidate_click(){
        this.setState({ messageValidationError: ""});
        this.setState({ messageValidationOk: ""});


        let apiResponse = await JobFinderWS.registerCandidate(UserSessionHelper.getToken(), this.state.idJob);

        if (apiResponse !== null && apiResponse !== undefined)
        {
            if (apiResponse.status === 200){
                this.setState({ messageValidationOk: MessageConstant.getRegisterOkMessage()});
            }
            else{
                this.setState({ messageValidationError: JobFinderHelper.prepareReturnMessagesResponse(apiResponse)});
            }
        }
        else{
            this.setState({ messageValidationError: MessageConstant.getFailAPIResponseMessage()});
        }
    }

}