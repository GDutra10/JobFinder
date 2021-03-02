export class JobFinderWS{


    /** 
     * Call API to try login
     * @param  {[string]} pDeEmail Email
     * @param  {[string]} pDePassword Password
     * @param  {[int]} pUserType User Type
     * @return  {[Object]} apiResponse response data Object from API
    */
    static async tryLogin(pDeEmail, pDePassword, pUserType){
        
        const response = await fetch('api/security', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "DeEmail": pDeEmail,
                "DePassword": pDePassword,
                "UserType" : pUserType
            })
        });

        const apiResponse = await response.json();
        //console.log(apiResponse);

        return apiResponse;
    }


    /**
     * Consume API try register a new Customer
     * @param  {[string]} pNmUser User Name
     * @param  {[string]} pDeEmail Email
     * @param  {[string]} pDePassword Password
     * @param  {[string]} pDePasswordConfirm Password Confirm
     * @param  {[string]} pNuPhone Nu Phone
     * @param  {[int]} pUserType User Type
     * @param  {[int]} pIdRole Id Role
     * @return  {[Object]} apiResponse response data Object from API
     */
    static async registerUser(pNmuser, pDeEmail, pDePassword, pDePasswordConfirm, pNuPhone, pUserType, pIdRole){
        
        const response = await fetch('api/user', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "NmUser": pNmuser,
                "DeEmail": pDeEmail,
                "DePassword": pDePassword,
                "DePasswordConfirm": pDePasswordConfirm,
                "NuTelephone": pNuPhone,
                "UserType": pUserType,
                "IdRole": pIdRole
            })
        });

        const apiResponse = await response.json();
        //console.log(apiResponse);

        return apiResponse;
    }

    /**
     * Consume API Register as Candidate to a Job
     * @param  {[string]} pToken Token
     * @param  {[int]} pIdJob IdJob
     * @return  {[Object]} apiResponse response data Object from API
     */
    static async registerCandidate(pToken, pIdJob){
        const response = await fetch(`api/Candidate/${pIdJob}`, {
            method: 'POST',
            headers: {
                // 'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            }
        });

        const apiResponse = await response.json();

        // console.log(apiResponse);

        return apiResponse;
    }

    /**
     * Consume API Register a new Job
     * @param  {[string]} pToken Token
     * @param  {[string]} pDeTitle Title
     * @param  {[string]} pDeDescription Description
     * @param  {[int]} pIdRole Id Role
     * @param  {[float]} pVlSalaryMin Salary Min
     * @param  {[float]} pVlSalaryMax Salary Max
     * @return  {[Object]} apiResponse response data Object from API
     */
    static async registerJob(pToken, pDeTitle, pDeDescription, pIdRole, pVlSalaryMin, pVlSalaryMax){
        const response = await fetch(`api/Job/`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            },
            body: JSON.stringify({
                "DeTitle": pDeTitle,
                "DeDescription": pDeDescription,
                "IdRole": pIdRole,
                "VlSalaryMin": pVlSalaryMin,
                "VlSalaryMax": pVlSalaryMax
            })
        });

        const apiResponse = await response.json();

        // console.log(apiResponse);

        return apiResponse;
    }

    /**
     * Consume API Get Jobs by Roles
     * @param  {[string]} pToken Token
     * @param  {[int]} pIdRole Role
     * @return  {[Object]} apiResponse response data Object from API
     */
    static async getJobsByRole(pToken, pIdRole){
        const response = await fetch(`api/Job/GetByRole/${pIdRole}`, {
            method: 'GET',
            headers: {
                // 'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            }
        });

        // console.log(response);

        if (response.status === 200){
            const apiResponse = await response.json();
        
            // if (apiResponse !== null){
            //     console.log(apiResponse);
            // }

            return apiResponse;
        }

        return null;
    }

    /**
     * Consume API Get Jobs by Roles
     * @param  {[string]} pToken Token
     * @return  {[Object]} apiResponse response data Object from API
     */
    static async getJobsByCompany(pToken){
        const response = await fetch(`api/Job/GetByCompany`, {
            method: 'GET',
            headers: {
                // 'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            }
        });

        if (response.status === 200)
        {
            const apiResponse = await response.json();
            return apiResponse;
        }

        return null;
    }

    /**
     * Consume API Get Company by ID
     * @param {*} pIdCompany 
     */
    static async getCompanyById(pIdCompany){
        const response = await fetch(`api/comapny/${pIdCompany}`);
        const apiResponse = await response.json();

        return apiResponse;
    }

    /**
     * Consume API Get Roles
     * @return  {[Object]} apiResponse response data Object from API
     */
    static async getRoles(){
        const response = await fetch(`api/role`);
        const apiResponse = await response.json();

        return apiResponse;
    }

    /**
     * Consume API Get Profile
     * @param {*} pToken Token
     */
    static async getProfile(pToken){
        const response = await fetch(`api/User/Profile`, {
            method: 'GET',
            headers: {
                // 'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            }
        });

        if (response.status === 200)
        {
            const apiResponse = await response.json();
            console.log(apiResponse);
            return apiResponse;
        }

        return null;
    }

    /**
     * Consume API to update the profile
     * @param {*} pToken Token
     * @param {*} pNmUser User Name
     * @param {*} pDeEmail Email
     * @param {*} pNuTelephone Telephone
     * @param {*} pUserType UserType
     * @param {*} pIdRole Id Role
     */
    static async updateProfile(pToken, pNmUser, pDeEmail, pNuTelephone, pUserType, pIdRole){
        const response = await fetch(`api/User/Profile`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            },
            body: JSON.stringify({
                "NmUser": pNmUser,
                "DeEmail": pDeEmail,
                "NuTelephone": pNuTelephone,
                "UserType": pUserType,
                "IdRole": pIdRole
            })
        });

        const apiResponse = await response.json();

        console.log(apiResponse);

        return apiResponse;
    }

    /**
     * Consume API to get candidates by job
     * @param {*} pToken Token
     * @param {*} pIdJob Id Job
     */
    static async getCandidates(pToken, pIdJob){
        const response = await fetch(`api/Candidate/${pIdJob}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            }
        });

        const apiResponse = await response.json();

        console.log(apiResponse);

        return apiResponse;
    }

    /**
     * Accept candidate by job
     * @param {*} pToken Token
     * @param {*} pIdCandidate 
     * @param {*} pIdJob 
     */
    static async acceptCandidate(pToken, pIdCandidate, pIdJob){
        const response = await fetch(`api/Candidate/Accept`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            },
            body: JSON.stringify({
                "IdCandidate": pIdCandidate,
                "IdJob": pIdJob
            })
        });

        const apiResponse = await response.json();

        console.log(apiResponse);

        return apiResponse;
    }

    /**
     * Reject candidate by job
     * @param {*} pToken 
     * @param {*} pIdCandidate 
     * @param {*} pIdJob 
     */
    static async rejectCandidate(pToken, pIdCandidate, pIdJob){
        const response = await fetch(`api/Candidate/Reject`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${pToken}`
            },
            body: JSON.stringify({
                "IdCandidate": pIdCandidate,
                "IdJob": pIdJob
            })
        });

        const apiResponse = await response.json();

        console.log(apiResponse);

        return apiResponse;
    }
}