export class JobFinderHelper {


    /**
     * Prepare validation messages from apiResponse
     * @param  {[Object]} arg2 responseData Object from API
     * @return  {[string]} return message
     */
    static prepareReturnMessagesResponse(apiResponse) {
        let message = "";

        if (apiResponse !== null && apiResponse !== undefined && 
            apiResponse.errors !== null && apiResponse.errors !== undefined && 
            Object.keys(apiResponse.errors).length > 0) {
            for (const prop in apiResponse.errors) {

                switch (prop) {
                    case "NmUser":
                        apiResponse.errors[prop] = `Name ${apiResponse.errors[prop]}.`;
                        break;
                    case "DeEmail":
                        apiResponse.errors[prop] = `Email ${apiResponse.errors[prop]}.`;
                        break;
                    case "IdRole":
                        apiResponse.errors[prop] = `Role ${apiResponse.errors[prop]}.`;
                        break;
                    case "DePassword":
                        apiResponse.errors[prop] = `Password ${apiResponse.errors[prop]}.`;
                        break;
                    case "DePasswordConfirm":
                        apiResponse.errors[prop] = `Password Confirm ${apiResponse.errors[prop]}.`;
                        break;
                    case "DeDescription":
                        apiResponse.errors[prop] = `Description ${apiResponse.errors[prop]}.`;
                        break;
                    case "DeTitle":
                        apiResponse.errors[prop] = `Title ${apiResponse.errors[prop]}.`;
                        break;
                    default:
                        break;
                }

                message += apiResponse.errors[prop] + "\n";
            }
        }

        return message;
    }
}