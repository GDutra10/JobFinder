export class UserSessionHelper{

    static setUser(pValue){
        sessionStorage.setItem(`user`, JSON.stringify(pValue));
    }

    static getUser(){
        let user = JSON.parse(sessionStorage.getItem('user'));
        //console.log(user);
        return user;
    }

    static getToken(){
        let user = UserSessionHelper.getUser();
        
        if (user !== null){
            return user.deToken;
        }

        return null;
    }

    static getName(){
        let user = UserSessionHelper.getUser();
        
        if (user !== null){
            return user.nmUser;
        }

        return null;
    }

    static getUserType(){
        let user = UserSessionHelper.getUser();
        
        if (user !== null){
            return user.userType;
        }

        return 0;
    }

    static getIsCompany(){
        let user = UserSessionHelper.getUser();
        
        if (user !== null && user.userType === 2){
            return true;
        }

        return false;
    }

}