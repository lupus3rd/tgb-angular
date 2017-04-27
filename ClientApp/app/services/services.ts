import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

@Injectable()
export class BlogServices {
    private link: string;
    //Whats a promise?

    constructor(private http: Http) {
        this.link = 'http://localhost:65510/api/'; //to change
    }

    getBlogList() {
        return this.http.get(this.link+'Blog');
    }

    getCategoryList() {
        return this.http.get(this.link +'categories');
    }  

    getBlogDetails(empId: any) {
        return this.http.get(this.link +'Blog/' + empId);
    }  

    postData(empObj: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.post(this.link +'Blog', JSON.stringify(empObj), options);
    }  

    editBlogData(empObj: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.put(this.link +'Blog', JSON.stringify(empObj), options);
    }  

    removeBlogDetails(empId: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        return this.http.delete(this.link +'Blog', new RequestOptions({
            headers: headers,
            body: empId
        }));
    }  

}   